namespace RestaurantApi.ModelsDto.Dish
{
    public class AddDishDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Dictionary<int,decimal> DishComponents { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
