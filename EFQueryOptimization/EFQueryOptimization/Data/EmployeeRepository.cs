using BenchmarkDotNet.Attributes;
using EFQueryOptimization.Context;
using Microsoft.EntityFrameworkCore;

namespace EFQueryOptimization.Data
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<EmployeeDto> GetEmployees()
        {
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
        public async Task<List<EmployeeDto>> GetEmployees_Tunned()
        {
            await Task.Delay(100);
            return new List<EmployeeDto>();
        }
    }
}
