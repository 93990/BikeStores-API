using API.Pitstop.Products.Managers;
using API.Pitstop.Products.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Pitstop.Products.Tests
{
    public class ProductsManagerTests
    {
        private readonly Mock<IProductsService> _mockProductsService;
        private readonly ProductsManager _ProductsManager;

        public ProductsManagerTests()
        {
            _mockProductsService = new Mock<IProductsService>();

            _ProductsManager = new ProductsManager(_mockProductsService.Object);
        }

        [Test]
        public void GetAll_Success()
        {
            var lstProducts = new List<Models.Product>() {
                new Models.Product() { ProductCode = "Prod1", ProductName = "Product 1" },
                new Models.Product() { ProductCode = "Prod2", ProductName = "Product 3" }
            };

            _mockProductsService.Setup(service => service.GetAll())
                .Returns(lstProducts);

            var response = _ProductsManager.GetAll();

            Assert.That(response, !Is.EqualTo(null));
            Assert.That(response.Products?.Count(), Is.EqualTo(2));
        }
    }
}