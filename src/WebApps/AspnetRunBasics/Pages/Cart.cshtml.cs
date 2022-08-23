using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class CartModel : PageModel
    {
        private IBasketService _basketService;

        public CartModel(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public BasketModel Cart { get; set; } = new BasketModel();        

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketService.GetBasket("saeed");            

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var username = "saeed";
            var basket = await _basketService.GetBasket(username);
            var basketItem = basket.Items.SingleOrDefault(a => a.ProductId == productId);

            basket.Items.Remove(basketItem);
            await _basketService.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}