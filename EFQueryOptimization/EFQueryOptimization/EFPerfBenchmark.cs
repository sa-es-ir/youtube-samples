using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using EFQueryOptimization.Context;
using Microsoft.EntityFrameworkCore;

namespace EFQueryOptimization;

[Config(typeof(Config))]
[HideColumns(Column.RatioSD, Column.AllocRatio)]
[MemoryDiagnoser(false)]
public class EFPerfBenchmark
{
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
        }
    }

    /*
    select (name, username, companyName) of top 2 employees who 
    belong to Backend or Cloud department
    and have at least one Bonus payroll
    and part of the company founded in year 2022.
     */
    [Benchmark(Baseline = true)]
    public List<EmployeeDto> GetEmployees()
    {
        var context = new ApplicationDbContext();

        var allowedDepartments = context
            .Departments.Where(x => x.Name == "Backend" || x.Name == "Cloud")
            .Select(x => x.Id).ToList();

        var employees = context.Employees
            .Include(x => x.Company)
            .Include(x => x.PayRolls)
            .Include(x => x.Department)
            .Include(x => x.User)
            .ThenInclude(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .ToList()
            .Where(x => x.Company.FoundedAt.ToString().Contains("2022"))
            .Select(x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.Name,
                UserId = x.UserId,
                UserFirstname = x.User.FirstName,
                UserLastname = x.User.LastName,
                CompanyId = x.CompanyId,
                CompanyName = x.Company.Name,
                CompanyCountry = x.Company.Country,
                CompanyFoundedAt = x.Company.FoundedAt,
                DepartmentId = x.DepartmentId,
                PayRolls = x.PayRolls,
                Username = x.User.UserName,
            })
            .OrderBy(x => x.Id)
            .ToList();

        var result = new List<EmployeeDto>();

        foreach (var item in employees)
        {
            if (allowedDepartments.Contains(item.DepartmentId)
                && item.PayRolls.Any(x => x.Type == "Bonus"))

                result.Add(item);
        }

        return result.Take(2).ToList();
    }

    [Benchmark]
    public List<EmployeeDto> GetEmployee_Tunned()
    {
        string[] departments = ["Backend", "Cloud"];
        var context = new ApplicationDbContext();

        var employees = context.Employees
            .Where(x => departments.Contains(x.Department.Name)
            && x.Company.FoundedAt.Year == 2022
            && x.PayRolls.Any(x => x.Type == "Bonus"))
            .Select(x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.Name,
                CompanyName = x.Company.Name,
                Username = x.User.UserName
            })
            .OrderBy(x => x.Id)
            .Take(2)
            .ToList();

        return employees;
    }
}