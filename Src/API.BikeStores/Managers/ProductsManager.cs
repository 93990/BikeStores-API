using API.Pitstop.Products.Controllers;
using API.Pitstop.Products.Services;

namespace API.Pitstop.Products.Managers
{
    public class ProductsManager : IProductsManager
    {
        private readonly IProductsService _productsService;

        public ProductsManager(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public Contracts.ProductsResponse GetAll()
        {
            // var lstProducts = _productsService.GetAll();

            Models.Product product = new Models.Product();
            Models.Product productsec = new Models.Product();


            List<Models.Product> lstProducts = new List<Models.Product>();

            product.ProductId = 1;
            product.ProductCode = "P01";
            product.ProductName = "Electronics";
            lstProducts.Add(product);
            //lstProducts.Add(new Models.Product(2, "P02", "Semiconductor"));

            productsec.ProductId = 2;
            productsec.ProductCode = "P02";
            productsec.ProductName = "Semiconductor";
            lstProducts.Add(productsec);

            Contracts.ProductsResponse productResponse = new Contracts.ProductsResponse() { };
            productResponse.Products = lstProducts.Select(p => new Contracts.Product()
            {
                ProductCode = p.ProductCode,
                ProductName = p.ProductName
            });

            return productResponse;
        }

    }
}
