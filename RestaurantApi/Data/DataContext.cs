using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RestaurantApi.Models;

namespace RestaurantApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishComponent> DishComponents { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentOrder> ComponentOrders { get; set; }
        public DbSet<ComponentQunatity> ComponentQunatities { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerOrder> WholesalerOrders { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}
