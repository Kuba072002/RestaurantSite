namespace RestaurantApi.Models
{
    public class ComponentQunatity
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public decimal Quantity { get; set; }
        public string FreshnessDate { get; set; }
    }
}
