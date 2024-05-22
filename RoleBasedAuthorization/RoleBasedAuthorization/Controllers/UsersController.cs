using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthorization.Controllers;

public class UsersController : ControllerBase
{
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public ActionResult GetUsers()
    {
        var users = new[]
        {
            new { Id = 1, Name = "John" },
            new { Id = 2, Name = "Jane" },
            new { Id = 3, Name = "Jack" }
        };

        return Ok(users);
    }
}
