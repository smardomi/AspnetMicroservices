using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class ProductModel : PageModel
    {

        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var productList = await _catalogService.GetCatalog();
            CategoryList = productList.Select(a=>a.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList =  productList.Where(a=>a.Category == categoryName);
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = productList;
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