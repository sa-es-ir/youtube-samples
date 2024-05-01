using System.Collections.Generic;

namespace DatabaseNight.Entities;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public int CompanyId { get; set; }

    public Company Company { get; set; }

    public int DepartmentId { get; set; }

    public Department Department { get; set; }

    public List<PayRoll> PayRolls { get; set; }
}