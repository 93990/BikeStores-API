using API.BikeStores.Contracts;
using API.BikeStores.Models;
using API.BikeStores.Services;
using Microsoft.AspNetCore.Http.Metadata;

namespace API.BikeStores.Managers
{
	public class BikeStoresManager : IBikeStoresManager
	{
		private readonly IBikeStoresService _bikeStoresService;

		public BikeStoresManager(IBikeStoresService bikeStoresService)
		{
			_bikeStoresService = bikeStoresService;
		}

		public CustomersResponse GetAllCustomers()
		{
			var lstCustomers = _bikeStoresService.GetAllCustomers();
			Contracts.CustomersResponse customersResponse = new CustomersResponse();
			customersResponse.Customers = lstCustomers.Select(c => new Contracts.Customers
			{
				CustomerId = c.CustomerId,
				FirstName = c.FirstName,
				LastName = c.LastName,
				PhoneNo = c.PhoneNo,
				State = c.State,
				Street = c.Street,
				City = c.City,
				ZipCode = c.ZipCode,
				EmailId = c.EmailId,
			});

			return customersResponse;

		}

		public Contracts.Customers GetCustomerById(int customerId)
		{
			Models.Customers customers = _bikeStoresService.GetCustomerById(customerId);
			Contracts.Customers customerResponse = new Contracts.Customers();
			customerResponse.CustomerId = customers.CustomerId;
			customerResponse.FirstName = customers.FirstName;
			customerResponse.LastName = customers.LastName;
			customerResponse.PhoneNo = customers.PhoneNo;
			customerResponse.State = customers.State;
			customerResponse.Street = customers.Street;
			customerResponse.City = customers.City;
			customerResponse.ZipCode = customers.ZipCode;
			customerResponse.EmailId = customers.EmailId;
			return customerResponse;
		}

		public bool InsertCustomer(CustomerRequest request)
		{
			InsertCustomerModel insertCustomer = new InsertCustomerModel();
			insertCustomer.FirstName = request.FirstName;
			insertCustomer.LastName = request.LastName;
			insertCustomer.PhoneNo = request.PhoneNo;
			insertCustomer.City = request.City;
			insertCustomer.State = request.State;
			insertCustomer.EmailId = request.EmailId;
			insertCustomer.Street = request.Street;
			insertCustomer.ZipCode = request.ZipCode;
			bool response = _bikeStoresService.InsertCustomers(insertCustomer);
			return response;
		}

		public bool DeleteCustomer(int customerId)
		{
			bool response = _bikeStoresService.DeleteCustomer(customerId);
			return response;
		}

		public bool UpdateCustomer(int customerId , CustomerRequest customerRequest )

		{
			InsertCustomerModel updateCustomer = new InsertCustomerModel();
			updateCustomer.FirstName = customerRequest.FirstName;
			updateCustomer.LastName = customerRequest.LastName;
			updateCustomer.PhoneNo = customerRequest.PhoneNo;
			updateCustomer.City = customerRequest.City;
			updateCustomer.Street = customerRequest.Street;
			updateCustomer.State = customerRequest.State;
			updateCustomer.EmailId = customerRequest.EmailId;
			updateCustomer.ZipCode = customerRequest.ZipCode;
			bool response = _bikeStoresService.UpdateCustomer(customerId, updateCustomer);
			return response;

		}
	}
}