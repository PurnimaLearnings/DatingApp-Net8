using API.DTOs;

namespace API;

public interface IUserRepository{
void Update(AppUser appUser);

Task<Boolean> SaveAllAsync();

Task<IEnumerable<AppUser>> GetUsersAsync();
Task<AppUser> GetUserAsyncById(int id);

Task<AppUser> GetUserAsyncByUsername(string Username);

Task<IEnumerable<MemberDTO>> GetMemebersAsync();

Task<MemberDTO?> GetMemberAsync(string username);
}