using DatabaseNight.Entities;
using System;
using System.Collections.Generic;

namespace DatabaseNight;

public class EmployeeDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; }

    public string UserFirstname { get; set; }

    public string UserLastname { get; set; }

    public int CompanyId { get; set; }

    public string CompanyName { get; set; }

    public string CompanyCountry { get; set; }

    public DateTime CompanyFoundedAt { get; set; }

    public int DepartmentId { get; set; }

    public string Department { get; set; }

    public List<PayRoll> PayRolls { get; set; }
}
