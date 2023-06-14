namespace RestaurantApi.Models
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FreshnessTime { get; set; }
        public string Unit { get; set; }
        public List<ComponentOrder> ComponentOrders { get; set; }
    }

    //public string FreshnessTime { get; set; }
    //public List<Dish> Dishes { get; set; }
}
