using API.DTOs;
using API.Entities;
using API.Extension;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfilers:Profile
{
public AutoMapperProfilers()
{
CreateMap<AppUser, MemberDTO>()
.ForMember(d=>d.Age, o=>o.MapFrom(s=>s.DateOfBirth.CalculateAge()))
.ForMember(d=>d.PhotoUrl, o=>o.MapFrom(s=>s.Photos.FirstOrDefault(x=>x.IsMain)!.url));

CreateMap<Photo, PhotoDTO>();    
CreateMap<MemberUpdateDTO, AppUser>();
}
}