using RestaurantApi.Models;
using RestaurantApi.ModelsDto.User;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse> AddUser(AddUserDto request);
        Task<ServiceResponse> DeleteUser(int userId);
        Task<ServiceResponse> DeleteUsers(List<int> userIds);
        Task<ServiceResponse<UserDto>> GetUser(int userId);
        Task<ServiceResponse<List<UserDto>>> GetUsers();
    }
}
