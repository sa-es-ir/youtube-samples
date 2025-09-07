using EFCoreCancellationToken.Infrastructure;
using EFCoreCancellationToken.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCancellationToken.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await userService.GetListAsync(HttpContext.RequestAborted);
        }
    }
}
