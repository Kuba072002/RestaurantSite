using RestaurantApi.ModelsDto.Dish;
using RestaurantApi.ModelsDto.User;

namespace RestaurantApi.ModelsDto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public List<DishDto> Dishes { get; set; }
        public string OrderDate { get; set; }
    }
}
