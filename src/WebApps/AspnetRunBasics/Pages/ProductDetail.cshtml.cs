using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductDetailModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _catalogService.GetCatalog(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogService.GetCatalog(productId);

            var username = "saeed";
            var basket = await _basketService.GetBasket(username);

            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                Color = "Red",
                Price = product.Price,
                ProductName = product.Name,
                Quantity = 1
            });

            await _basketService.UpdateBasket(basket);

            return RedirectToPage("Cart");
        }
    }
}