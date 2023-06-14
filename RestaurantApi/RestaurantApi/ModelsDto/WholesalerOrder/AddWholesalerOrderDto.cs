using RestaurantApi.Models;

namespace RestaurantApi.ModelsDto.WholesalerOrder
{
    public class AddWholesalerOrderDto
    {
        public int WholesalerId { get; set; }
        public Dictionary<int,decimal> ComponentOrders { get; set; }
        //public string OrderDate { get; set; }
    }
}
