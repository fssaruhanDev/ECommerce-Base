using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Models.RequestModels.ShoppingCart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.Commands.ShoppingCart;

public class GetBuyShoppingCartCommandHandle : IRequestHandler<GetBuyShoppingCartCommand, Guid>
{

    private readonly IShoppingCartRepository shoppingCartRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetBuyShoppingCartCommandHandle(IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        this.shoppingCartRepository = shoppingCartRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(GetBuyShoppingCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var shoppingCart = await shoppingCartRepository.GetByIdAsync(request.shoppingCartId);
            var value = shoppingCart.ID;
            if (shoppingCart != null)
            {

                // Şimdi ShoppingCart'ı silebilirsin
                await shoppingCartRepository.DeleteAsync(shoppingCart);
            }

            return value;
        }
        catch (Exception)
        {

            throw;
        }



    }
}
