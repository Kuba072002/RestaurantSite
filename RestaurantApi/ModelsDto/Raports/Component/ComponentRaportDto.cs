using RestaurantApi.ModelsDto.Component;

namespace RestaurantApi.ModelsDto.Raports.Component
{
    public class ComponentRaportDto
    {
        //component [0] - used, [1] - all, [2] - rotten
        //[0] - all, [1] - rotten
        public Dictionary<ComponentDto,List<decimal>> ComponentQuantityDict { get; set; }
        //public Dictionary<int,List<(string,decimal)>> DictOfComponentIdDishNameNumber { get; set; }
        //public Dictionary<ComponentDto, Dictionary<string,int>> ComponentDishQuantityDict { get; set; }
    }
}
