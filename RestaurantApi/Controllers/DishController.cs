using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.ModelsDto.Dish;
using RestaurantApi.ModelsDto.User;
using RestaurantApi.Services.DishService;

namespace RestaurantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost("AddDish")]
        public async Task<ActionResult<string>> AddDish([FromForm]AddDishDto request)
        {
            var response = await _dishService.AddDish(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpPost("DeleteDishes")]
        public async Task<ActionResult<string>> DeleteDishes(DeleteDto deleteDto)
        {
            var response = await _dishService.DeleteDishes(deleteDto.Ids);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet()]
        public async Task<ActionResult<DishDto>> GetDishes()
        {
            var response = await _dishService.GetDishes();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetDish(int dishId)
        {
            var response = await _dishService.GetDish(dishId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
