using System.Collections.Generic;

namespace DatabaseNight.Entities;

public class Role
{
    public int Id { get; set; }

    public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public string Name { get; set; }
}