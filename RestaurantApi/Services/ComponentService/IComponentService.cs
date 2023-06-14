using RestaurantApi.ModelsDto.Component;
using RestaurantApi.ModelsDto.Raports.Component;
using RestaurantApi.ModelsDto.Raports.Order;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.ComponentService
{
    public interface IComponentService
    {
        Task<ServiceResponse> AddComponent(AddComponentDto request);
        Task<ServiceResponse<List<ComponentDto>>> GetComponents();
        Task<ServiceResponse<ComponentRaportDto>> GetComponentsRaport(RequestOrderRaportDto request);
        Task<ServiceResponse<Dictionary<ComponentDto,decimal>>> GetComponentsQuantities(RequestOrderRaportDto request);
    }
}
