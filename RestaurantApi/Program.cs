using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Services.DishService;
using RestaurantApi.Services.UserService;
using RestaurantApi.Services.OrderService;
using RestaurantApi.Services.WholesalerService;
using RestaurantApi.Services.WholesalerOrderService;
using RestaurantApi.Services.ComponentService;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("postgresDbConnection2"))
    );
// Add services to the container.

builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWholesalerService, WholesalerService>();
builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddScoped<IWholesalerOrderService, WholesalerOrderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();
