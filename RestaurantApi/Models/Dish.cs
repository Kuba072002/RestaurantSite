namespace RestaurantApi.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Created_at { get; set; }
        public List<Order> Orders { get; set; }
        public List<DishComponent> DishComponents { get; set; }
        public int? PictureId { get; set; }
        public Picture? Picture { get; set; }
    }
    //public List<Component> Components { get; set; }

}
