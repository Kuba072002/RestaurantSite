using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RestaurantApi.Data;
using RestaurantApi.Models;
using RestaurantApi.ModelsDto.Dish;
using RestaurantApi.ModelsDto.User;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.DishService
{
    public class DishService : IDishService
    {
        private readonly DataContext _context;

        public DishService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddDish(AddDishDto request)
        {
            if (!await _context.Dishes.AnyAsync(d => d.Name.Equals(request.Name)))
            {
                Dish d = new()
                {
                    Name = request.Name,
                    Price= request.Price,
                    Description= request.Description,
                    Created_at = DateTime.Now.ToString("yyyy-MM-dd"),
                };
                //if(request.Picture != null)
                //{
                //    if (request.Picture.Length > 0)
                //    {
                //        var picture = new Picture();
                //        picture.Create(request.Picture);
                //        _context.Pictures.Add(picture);
                //        d.Picture = picture;
                //    }
                //}
                var dishComponents = await CreateDishComponents(request.DishComponents);
                _context.DishComponents.AddRange(dishComponents);
                d.DishComponents = dishComponents;
                _context.Dishes.Add(d);
                await _context.SaveChangesAsync();
                return new ServiceResponse { Success = true, Message = "Dish added successfully!" };
            }
            return new ServiceResponse { Success = false, Message = "Failure" };
        }

        public async Task<ServiceResponse> DeleteDishes(List<int> dishIds)
        {
            var dishes = await _context.Dishes.Where(d => dishIds.Contains(d.Id)).ToListAsync();
            _context.Dishes.RemoveRange(dishes);
            await _context.SaveChangesAsync();
            return new ServiceResponse { Success = true, Message = "Dishes deleted successfully!" };
        }

        public async Task<ServiceResponse<DishDto>> GetDish(int dishId)
        {
            var dish = await _context.Dishes
                .Include(d => d.DishComponents)
                .ThenInclude(dc => dc.Component)
                .FirstOrDefaultAsync(d => d.Id == dishId);
            if (dish != null)
            {
                var result = new DishDto
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Price = dish.Price,
                    Description = dish.Description,
                    Created_at = dish.Created_at,
                    DishComponents = dish.DishComponents.ToDictionary(dc => dc.Component.Name, dc => dc.Quantity)
                };
                return new ServiceResponse<DishDto> { Data = result, Success = true, Message = "Success" };
            }
            return new ServiceResponse<DishDto> { Success = false, Message = "Failure" };
        }

        public async Task<ServiceResponse<List<DishDto>>> GetDishes()
        {
            var dishes = await _context.Dishes
                .Select(d => new DishDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Price = d.Price,
                    Description = d.Description,
                    Created_at = d.Created_at
                })
                .ToListAsync();
            return new ServiceResponse<List<DishDto>> { Data = dishes, Success = true, Message = "Success" };
        }

        private async Task<List<DishComponent>> CreateDishComponents(Dictionary<int, decimal> dict)
        {
            var dishComponents = new List<DishComponent>();
            foreach ( var elem in dict)
            {
                var componentId = elem.Key;
                var quantity = elem.Value;
                var component = await _context.Components.FirstOrDefaultAsync(c => c.Id == componentId);
                if(component != null)
                {
                    dishComponents.Add(new DishComponent
                        {
                            ComponentId = componentId,
                            Quantity = quantity,
                            Component = component
                        });
                }
            }
            return dishComponents;
        }
    }
}
