using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.ModelsDto.User;
using RestaurantApi.Services.UserService;

namespace RestaurantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<string>> AddUser(AddUserDto request)
        {
            var response = await _userService.AddUser(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<string>> DeleteUser(int userId)
        {
            var response = await _userService.DeleteUser(userId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpPost("DeleteUsers")]
        public async Task<ActionResult<string>> DeleteUsers(DeleteDto deleteDto)
        {
            var response = await _userService.DeleteUsers(deleteDto.Ids);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var response = await _userService.GetUser(userId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUsers()
        {
            var response = await _userService.GetUsers();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
