using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.ModelsDto.Component;
using RestaurantApi.ModelsDto.Raports.Component;
using RestaurantApi.ModelsDto.Raports.Order;
using RestaurantApi.Services.ComponentService;

namespace RestaurantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;
        public ComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<string>> AddComponent(AddComponentDto request)
        {
            var response = await _componentService.AddComponent(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpGet()]
        public async Task<ActionResult<ComponentDto>> GetComponents()
        {
            var response = await _componentService.GetComponents();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost("Raport")]
        public async Task<ActionResult<ComponentRaportDto>> GetComponentsRaport(RequestOrderRaportDto request)
        {
            var response = await _componentService.GetComponentsRaport(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data.ComponentQuantityDict.ToList());
        }
    }
}
