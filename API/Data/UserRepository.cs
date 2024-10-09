using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<MemberDTO?> GetMemberAsync(string username)
    {
        return await context.Users.
        Where(x=>x.UserName==username).
        ProjectTo<MemberDTO>(mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<PagedList<MemberDTO>> GetMemebersAsync(UserParams userParams)
    {
       var query= context.Users.
       ProjectTo<MemberDTO>(mapper.ConfigurationProvider);

       return await PagedList<MemberDTO>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
    }

    public  async Task<AppUser?> GetUserAsyncById(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string Username)
    {
       return await context.Users.Include(x=>x.Photos).SingleOrDefaultAsync(x=> x.UserName==Username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
      return await context.Users.ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
     return  await  context.SaveChangesAsync()>0;
    }

    public void Update(AppUser user)
    {
        context.Entry(user).State=EntityState.Modified;
    }
}