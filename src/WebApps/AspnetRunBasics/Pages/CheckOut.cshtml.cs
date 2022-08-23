using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class CheckOutModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        public CheckOutModel(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }


        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketService.GetBasket("saeed");
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var username = "saeed";
            Cart = await _basketService.GetBasket(username);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = username;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.CheckoutBasket(Order);
                        
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}