using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;
using RestaurantApi.ModelsDto.ComponentOrder;
using RestaurantApi.ModelsDto.WholesalerOrder;
using RestaurantApi.Utility;

namespace RestaurantApi.Services.WholesalerOrderService
{
    public class WholesalerOrderService : IWholesalerOrderService
    {
        private readonly DataContext _context;

        public WholesalerOrderService (DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> AddWholesalerOrder(AddWholesalerOrderDto request)
        {
            var wholesaler = await _context.Wholesalers.FirstOrDefaultAsync(w => w.Id == request.WholesalerId);
            if(wholesaler != null)
            {
                var wholesalerOrder = new WholesalerOrder
                {
                    WholesalerId = request.WholesalerId,
                    Wholesaler = wholesaler,
                    OrderDate = DateTime.Now.ToString("yyyy-MM-dd")
                };
                var componentData = await CreateComponentData(request.ComponentOrders);
                _context.ComponentOrders.AddRange(componentData.Item1);
                _context.ComponentQunatities.AddRange(componentData.Item2);
                wholesalerOrder.ComponentOrders = componentData.Item1;
                _context.WholesalerOrders.Add(wholesalerOrder);
                await _context.SaveChangesAsync();
                return new ServiceResponse { Success = true, Message = "WholesalerOrder added successfully!" };
            }
            return new ServiceResponse { Success = false, Message = "Failure" };

        }

        private async Task<(List<ComponentOrder>,List<ComponentQunatity>)> CreateComponentData(Dictionary<int, decimal> dictionary)
        {
            var componentOrders = new List<ComponentOrder>();
            var componentQuantities = new List<ComponentQunatity>();
            foreach (var elem in dictionary)
            {
                var componentId = elem.Key;
                var quantity = elem.Value;
                var component = await _context.Components.FirstOrDefaultAsync(c => c.Id == componentId);
                if (component != null)
                {
                    componentOrders.Add(new ComponentOrder
                    {
                        ComponentId = componentId,
                        Quantity = quantity,
                        Component = component,
                        OrderDate = DateTime.Now.ToString("yyyy-MM-dd"),
                        FreshnessDate = DateTime.Now
                            .AddDays(int.Parse(component.FreshnessTime))
                            .ToString("yyyy-MM-dd")
                    });
                    componentQuantities.Add(new ComponentQunatity
                    {
                        ComponentId = componentId,
                        Quantity = quantity,
                        FreshnessDate = DateTime.Now
                            .AddDays(int.Parse(component.FreshnessTime))
                            .ToString("yyyy-MM-dd")
                    });
                }
            }
            return (componentOrders,componentQuantities);
        }

        public async Task<ServiceResponse<List<WholesalerOrderDto>>> GetWholesalerOrders()
        {
            var result = await _context.WholesalerOrders
                .Select(wo => new WholesalerOrderDto
                {
                    Id= wo.Id,
                    WholesalerId= wo.WholesalerId,
                    OrderDate = wo.OrderDate
                })
                .ToListAsync();
            return new ServiceResponse<List<WholesalerOrderDto>> { Data = result, Success = true, Message = "WholesalerOrder added successfully!" };
        }

        public async Task<ServiceResponse<List<WholesalerOrderDto>>> GetWholesalerOrdersByWholesalerId(int wholesalerId)
        {
            var result = await _context.WholesalerOrders
                .Where(wo => wo.WholesalerId == wholesalerId)
                .Select(wo => new WholesalerOrderDto
                {
                    Id = wo.Id,
                    WholesalerId = wo.WholesalerId,
                    OrderDate = wo.OrderDate
                })
                .ToListAsync();
            return new ServiceResponse<List<WholesalerOrderDto>> { Data = result, Success = true, Message = "WholesalerOrder added successfully!" };
        }

        public async Task<ServiceResponse<WholesalerOrderDto>> GetWholesalerOrder(int wholesalerOrderId)
        {
            var wholesalerOrder = await _context.WholesalerOrders
                .Include(wo => wo.ComponentOrders)
                .ThenInclude(co => co.Component)
                .FirstOrDefaultAsync(wo => wo.Id == wholesalerOrderId);
            if(wholesalerOrder != null) 
            {
                var result = new WholesalerOrderDto 
                { 
                    Id = wholesalerOrder.Id,
                    WholesalerId = wholesalerOrder.WholesalerId,
                    OrderDate = wholesalerOrder.OrderDate,
                    ComponentOrders = wholesalerOrder.ComponentOrders
                    .Select(co => new ComponentOrderDto 
                    { 
                      Id = co.Id,
                      ComponentId= co.ComponentId,
                      Quantity= co.Quantity,
                      FreshnessDate= co.FreshnessDate,
                      ComponentName = co.Component.Name
                    }).ToList()
                };

                return new ServiceResponse<WholesalerOrderDto> { Data = result, Success = true, Message = "WholesalerOrder added successfully!" };
            }
            return new ServiceResponse<WholesalerOrderDto> { Success = false, Message = "Failure" };
        }
    }
}
