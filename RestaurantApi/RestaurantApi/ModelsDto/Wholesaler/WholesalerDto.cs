using RestaurantApi.ModelsDto.Raports.Wholesaler;
using RestaurantApi.ModelsDto.WholesalerOrder;

namespace RestaurantApi.ModelsDto.Wholesaler
{
    public class WholesalerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        //public List<WholesalerRaportDto>? RaportDtos { get; set; }
    }
}
