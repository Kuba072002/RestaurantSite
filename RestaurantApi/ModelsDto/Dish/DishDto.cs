namespace RestaurantApi.ModelsDto.Dish
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Created_at { get; set; }
        public Dictionary<string,decimal>? DishComponents { get; set; }
    }
}
