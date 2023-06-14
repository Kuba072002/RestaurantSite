using RestaurantApi.ModelsDto.ComponentOrder;
using RestaurantApi.ModelsDto.Wholesaler;

namespace RestaurantApi.ModelsDto.WholesalerOrder
{
    public class WholesalerOrderDto
    {
        public int Id { get; set; }
        public int WholesalerId { get; set; }
        //public WholesalerDto? Wholesaler { get; set; }
        public List<ComponentOrderDto>? ComponentOrders { get; set; }
        public string OrderDate { get; set; }
    }
}
