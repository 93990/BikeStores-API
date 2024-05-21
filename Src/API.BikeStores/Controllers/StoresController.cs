using API.BikeStores.Contracts;
using API.BikeStores.Managers;
using Microsoft.AspNetCore.Mvc;

namespace API.BikeStores.Controllers
{
	[ApiVersion("1")]
	[Route("api/v{version:ApiVersion}/")]
	[ApiController]
	public class StoresController : ControllerBase
		{
			private readonly ILogger<StoresController> _logger;
			private readonly IStoresManager _storesManager;

			public StoresController(ILogger<StoresController> logger, IStoresManager storesManager)
			{
				_logger = logger;
				_storesManager = storesManager;
			}
			[HttpGet]
			[Route("Stores")]
			public ActionResult<StoresResponse> GetALlCustomer()
			{
				_logger.Log(LogLevel.Information, "CustomersController, Get(): Get all Customers called.");

				var Response = _storesManager.GetAllStores();
				return Ok(Response);
			}
		}		
	
}
