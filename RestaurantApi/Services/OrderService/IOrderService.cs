using RestaurantApi.ModelsDto.Dish;
using RestaurantApi.ModelsDto.Order;
using RestaurantApi.ModelsDto.Raports.Order;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse> AddOrder(AddOrderDto request);
        Task<ServiceResponse<List<OrderDto>>> GetOrders();
        Task<ServiceResponse<List<OrderDto>>> GetOrdersByUserId(int userId);
        Task<ServiceResponse<OrderRaportDto>> GetOrdersRaport(RequestOrderRaportDto request);
        Task<ServiceResponse<Dictionary<DishDto, int>>> GetDictOfDishAndQuantity(RequestOrderRaportDto request);
        Task<ServiceResponse<Dictionary<string, List<List<string>> >>> DictOfComponentIdDishNameNumber(RequestOrderRaportDto request);
    }
}
