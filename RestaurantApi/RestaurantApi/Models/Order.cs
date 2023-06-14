namespace RestaurantApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Dish> Dishes { get; set; }
        public string OrderDate { get; set; }
    }
}
