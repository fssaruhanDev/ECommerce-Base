using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Events.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Queries.Order.GetOrderQuery;

public class GetOrderQueryHandle : IRequestHandler<GetOrderQuery, List<GetOrdersViewModel>>
{

    private readonly IOrderRepository orderRepository;

    public GetOrderQueryHandle(IOrderRepository orderRepository )
    {
        this.orderRepository = orderRepository;
    }


    public async Task<List<GetOrdersViewModel>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {

        var order = orderRepository.AsQueryable();

        order = order
            .Include(i => i.User)
            .Include(i => i.CartItems)
                .ThenInclude(i => i.Product)
            .Include(i => i.CartItems)
                .ThenInclude(i => i.ShoppingCart);

        var orders = order.Where(x => x.UserID == request.UserID);


        orders = orders.Where(x => x.CartItems.Any(ci => ci.ShoppingCartID == null));


        var groupedOrders = orders
            .GroupBy(i => i.CartItems.FirstOrDefault().OrderId)
            .Select(group => new GetOrdersViewModel
            {
                ShoppingCartID = group.Key,
                Items = group.SelectMany(i => i.CartItems)
                             .Select(x => new OrderCartItems
                             {
                                 Id = x.ID,
                                 Name = x.Product.Name,
                                 Picture = x.Product.Picture,
                                 Price = x.Product.Price,
                                 Quantity = x.Quantity,
                                 OrderID = x.Order.ID,
                                 Size = x.Product.Size,
                                 TotalPrice = x.Quantity * x.Product.Price
                             })
                             .ToList()
            })
            .ToList();

        // Toplam fiyatı hesapla
        foreach (var group in groupedOrders)
        {
            group.TotalPrice = group.Items.Sum(i => i.TotalPrice);
        }

        groupedOrders.RemoveAll(x => x.Items.Count == 0);

        return groupedOrders;


    }
}
