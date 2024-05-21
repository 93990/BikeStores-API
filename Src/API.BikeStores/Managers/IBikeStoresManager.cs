using API.BikeStores.Contracts;

namespace API.BikeStores.Managers
{
	public interface IBikeStoresManager
	{
		CustomersResponse GetAllCustomers();
		Customers GetCustomerById(int customerId);
		bool InsertCustomer(CustomerRequest request);

		bool DeleteCustomer(int customerId);

		bool UpdateCustomer(int customerId , CustomerRequest customerRequest);


	}
}
