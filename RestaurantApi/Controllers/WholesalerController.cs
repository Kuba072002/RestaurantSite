using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.ModelsDto.Raports.Wholesaler;
using RestaurantApi.ModelsDto.Wholesaler;
using RestaurantApi.Services.WholesalerService;

namespace RestaurantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerController : ControllerBase
    {
        private readonly IWholesalerService _wholesalerService;

        public WholesalerController(IWholesalerService wholesalerService)
        {
            _wholesalerService = wholesalerService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<string>> AddWholesaler(AddWholesalerDto request)
        {
            var response = await _wholesalerService.AddWholesaler(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet()]
        public async Task<ActionResult<WholesalerDto>> GetWholesalers()
        {
            var response = await _wholesalerService.GetWholesalers();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost("Raport")]
        public async Task<ActionResult<List<WholesalerRaportDto>>> GetWholesalerRaport(RequestWholesalerRaportDto request)
        {
            var response = await _wholesalerService.GetWholesalerRaport(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
