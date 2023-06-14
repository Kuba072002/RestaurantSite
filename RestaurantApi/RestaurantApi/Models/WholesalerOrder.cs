namespace RestaurantApi.Models
{
    public class WholesalerOrder
    {
        public int Id { get; set; }
        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }
        public List<ComponentOrder> ComponentOrders { get; set; }
        public string OrderDate { get; set; }
        public WholesalerOrder()
        {
            ComponentOrders = new List<ComponentOrder>();
        }
    }
}
