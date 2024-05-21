using API.BikeStores.Contracts;
using API.BikeStores.Models;

namespace API.BikeStores.Services
{
	public interface IOrderService
	{
		IEnumerable<Orders> GetAllOrders();

		Orders GetOrderById(int OrderId);

		bool InsertOrder(InsertOrderModel insertorder);

		bool DeleteOrder(int orderid);

		bool UpdateOrder(int orderid , InsertOrderModel updateorder);
	}
}
