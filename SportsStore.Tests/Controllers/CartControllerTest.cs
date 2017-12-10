using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models.Domain;
using SportsStore.Tests.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests.Controllers
{
    public class CartControllerTest
    {
        private readonly CartController _controller;
        private readonly Cart _cart;

        public CartControllerTest()
        {
            var context = new DummyApplicationDbContext();

            var productRepository = new Mock<IProductRepository>();

            _controller = new CartController(productRepository.Object);
            _cart = new Cart();
            _cart.AddLine(context.Football, 2);
        }

        #region Index
        [Fact]
        public void Index_EmptyCart_ShowsEmptyCartView()
        {
            var emptycart = new Cart();
            var result = _controller.Index(emptycart) as ViewResult;
            Assert.Equal("EmptyCart", result?.ViewName);
        }

        [Fact]
        public void Index_NonEmptyCart_PassesCartLinesToViewViaModel()
        {
            var result = _controller.Index(_cart) as ViewResult;
            var cartresult = result?.Model as IEnumerable<CartLine>;
            Assert.Equal(1, cartresult?.Count());
        }

        [Fact]
        public void Index_NonEmptyCart_StoresTotalInViewData()
        {
            var result = _controller.Index(_cart) as ViewResult;
            Assert.Equal(50M, result.ViewData["Total"]);
        }
        #endregion
    }
}