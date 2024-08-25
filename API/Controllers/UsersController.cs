using System.Security.Claims;
using API.Data;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository repository, IMapper mapper) : BaseAPIController
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

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO){
        var username= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(username==null){
            return BadRequest("No user name found in the claim.");
        }

        var user= await repository.GetUserAsyncByUsername(username);
        if(user==null){
            return BadRequest("Could not find the user.");
        }

        mapper.Map(memberUpdateDTO, user);
        
        if(await repository.SaveAllAsync()){
            return NoContent();
        }
        return BadRequest("failed to update the user.");
    }
}
