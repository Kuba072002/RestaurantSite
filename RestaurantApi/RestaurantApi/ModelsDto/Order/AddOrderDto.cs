namespace RestaurantApi.ModelsDto.Order
{
    public class AddOrderDto
    {
        public int UserId { get; set; }
        public List<int> DishIds { get; set; }
    }
}
