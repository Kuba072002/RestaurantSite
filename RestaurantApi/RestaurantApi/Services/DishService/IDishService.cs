using RestaurantApi.ModelsDto.Dish;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.DishService
{
    public interface IDishService
    {
        Task<ServiceResponse> AddDish(AddDishDto request);
        Task<ServiceResponse> DeleteDishes(List<int> dishIds);
        Task<ServiceResponse<List<DishDto>>> GetDishes();
        Task<ServiceResponse<DishDto>> GetDish(int dishId);
    }
}
