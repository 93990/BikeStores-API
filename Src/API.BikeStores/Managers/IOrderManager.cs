using API.BikeStores.Contracts;
//using API.BikeStores.Models;

namespace API.BikeStores.Managers
{
	public interface IOrderManager
	{
		OrderResponse GetAllOrder();

		Order GetOrderById(int OrderId);

		bool InsertOrder(OrderRequest request);

		bool DeleteOrder(int orderid);

		bool UpdateOrder(int orderid , OrderRequest request);
	}
}
