using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.ModelsDto.WholesalerOrder;
using RestaurantApi.Services.WholesalerOrderService;

namespace RestaurantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerOrderController : ControllerBase
    {
        private readonly IWholesalerOrderService _wholesalerOrderService;

        public WholesalerOrderController(IWholesalerOrderService wholesalerOrderService)
        {
            _wholesalerOrderService = wholesalerOrderService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<string>> AddWholesalerOrder(AddWholesalerOrderDto request)
        {
            var response = await _wholesalerOrderService.AddWholesalerOrder(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet()]
        public async Task<ActionResult<List<WholesalerOrderDto>>> GetWholesalerOrders()
        {
            var response = await _wholesalerOrderService.GetWholesalerOrders();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpGet("{wholesalerOrderId}")]
        public async Task<ActionResult<WholesalerOrderDto>> GetWholesalerOrder(int wholesalerOrderId)
        {
            var response = await _wholesalerOrderService.GetWholesalerOrder(wholesalerOrderId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
