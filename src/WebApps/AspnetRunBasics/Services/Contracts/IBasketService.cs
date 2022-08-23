using System.Threading.Tasks;
using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services.Contracts
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
        Task<BasketModel> UpdateBasket(BasketModel model);
        Task CheckoutBasket(BasketCheckoutModel model);
    }
}
