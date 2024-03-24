using ECommerce.Services.Order.Application.Commands;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Domain.OrderAggregate;
using ECommerce.Services.Order.Infrastructure;
using MediatR;
using SharedLib.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.Line, request.Address.ZipCode);

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);

            request.OrderItems.ForEach(item =>
            {
                newOrder.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.PictureUrl);
            });

            await _orderDbContext.AddAsync(newOrder);

            await _orderDbContext.SaveChangesAsync();
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);

        }
    }
}
