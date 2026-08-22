using WebApplicationAPI.DTo;

namespace WebApplicationAPI.IServices
{
    public interface IAuthService
    {
        Task<Tuple<int, string>> LoginUser(UserDto dto);
    }
}
