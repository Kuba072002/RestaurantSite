using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;
using RestaurantApi.ModelsDto.User;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context) {
            _context = context;
        }

        public async Task<ServiceResponse> AddUser(AddUserDto request)
        {
            if(!await _context.Users.AnyAsync(u => u.Username.Equals(request.Username)))
            {
                User u = new User
                {
                    Username = request.Username,
                    FirstName= request.FirstName,
                    LastName= request.LastName,
                    Created_at = DateTime.Now.ToString("yyyy-MM-dd"),
                };
                _context.Users.Add(u);
                await _context.SaveChangesAsync();
                return new ServiceResponse { Success = true, Message = "User added successfully!" };
            }
            return new ServiceResponse { Success = false, Message = "Failure" };
        }

        public async Task<ServiceResponse> DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Id == userId);
            if (user == null)
            {
                return new ServiceResponse { Success = false, Message = "Failure" };
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new ServiceResponse { Success = true, Message = "User deleted successfully!" };
        }

        public async Task<ServiceResponse> DeleteUsers(List<int> userIds)
        {
            var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();
            return new ServiceResponse { Success = true, Message = "Users deleted successfully!" };
        }

        public async Task<ServiceResponse<UserDto>> GetUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ServiceResponse<UserDto> { Success = false, Message = "Failure" };
            }
            var response = new UserDto { 
                Id= userId,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Username= user.Username,
                Created_at= user.Created_at
            };
            return new ServiceResponse<UserDto> {Data=response, Success = true, Message = "Success" };
        }

        public async Task<ServiceResponse<List<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u=> new UserDto 
                {
                    Id = u.Id,
                    FirstName= u.FirstName,
                    LastName= u.LastName,
                    Username= u.Username,
                    Created_at= u.Created_at
                })
                .ToListAsync();
            return new ServiceResponse<List<UserDto>> { Data = users, Success = true, Message = "Success" };
        }

        
    }
}
