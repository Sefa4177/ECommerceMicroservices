using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Application.Mappings;
using ECommerce.Services.Order.Application.Queries;
using ECommerce.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLib.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _orderDbontext;

        public GetOrdersByUserIdQueryHandler(OrderDbContext orderDbontext)
        {
            _orderDbontext = orderDbontext;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbontext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

            if(!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }
            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, 200);
        }
    }
}
