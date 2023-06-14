namespace RestaurantApi.Models
{
    public class Wholesaler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<WholesalerOrder> WholesalerOrders { get; set; }
    }
}
