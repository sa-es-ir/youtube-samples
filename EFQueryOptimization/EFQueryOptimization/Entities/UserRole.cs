namespace DatabaseNight.Entities;

public class UserRole
{
    public int Id { get; set; }

    public User User { get; set; }

    public int UserId { get; set; }

    public Role Role { get; set; }

    public int RoleId { get; set; }
}