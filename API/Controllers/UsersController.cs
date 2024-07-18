using System.Security.Cryptography.X509Certificates;
using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/[Controller]")]
public class UsersController(DataContext context): ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers(){
        var users= context.Users.ToList();
        return Ok(users);
    }
[HttpGet("{id}")]//api/users/2
    public ActionResult<IEnumerable<AppUser>> GetUser(int id){
        var user= context.Users.Find(id);
        if(user==null)
        {
            return NotFound();
        }


        return Ok(user);
    }
}
