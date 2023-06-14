using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;
using RestaurantApi.ModelsDto.Component;
using RestaurantApi.ModelsDto.Dish;
using RestaurantApi.ModelsDto.Raports.Component;
using RestaurantApi.ModelsDto.Raports.Order;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.ComponentService
{
    public class ComponentService : IComponentService
    {
        private readonly DataContext _context;

        public ComponentService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddComponent(AddComponentDto request)
        {
            if (!await _context.Components.AnyAsync(c => c.Name.Equals(request.Name)))
            {
                var component = new Component()
                {
                    Name = request.Name,
                    FreshnessTime = request.FreshnessTime,
                    Unit = request.Unit,
                };
                _context.Components.Add(component);
                await _context.SaveChangesAsync();
                return new ServiceResponse { Success = true, Message = "Component added successfully!" };
            }
            return new ServiceResponse { Success = false, Message = "Failure" };
        }

        public async Task<ServiceResponse<List<ComponentDto>>> GetComponents()
        {
            var components = await _context.Components
                .Select(c => new ComponentDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    FreshnessTime = c.FreshnessTime,
                    Unit= c.Unit,
                })
                .ToListAsync();
            return new ServiceResponse<List<ComponentDto>> { Data = components, Success = true, Message = "Success" };
        }

        public async Task<ServiceResponse<Dictionary<ComponentDto, decimal>>> GetComponentsQuantities(RequestOrderRaportDto request)
        {
            var componentQuantities = await _context.ComponentQunatities
                .Where(cq =>
                    string.Compare(cq.FreshnessDate, request.StartDate) >= 0 &&
                    string.Compare(cq.FreshnessDate, request.EndDate) < 0 &&
                    cq.Quantity > 0)
                .ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ComponentRaportDto>> GetComponentsRaport(RequestOrderRaportDto request)
        {
            var orders = await _context.Orders
                .Where(o => 
                    string.Compare(o.OrderDate, request.StartDate) >= 0 && 
                    string.Compare(o.OrderDate, request.EndDate) <= 0)
                .Include(o => o.Dishes)
                    .ThenInclude(d => d.DishComponents)
                        .ThenInclude(dc => dc.Component)
                .ToListAsync();
            var componentQuantities = await _context.ComponentQunatities
                .Where(cq =>
                    string.Compare(cq.FreshnessDate, request.StartDate) >= 0 &&
                    string.Compare(cq.FreshnessDate, request.EndDate) < 0 &&
                    cq.Quantity > 0)
                .ToListAsync();
            //to mowi ile uzylem danego skladnika na podstawie zamowien
            var usedComponentsNumber = new Dictionary<ComponentDto, List<decimal>>();
            foreach(var order in orders)
            {
                foreach(var dish in order.Dishes)
                {
                    foreach(var dishComponent in dish.DishComponents)
                    {
                        var componentDto = usedComponentsNumber.Keys
                            .FirstOrDefault(c => c.Id == dishComponent.ComponentId);
                        if (componentDto != null)
                        {
                            usedComponentsNumber[componentDto][0] += dishComponent.Quantity;
                        }
                        else
                        {
                            usedComponentsNumber.Add(new ComponentDto 
                                { Id = dishComponent.ComponentId, 
                                Name = dishComponent.Component.Name,
                                FreshnessTime = dishComponent.Component.FreshnessTime,
                                Unit = dishComponent.Component.Unit,
                                }, 
                                new List<decimal>{ dishComponent.Quantity, 0});
                        }
                    }
                }
            }
            //to mowi ile zostalo nie swiezych skladnikow w tym czasie 
            foreach (var componentQuantity in componentQuantities)
            {
                var componentDto = usedComponentsNumber.Keys
                            .FirstOrDefault(c => c.Id == componentQuantity.ComponentId);
                if (componentDto != null)
                {
                    usedComponentsNumber[componentDto][1] += componentQuantity.Quantity;
                }
                else
                {
                    usedComponentsNumber.Add(new ComponentDto
                        {
                            Id = componentQuantity.ComponentId,
                            Name = componentQuantity.Component.Name,
                            FreshnessTime = componentQuantity.Component.FreshnessTime,
                            Unit = componentQuantity.Component.Unit,
                        },
                        new List<decimal> { 0, componentQuantity.Quantity });
                }
            }
            var result = new ComponentRaportDto { ComponentQuantityDict = usedComponentsNumber };
            return new ServiceResponse<ComponentRaportDto> { Data = result, Success = true, Message = "Failure" };
        }

        //private async Task<Dictionary<int,int>> GetDictOfUsedComponentsAndQuantity()
        //{

        //}
    }
}
