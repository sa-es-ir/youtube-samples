using System;
using System.Collections.Generic;

namespace DatabaseNight.Entities;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public DateTime Created { get; set; }
}