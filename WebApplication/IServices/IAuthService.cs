using WebApplicationAPI.DTo;

namespace WebApplicationAPI.IServices
{
    public interface IAuthService
    {
        Task<Tuple<int, TokenDto>> LoginUser(UserDto dto);
        Task<Tuple<int, string>> RegisterUser(UserDto dto);
    }
}
