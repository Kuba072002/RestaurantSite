namespace RestaurantApi.Models
{
    public class DishComponent
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public decimal Quantity { get; set; }
    }

    //public int DishId { get; set; }
    //public Dish Dish { get; set; }
    //List<Component> Components { get; set; }

}
