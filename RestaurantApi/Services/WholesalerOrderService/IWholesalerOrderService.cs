using RestaurantApi.ModelsDto.WholesalerOrder;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.WholesalerOrderService
{
    public interface IWholesalerOrderService
    {
        Task<ServiceResponse> AddWholesalerOrder(AddWholesalerOrderDto request);
        Task<ServiceResponse<List<WholesalerOrderDto>>> GetWholesalerOrders();
        Task<ServiceResponse<List<WholesalerOrderDto>>> GetWholesalerOrdersByWholesalerId(int wholesalerId);
        Task<ServiceResponse<WholesalerOrderDto>> GetWholesalerOrder(int wholesalerOrderId);
    }
}
