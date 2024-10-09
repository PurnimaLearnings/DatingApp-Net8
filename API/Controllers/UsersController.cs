using System.Runtime.CompilerServices;
using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extension;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository repository, IMapper mapper,
 IPhotoService photoService) : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers([FromQuery]UserParams userParams)
    {
        
        var users = await repository.GetMemebersAsync(userParams);
        Response.AddPaginationHeader(users);

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

        var user= await repository.GetUserByUsernameAsync(User.GetUserName());
        if(user==null){
            return BadRequest("Could not find the user.");
        }

        mapper.Map(memberUpdateDTO, user);
        
        if(await repository.SaveAllAsync()){
            return NoContent();
        }
        return BadRequest("failed to update the user.");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file){
        var user= await repository.GetUserByUsernameAsync(User.GetUserName());
        if(user==null){
            BadRequest("Can not update the user.");
        }        

        var result=await photoService.AddPhotoAsync(file);
        if(result.Error !=null){
            return BadRequest(result.Error.Message);
        }

        var photo=new Photo{
            url=result.SecureUrl.AbsoluteUri,
            publicId=result.PublicId
        };
        if(user.Photos.Count==0){
            photo.IsMain=true;
        }
        user.Photos.Add(photo);
        
        if(await repository.SaveAllAsync())
            return CreatedAtAction(nameof(GetUser), new { username = user.UserName}, mapper.Map<PhotoDTO>(photo) );

        return BadRequest("Problem adding photo.");


    }

    [HttpPut("set-main-photo/{photoid:int}")]
    public async Task<ActionResult> SetMainPhoto(int photoid){

        var user=await repository.GetUserByUsernameAsync(User.GetUserName());
        if(user==null){
            return BadRequest("Could not get the user.");
        }
    var photo= user.Photos.FirstOrDefault(x=>x.Id==photoid);

    if(photo==null ||photo.IsMain){
        return BadRequest("Can not use this as main photo.");
    }

    var currentMain=user.Photos.FirstOrDefault(x=>x.IsMain);
    if(currentMain!=null){
        currentMain.IsMain=false;
        photo.IsMain=true;
    }

    if(await repository.SaveAllAsync()){
        return NoContent();
    }

    return BadRequest("Problem saving the main photo.");
    }
    [HttpDelete("delete-photo/{photoid}")]
    public async Task<ActionResult> DeletePhoto(int photoId){
        var user= await repository.GetUserByUsernameAsync(User.GetUserName());
    if(user==null){
        return BadRequest("User not found.");
    }
    var photo= user.Photos.FirstOrDefault(x=>x.Id==photoId);
    if(photo==null || photo.IsMain){
        return BadRequest("This photo can not be deleted.");
    }

    if(photo.publicId !=null){
        var result=await photoService.DeletePhotoAsync(photo.publicId);
        if(result.Error!=null){
            return BadRequest(result.Error.Message);
        }
    }

    user.Photos.Remove(photo);

    if(await repository.SaveAllAsync())
    {
        return Ok();
    }

    return BadRequest("Could not delete the photo.");
    }
}
