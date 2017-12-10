using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Domain;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var cart = ReadCartFromSession();
            if (cart.IsEmpty)
                return View("EmptyCart");
            ViewData["Total"] = cart.TotalValue;
            return View(cart.CartLines);
        }

        private Cart ReadCartFromSession()
        {
            Cart cart = HttpContext.Session.GetString("cart") == null ?
                new Cart() : JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("cart"));
            foreach (var l in cart.CartLines)
                l.Product = _productRepository.GetById(l.Product.ProductId);
            return cart;
        }

        private void WriteCartToSession(Cart cart)
        {
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
        }
    }
}