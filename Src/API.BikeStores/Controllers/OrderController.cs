
using API.BikeStores.Contracts;
using API.BikeStores.Managers;
using API.BikeStores.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.BikeStores.Controllers
{
	
		[ApiVersion("1")]
		[Route("api/v{version:ApiVersion}/")]
		[ApiController]

		public class OrderController : ControllerBase
		{
			private readonly ILogger<OrderController> _logger;
			private readonly IOrderManager _orderManager;

			public OrderController(ILogger<OrderController> logger,IOrderManager orderManager)
			{
				_logger = logger;
				_orderManager =orderManager;
			}

		//public class BikeStoresController : ControllerBase
		//{
		//	private readonly ILogger<BikeStoresController> _logger;
		//	private readonly IBikeStoresManager _bikestoresManager;
		//'API.BikeStores.Managers.IOrderManager' while attempting to activate 'API.BikeStores.Controllers.OrderController'.

		//	public BikeStoresController(ILogger<BikeStoresController> logger, IBikeStoresManager bikeStoresManager)
		//	{
		//		_logger = logger;
		//		_bikestoresManager = bikeStoresManager;
		//	}
		/// <summary>
		/// GetAllCustomers
		/// </summary>
		/// <returns>Returns all customers details</returns>

		[HttpGet]
			[Route("order")]
			public ActionResult<OrderResponse> GetAllOrder()
			{
				_logger.Log(LogLevel.Information, "OrderController, Get(): Get all order called.");

				var response = _orderManager.GetAllOrder();
				return Ok(response);
			}
		[HttpGet]
		[Route("order/{OrderId}")]
		public ActionResult<Orders> GetOrderById(int OrderId)
		{
			_logger.Log(LogLevel.Information, "OrderController, Get(): Get  orderbyid called.");
			var response = _orderManager.GetOrderById(OrderId);
			if(response == null)
			{
				return StatusCode(404, "No customer available");
			}
			return Ok(response);

		}
		[HttpPost]
		[Route("order")]
		public IActionResult InsertOrder([FromBody] OrderRequest request )
		{

			_logger.Log(LogLevel.Information, "OrderController, Post(): Insert Order called.");
			bool response = _orderManager.InsertOrder(request);
			return Ok(response);
		}
		[HttpDelete]
		[Route("order/{orderid}")]
		public IActionResult DeleteOrder(int orderid)
		{
			_logger.Log(LogLevel.Information,"OrderController, Delete(): Delete order called.");
			bool response = _orderManager.DeleteOrder(orderid);
			return Ok(response);
		}

		[HttpPut]
		[Route("order")]
		public IActionResult UpdateOrder (int orderid , [FromBody] OrderRequest order)
		{
			_logger.Log(LogLevel.Information, "OrderController, Put(): Update Order Calledd");
			bool response = _orderManager.UpdateOrder(orderid , order);
			return Ok(response);
		}
	}
}
