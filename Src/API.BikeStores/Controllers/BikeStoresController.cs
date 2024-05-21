using API.BikeStores.Contracts;
using API.BikeStores.Managers;
using API.Pitstop.Products.Controllers;
using API.Pitstop.Products.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API.BikeStores.Controllers
{
	//[Route("api/[controller]")]
	[ApiVersion("1")]
	[Route("api/v{version:ApiVersion}/")]
	[ApiController]

	public class BikeStoresController : ControllerBase
	{
		private readonly ILogger<BikeStoresController> _logger;
		private readonly IBikeStoresManager _bikestoresManager;

		public BikeStoresController(ILogger<BikeStoresController> logger, IBikeStoresManager bikeStoresManager )
		{
			_logger = logger;
			_bikestoresManager = bikeStoresManager;
		}

		/// <summary>
		/// GetAllCustomers
		/// </summary>
		/// <returns>Returns all customers details</returns>
		
		[HttpGet]
		[Route("customers")]
		public ActionResult<CustomersResponse> GetAllCustomers()
		{
			_logger.Log(LogLevel.Information, "CustomersController, Get(): Get all Customers called.");

			var response = _bikestoresManager.GetAllCustomers();
			return Ok(response);
		}

		///<summary>
		///GetCustomerById
		///</summary>
		///<param name="customerId"></param>
		///<return>returns cutomer data</return>
		///<remarks>This endpoint returns customer data by id</remarks>

		[HttpGet]
		[Route("customers/{customerId}")]
		
		public ActionResult<Customers> GetCustomerById(int customerId)
		{
			_logger.Log(LogLevel.Information, "CustomersController, Get(): Get customer By Id called");

			var response = _bikestoresManager.GetCustomerById(customerId);
			if(response.CustomerId == 0)
			{
				return StatusCode(404, "Customer Id not Found");
			}
			return Ok(response);
		}

		[HttpPost]
		[Route("customers")]

		public IActionResult InsertCustomer([FromBody] CustomerRequest request)
		{
			_logger.Log(LogLevel.Information, "CustomersController, Post(): Insert customer called");
			bool response = _bikestoresManager.InsertCustomer(request);
			return Ok(response);
		}
		[HttpDelete]
		[Route("Customers/{customerId}")]
		public IActionResult DeleteCustomer(int customerId)
		{
			_logger.Log(LogLevel.Information, "CustomersController, Post(): Insert customer called");
			bool response = (_bikestoresManager.DeleteCustomer(customerId));
			if (response == false)
			{
				return StatusCode(404, "CustomerId not found");
			}
			return Ok(response);


		}
		[HttpPut]
		[Route("Customers/{customerId}")]

		public ActionResult UpdateCustomer(int customerId, [FromBody] CustomerRequest customerRequest)
		{
			_logger.Log(LogLevel.Information, "BikeStoresController, Put(): Update customer by id called");
			bool response = _bikestoresManager.UpdateCustomer(customerId , customerRequest);
			return Ok(response);

		}
	}

}
