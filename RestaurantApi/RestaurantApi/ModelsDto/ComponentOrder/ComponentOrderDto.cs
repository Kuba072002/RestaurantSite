using RestaurantApi.ModelsDto.Component;
using RestaurantApi.ModelsDto.Wholesaler;

namespace RestaurantApi.ModelsDto.ComponentOrder
{
    public class ComponentOrderDto
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal Quantity { get; set; }
        //public int WholesalerId { get; set; }
        //public string WholesalerName { get; set; }
        public string FreshnessDate { get; set; }
    }
}
