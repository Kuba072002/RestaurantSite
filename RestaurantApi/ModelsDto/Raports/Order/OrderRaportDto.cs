namespace RestaurantApi.ModelsDto.Raports.Order
{
    public class OrderRaportDto
    {
       // public string Year { get; set; }
       // public string Month { get; set; }
        public int NumberOfOrder { get; set; }
        public int NumberOfDistinctUsers { get; set; }
        public string AverageNumberOfOrdersForUser { get; set; }
        public Dictionary<string,int> Dictionary { get; set; }
    }
}
