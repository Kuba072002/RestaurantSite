using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;
using RestaurantApi.ModelsDto.Wholesaler;
using RestaurantApi.ModelsDto.Raports.Wholesaler;
using RestaurantApi.Utility;
using System.Collections.Generic;

namespace RestaurantApi.Services.WholesalerService
{
    public class WholesalerService : IWholesalerService
    {
        private readonly DataContext _context;

        public WholesalerService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> AddWholesaler(AddWholesalerDto request)
        {
            if (!await _context.Wholesalers.AnyAsync(d => d.Name.Equals(request.Name)))
            {
                var wholesaler = new Wholesaler()
                {
                    Name = request.Name,
                    Address = request.Address,
                };
                _context.Wholesalers.Add(wholesaler);
                await _context.SaveChangesAsync();
                return new ServiceResponse { Success = true, Message = "Wholesaler added successfully!" };
            }
            return new ServiceResponse { Success = false, Message = "Failure" };
        }

        public async Task<ServiceResponse<List<WholesalerDto>>> GetWholesalers()
        {
            var wholesalers = await _context.Wholesalers
                .Select(w => new WholesalerDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Address = w.Address
                })
                .ToListAsync();
            return new ServiceResponse<List<WholesalerDto>> { Data = wholesalers, Success = true, Message = "Success" };
        }

        public async Task<ServiceResponse<List<WholesalerRaportDto>>> GetWholesalerRaport(RequestWholesalerRaportDto request)
        {
            var wholesalerOrders = await _context.WholesalerOrders
                .Include(wo => wo.ComponentOrders)
                    .ThenInclude(co => co.Component)
                .Where(w => w.WholesalerId == request.WholesalerId
                    && string.Compare(w.OrderDate, request.StartDate) >= 0 
                    && string.Compare(w.OrderDate, request.EndDate) <= 0)
                .ToListAsync();
            var raport = new List<WholesalerRaportDto>();
            if (wholesalerOrders.Count > 0)
            {
                foreach (var wOrder in wholesalerOrders)
                {
                    foreach(var cOrder in wOrder.ComponentOrders)
                    {
                        var elem = raport.FirstOrDefault(r => r.Id == cOrder.ComponentId);
                        if(elem != null)
                        {
                            elem.Quantity += cOrder.Quantity;
                        }
                        else
                        {
                            raport.Add(new WholesalerRaportDto
                            {
                                Id= cOrder.ComponentId,
                                Quantity = cOrder.Quantity,
                                ComponentName = cOrder.Component.Name,
                                Unit = cOrder.Component.Unit,
                            });
                        }
                    }
                }
            }
            return new ServiceResponse<List<WholesalerRaportDto>> { Data = raport, Success = true, Message = "Success" };
        }
    }
}
