using API.BikeStores.Models;
using Azure.Core;

namespace API.BikeStores.Services
{
	public interface IBikeStoresService
	{
		IEnumerable<Customers> GetAllCustomers();
		Customers GetCustomerById(int customerId);
		bool InsertCustomers(InsertCustomerModel modelrequest);

		bool DeleteCustomer(int customerId);

		bool UpdateCustomer(int customerId, InsertCustomerModel updateCustomer);

	}

}
