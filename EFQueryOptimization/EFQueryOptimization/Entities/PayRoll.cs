using System;

namespace DatabaseNight.Entities;

public class PayRoll
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string Type { get; set; }

    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }
}