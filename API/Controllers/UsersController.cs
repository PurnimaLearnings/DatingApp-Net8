using API.Data;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository repository) : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        var users = await repository.GetMemebersAsync();
        
        return Ok(users);
    }

    [HttpGet("{username}")]//api/users/2
    public async Task<ActionResult<MemberDTO>> GetUser(string username)
    {
        var user = await repository.GetMemberAsync(username);
        if (user == null)
        {
            return NotFound();
        }
      
        return Ok(user);
    }
}
