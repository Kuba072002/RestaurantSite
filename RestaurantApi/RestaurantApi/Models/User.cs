namespace RestaurantApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Created_at { get; set; }
        public List<Order> Orders { get; set; }
    }
}
