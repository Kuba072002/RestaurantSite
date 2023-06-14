using RestaurantApi.ModelsDto.Wholesaler;
using RestaurantApi.ModelsDto.Raports.Wholesaler;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.WholesalerService
{
    public interface IWholesalerService
    {
        Task<ServiceResponse> AddWholesaler(AddWholesalerDto request);
        Task<ServiceResponse<List<WholesalerDto>>> GetWholesalers();
        Task<ServiceResponse<List<WholesalerRaportDto>>> GetWholesalerRaport(RequestWholesalerRaportDto request);
    }
}
