namespace API.Pitstop.Products.Services
{
    public interface IProductsService
    {
        IEnumerable<Models.Product> GetAll();
    }
}
