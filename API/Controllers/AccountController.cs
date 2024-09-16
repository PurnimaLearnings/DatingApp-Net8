using System.Security.Cryptography;
using System.Text;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class AccountController(DataContext context, ITokenService tokenService) : BaseAPIController
{

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {

        if (await UserExists(registerDTO.UserName))
        {
            return BadRequest("User name is already taken");
        }

return Ok();
        // var hmac = new HMACSHA1();

        // var user = new AppUser
        // {
        //     UserName = registerDTO.UserName,
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
        //     PasswordSalt = hmac.Key
        // };
        // context.Users.Add(user);
        // await context.SaveChangesAsync();
        // var userDTO= new UserDTO{
        //     Username=user.UserName,
        //     Token= tokenService.CreateToken(user)
        // };
        // return userDTO;

    }


    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await context.Users
        .Include(p=>p.Photos).FirstOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());
        if (user == null)
        {
            return Unauthorized("Invalid user.");
        }

        var hmac = new HMACSHA1(user.PasswordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        for (int i = 0; i < computeHash.Length; i++)
        {
            if (computeHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Incorrect Password.");
            }
        }

        var userDTO= new UserDTO(){
            Username=user.UserName,
            Token=tokenService.CreateToken(user),
            PhotoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.url
        };
        return userDTO;
    }
    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }

}