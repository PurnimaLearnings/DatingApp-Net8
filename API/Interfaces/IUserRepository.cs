using API.DTOs;
using API.Helpers;

namespace API;

public interface IUserRepository{
void Update(AppUser appUser);

Task<Boolean> SaveAllAsync();

Task<IEnumerable<AppUser>> GetUsersAsync();
Task<AppUser> GetUserAsyncById(int id);

Task<AppUser> GetUserByUsernameAsync(string Username);

Task<PagedList<MemberDTO>> GetMemebersAsync(UserParams userParamsS);

Task<MemberDTO?> GetMemberAsync(string username);
}