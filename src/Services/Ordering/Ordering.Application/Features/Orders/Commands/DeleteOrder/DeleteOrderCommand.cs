
using MediatR;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}
