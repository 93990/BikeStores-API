using Microsoft.AspNetCore.Mvc;
using API.Pitstop.Products.Managers;

namespace API.Pitstop.Products.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:ApiVersion}/")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsManager _productsManager;
        private readonly IStudentsManager _studentsManager;

        public ProductsController(ILogger<ProductsController> logger, IProductsManager productsManager, IStudentsManager studentsManager)
        {
            _logger = logger;
            _productsManager = productsManager;
            _studentsManager = studentsManager;
        }


        /// <summary>
        /// This endpoint returns product
        /// </summary>       
        /// <returns>Returns product</returns>
        /// 
        [HttpGet]
        [Route("products")]
        public ActionResult<Contracts.ProductsResponse> Get()
        {
            _logger.Log(LogLevel.Information, "ProductsController, Get(): Get all products called.");

            var response = _productsManager.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// This endpoint returns students
        /// </summary>       
        /// <returns>Returns students</returns>
        /// 

        [HttpGet]
        [Route("students")]
        public IActionResult GetStudents()
        {
            _logger.Log(LogLevel.Information, "ProductsController, GetStudents(): Get all students called.");

            var response = _studentsManager.GetAllStudents();
            return Ok(response);
        }
    }
}