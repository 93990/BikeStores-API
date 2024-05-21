using API.Pitstop.Products.Controllers;
using API.Pitstop.Products.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace API.Pitstop.Products.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly Mock<IProductsManager> _mockProductsManager;
        private readonly Mock<IStudentsManager> _mockStudentsManager;
        private readonly ProductsController _productsController;

        public ProductsControllerTests()
        {
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockProductsManager = new Mock<IProductsManager>();
            _mockStudentsManager = new Mock<IStudentsManager>();

            _productsController = new ProductsController(_mockLogger.Object, _mockProductsManager.Object, _mockStudentsManager.Object);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void GetProducts_Success()
        {
            Contracts.ProductsResponse productResponse = new Contracts.ProductsResponse()
            {
                Products = new Contracts.Product[] {
                    new Contracts.Product() { ProductCode = "Prod1", ProductName = "Product 1" }
                    }
            };

            _mockProductsManager.Setup(mgr => mgr.GetAll())
                .Returns(productResponse);

            var response = _productsController.Get()?.Result;
            var returnValue = (response as OkObjectResult)?.Value;

            Assert.That(response, !Is.EqualTo(null));
            Assert.That((response as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            Assert.That((returnValue as Contracts.ProductsResponse)?.Products?.Count(), Is.EqualTo(1));
        }
    }
}