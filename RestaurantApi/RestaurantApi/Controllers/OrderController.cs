using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.ModelsDto.Order;
using RestaurantApi.ModelsDto.Raports.Order;
using RestaurantApi.Services.OrderService;
using System;

namespace RestaurantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<string>> AddOrder(AddOrderDto request)
        {
            var response = await _orderService.AddOrder(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpPost("RadndomOrdersAdd")]
        public async Task<ActionResult<string>> RadndomOrdersAdd(int number)
        {
            Random r = new Random();
            for(int i=0;i<number; i++)
            {
                int dishIdsLength = r.Next(1,3);
                List<int> dishIds = new List<int>();
                for (int j = 0; j < dishIdsLength; j++)
                {
                    int dishId = r.Next(5, 11); // Replace this with the logic to generate random dish IDs
                    dishIds.Add(dishId);
                }
                var response = await _orderService.AddOrder(new AddOrderDto
                {
                    UserId = r.Next(30, 60),
                    DishIds = dishIds
                }) ;
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
            }
            
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            var response = await _orderService.GetOrders();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpGet("GetOrdersByUserId/{userId}")]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByUserId(int userId)
        {
            var response = await _orderService.GetOrdersByUserId(userId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost("GetOrdersRaport/")]
        public async Task<ActionResult<OrderRaportDto>> GetOrdersRaport(RequestOrderRaportDto request)
        {
            var response = await _orderService.GetOrdersRaport(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost("GetDictOfDishAndQuantity/")]
        public async Task<ActionResult<OrderRaportDto>> GetDictOfDishAndQuantity(RequestOrderRaportDto request)
        {
            var response = await _orderService.GetDictOfDishAndQuantity(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data.ToList());
        }

        [HttpPost("DictOfComponentIdDishNameNumber/")]
        public async Task<ActionResult<Dictionary<string, List<List<string>>>>> DictOfComponentIdDishNameNumber(RequestOrderRaportDto request)
        {
            var response = await _orderService.DictOfComponentIdDishNameNumber(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
