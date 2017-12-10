using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Domain;
using SportsStore.Filters;

namespace SportsStore.Controllers
{
    [ServiceFilter(typeof(CartSessionFilter))]
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index(Cart cart)
        {
            if (cart.IsEmpty)
                return View("EmptyCart");
            ViewData["Total"] = cart.TotalValue;
            return View(cart.CartLines);
        }
    }
}