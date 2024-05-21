
using API.BikeStores.Contracts;
using API.BikeStores.Models;
using API.BikeStores.Services;
using Microsoft.AspNetCore.Http.Metadata;

namespace API.BikeStores.Managers
{
	public class OrderManager:IOrderManager
	{
		private readonly IOrderService _orderService;

		public OrderManager(IOrderService OrderService)
		{
			_orderService = OrderService;
		}

		public OrderResponse GetAllOrder()
		{
			var lstOrder = _orderService.GetAllOrders();
			BikeStores.Contracts.OrderResponse orderResponse = new OrderResponse();
			orderResponse.Orders = lstOrder.Select(c => new BikeStores.Contracts.Order
			{
				Order_id = (int)c.Order_id,
				Customer_id = (int)c.Customer_id,
				Order_Status = (byte)c.Order_Status,
				Order_date = (DateTime)c.Order_date,
				Required_date = (DateTime)c.Required_date,
				Shipped_date = c.Shipped_date.HasValue ? c.Shipped_date.Value : null,
			
				Store_id = (int)c.Store_id,
				Staff_id = (int)c.Staff_id
			});

			return orderResponse;

		}

		public Contracts.Order GetOrderById(int OrderId)
		{
			Models.Orders order = _orderService.GetOrderById(OrderId);

			Contracts.Order OrderResponse = new Contracts.Order();

			{
				//OrderResponse.Order_id = Order_id;
				OrderResponse.Customer_id = order.Customer_id;
				OrderResponse.Order_Status = order.Order_Status;
				OrderResponse.Order_date = order.Order_date;
				OrderResponse.Store_id = order.Store_id;
				OrderResponse.Shipped_date = order.Shipped_date;
				OrderResponse.Staff_id = order.Staff_id;
				OrderResponse.Required_date = order.Required_date;
			}
			return OrderResponse;
		}

		public bool InsertOrder(OrderRequest request)
		{
			InsertOrderModel insertorder = new InsertOrderModel();
			insertorder.Customer_id = request.Customer_id;
			insertorder.Order_date = request.Order_date;
			insertorder.Order_Status = request.Order_Status;
			insertorder.Store_id = request.Store_id;
			insertorder.Staff_id = request.Staff_id;
			insertorder.Shipped_date = request.Shipped_date;
			insertorder.Required_date = request.Required_date;
			bool response = _orderService.InsertOrder(insertorder);
			return response;
		}

		public bool DeleteOrder(int orderid)
		{
			bool response = _orderService.DeleteOrder(orderid);
			return response;
		}

		public bool UpdateOrder(int orderid, OrderRequest request)
		{

			InsertOrderModel updateorder = new InsertOrderModel();
			updateorder.Customer_id = request.Customer_id;
			updateorder.Order_date = request.Order_date; 
			updateorder.Order_Status = request.Order_Status;
			updateorder.Store_id = request.Store_id;
			updateorder.Required_date = request.Required_date;
			updateorder.Staff_id= request.Staff_id;
			updateorder.Shipped_date = request.Shipped_date;
			bool response = _orderService.UpdateOrder(orderid, updateorder);
			return response;
		}
		
	}
}
