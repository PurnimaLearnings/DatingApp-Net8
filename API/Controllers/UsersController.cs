using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseAPIController
{
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = context.Users.ToList();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id}")]//api/users/2
    public ActionResult<IEnumerable<AppUser>> GetUser(int id)
    {
        var user = context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }


        return Ok(user);
    }
}
