using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;
using RestaurantApi.ModelsDto.Order;
using RestaurantApi.ModelsDto.User;
using RestaurantApi.ModelsDto.Dish;
using RestaurantApi.Utility;
using RestaurantApi.ModelsDto.Raports.Order;
using System.Globalization;
using RestaurantApi.ModelsDto.Component;

namespace RestaurantApi.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddOrder(AddOrderDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user != null) {
                var order = new Order
                {
                    UserId = request.UserId,
                    User = user,
                    Dishes = new List<Dish>(),
                    OrderDate = DateTime.Now.AddDays(-2).Date.ToString("yyyy-MM-dd")
                };
                foreach (var dishId in request.DishIds)
                {
                    var dish = await _context.Dishes
                        .Include(d => d.DishComponents)
                        .FirstOrDefaultAsync(d => d.Id == dishId);
                    if (dish != null)
                    {
                        var res = await ReduceComponentQuantities(dish);
                        if(res == false)
                            return new ServiceResponse { Success = false, Message = "Not enough components" };
                        else
                            order.Dishes.Add(dish);
                    }
                    else
                        return new ServiceResponse { Success = false, Message = "That dish not exist" };
                }
                if(order.Dishes.Count() > 0)
                {
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    return new ServiceResponse { Success = true, Message = "Order added successfully!" };
                }
            }
            return new ServiceResponse { Success = false, Message = "Failure" };
        }

        private async Task<bool> ReduceComponentQuantities(Dish dish)
        {
            var todayDate = DateTime.Now.ToString("yyyy-MM-dd");
            foreach(var dc in dish.DishComponents)
            {
                var componentQunatity = await _context.ComponentQunatities
                    .Where(cq =>
                        cq.ComponentId == dc.ComponentId &&
                        string.Compare(cq.FreshnessDate, todayDate) >= 0 &&
                        cq.Quantity >= dc.Quantity)
                    .OrderBy(cq => string.Compare(cq.FreshnessDate, todayDate))
                    .FirstOrDefaultAsync();
                if (componentQunatity != null)
                {
                    if(componentQunatity.Quantity < dc.Quantity)
                    {
                        return false;
                    }
                    componentQunatity.Quantity -= dc.Quantity;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<ServiceResponse<List<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders.Include(o => o.User).Include(o => o.Dishes).ToListAsync();
            var orderDtos = new List<OrderDto>();
            foreach ( var order in orders)
            {
                var dishDtos = order.Dishes.Select(
                    d => new DishDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Price = d.Price,
                        Description = d.Description,
                        Created_at = d.Created_at
                    })
                    .ToList();
                orderDtos.Add(new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    User = new UserDto
                    {
                        Id = order.UserId,
                        Username = order.User.Username,
                        FirstName = order.User.FirstName,
                        LastName = order.User.LastName,
                        Created_at = order.User.Created_at
                    },
                    Dishes = dishDtos,
                    OrderDate = order.OrderDate,
                });
            }
            return new ServiceResponse<List<OrderDto>> { Data = orderDtos, Success = true, Message = "Success" };
        }

        public async Task<ServiceResponse<List<OrderDto>>> GetOrdersByUserId(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .ThenInclude(o => o.Dishes)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if(user != null)
            {
                var orderDtos = new List<OrderDto>();
                foreach (var order in user.Orders)
                {
                    var dishDtos = order.Dishes.Select(
                        d => new DishDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Price = d.Price,
                            Description = d.Description,
                            Created_at = d.Created_at
                        })
                        .ToList();
                    orderDtos.Add(new OrderDto
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        Dishes = dishDtos,
                        OrderDate = order.OrderDate,
                    });
                }
                return new ServiceResponse<List<OrderDto>> { Data = orderDtos, Success = true, Message = "Success" };
            }
            return new ServiceResponse<List<OrderDto>> { Success = false, Message = "Failure" };
        }

        public async Task<ServiceResponse<OrderRaportDto>> GetOrdersRaport(RequestOrderRaportDto request)
        {
            var orders = await _context.Orders
                .Where(o => string.Compare(o.OrderDate, request.StartDate) >= 0 && string.Compare(o.OrderDate, request.EndDate) <= 0)
                .Include(o => o.Dishes)
                .ToListAsync();
            if(orders.Count == 0)
            {
                return new ServiceResponse<OrderRaportDto> { Success = false, Message = "Failure" };
            }
            var response = new OrderRaportDto
            {
               // Year = request.Year,
                //Month = request.Month,
                NumberOfOrder = orders.Count,
                NumberOfDistinctUsers = orders.Select(o => o.UserId).Distinct().Count(),
                AverageNumberOfOrdersForUser = orders.GroupBy(o => o.UserId).Average(group => group.Count()).ToString("0.000")
            };
            var dictionary = new Dictionary<string, int>();
            foreach(var order in orders)
            {
                foreach(var dish in order.Dishes)
                {
                    if (dictionary.ContainsKey(dish.Name))
                    {
                        dictionary[dish.Name] += 1;
                    }
                    else
                    {
                        dictionary.Add(dish.Name, 1);
                    }
                }
            }
            response.Dictionary = dictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return new ServiceResponse<OrderRaportDto> { Data = response, Success = true, Message = "Success" };
        }

        public async Task<ServiceResponse<Dictionary<DishDto,int>>> GetDictOfDishAndQuantity(RequestOrderRaportDto request) {
            var orders = await _context.Orders
                .Where(o => string.Compare(o.OrderDate, request.StartDate) >= 0 && string.Compare(o.OrderDate, request.EndDate) <= 0)
                .Include(o => o.Dishes)
                    .ThenInclude(d => d.DishComponents)
                        .ThenInclude(dc => dc.Component)
                .ToListAsync();
            if (orders.Count == 0)
            {
                return new ServiceResponse<Dictionary<DishDto, int>> { Success = false, Message = "Failure" };
            }
            var dictionary = new Dictionary<DishDto, int>();
            foreach (var order in orders)
            {
                foreach (var dish in order.Dishes)
                {
                    var dishDto = dictionary.Keys
                            .FirstOrDefault(d => d.Id == dish.Id);
                    if (dishDto != null)
                    {
                        dictionary[dishDto] += 1;
                    }
                    else
                    {
                        dishDto = new DishDto
                        {
                            Id = dish.Id,
                            Name = dish.Name,
                        };
                        dishDto.DishComponents = new Dictionary<string, decimal>();
                        foreach (var component in dish.DishComponents)
                        {
                            dishDto.DishComponents.Add(component.Component.Name, component.Quantity);
                        }
                        dictionary.Add(dishDto, 1);
                    }
                }
            }
            return new ServiceResponse<Dictionary<DishDto, int> >{ Data = dictionary, Success = true, Message = "Success" };
        }

        public async Task<ServiceResponse<Dictionary<string, List<List<string>>>>> DictOfComponentIdDishNameNumber(RequestOrderRaportDto request)
        {
            var dishCountMap = await _context.Orders
                .Where(o => string.Compare(o.OrderDate, request.StartDate) >= 0 && string.Compare(o.OrderDate, request.EndDate) <= 0)
                .Include(o => o.Dishes)
                .SelectMany(o => o.Dishes)
                .GroupBy(d => d.Id)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
            var dishes = await _context.Dishes
                .Include(d=> d.DishComponents)
                    .ThenInclude(dc => dc.Component)
                .ToListAsync();
            var result = new Dictionary<string, List<List<string>>>();
            foreach (var dish in dishes)
            {
                foreach (var dc in dish.DishComponents)
                {
                    if (result.ContainsKey(dc.Component.Name))
                    {
                        result[dc.Component.Name].Add(new List<string> { dish.Name, (dc.Quantity * dishCountMap[dish.Id]).ToString("0.00"), dc.Component.Unit });
                    }
                    else
                    {
                        result.Add(dc.Component.Name, 
                            new List<List<string>>() { new List<string>{ dish.Name, (dc.Quantity * dishCountMap[dish.Id]).ToString("0.00"),dc.Component.Unit } } 
                            );
                    }
                }
            }

            return new ServiceResponse<Dictionary<string, List<List<string>>>> { Data= result, Success = true, Message = "Success" };
        }
    }
}
