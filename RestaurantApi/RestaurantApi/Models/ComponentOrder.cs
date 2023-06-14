namespace RestaurantApi.Models
{
    public class ComponentOrder
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public decimal Quantity { get; set; }
        public string OrderDate { get; set; }
        public string FreshnessDate { get; set; }
    }

    //public int WholesalerId { get; set; }
    //public Wholesaler Wholesaler { get; set; }
    //public string OrderDate { get; set; }
    //public Dictionary<Component,int> Components { get; set; }

}
