using API.BikeStores.Models;
using API.Pitstop.Products;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace API.BikeStores.Services
{
	public class BikeStoresService : IBikeStoresService

	{
		private readonly IConfiguration _configuration;
		private readonly string _sqlConnectionString;
		public BikeStoresService(IConfiguration configuration)
		{	
			_configuration = configuration;
			_sqlConnectionString = configuration["PitstopSqlConnectionString"];
		}
		public IEnumerable<Models.Customers> GetAllCustomers()
		{
			var lstCustomers = new List<Models.Customers>();

			SqlConnection sql;

			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spGetAllCustomers, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					lstCustomers.Add(new Models.Customers
					{
						CustomerId = (int)reader["customer_id"],
						FirstName = reader["first_name"].ToString(),
						LastName = reader["last_name"].ToString(),
						PhoneNo = reader["phone"].ToString(),
						City = reader["city"].ToString(),
						EmailId = reader["email"].ToString(),
						State = reader["state"].ToString(),
						ZipCode = reader["zip_code"].ToString(),
						Street = reader["street"].ToString()
					});


				}
				sql.Close();

			}
			return lstCustomers;	
		}

		public Models.Customers GetCustomerById(int customerId)
		{
			Customers customer = new Customers();
			

			SqlConnection sql;
			using(sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spGetCustomerById, sql);
				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.Int).Value= customerId;
				var reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					customer.CustomerId = (int)reader["customer_id"];
					customer.FirstName = reader["first_name"].ToString();
					customer.LastName = reader["last_name"].ToString();
					customer.PhoneNo = reader["phone"].ToString();
					customer.City = reader["city"].ToString();
					customer.EmailId = reader["email"].ToString();
					customer.Street = reader["street"].ToString();
					customer.ZipCode = reader["zip_code"].ToString();
					customer.State = reader["state"].ToString();
				}
				sql.Close();
			}
			return customer;
		}

		public bool InsertCustomers(InsertCustomerModel modelrequest)
		{
			int nocustomerpresent = 0;
			SqlConnection sql;
			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spInsertCustomer, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@FirstName", SqlDbType.Structured).Value = modelrequest.FirstName; //Addwithvalue jo parameter sp expect kar ra hai wo provide karti hai
				cmd.Parameters.AddWithValue("@LastName", SqlDbType.Structured).Value = modelrequest.LastName;
				cmd.Parameters.AddWithValue("@Phone", SqlDbType.Structured).Value = modelrequest.PhoneNo;
				cmd.Parameters.AddWithValue("@Email", SqlDbType.Structured).Value = modelrequest.EmailId;
				cmd.Parameters.AddWithValue("@Street", SqlDbType.Structured).Value = modelrequest.Street;
				cmd.Parameters.AddWithValue("@City", SqlDbType.Structured).Value = modelrequest.City;
				cmd.Parameters.AddWithValue("@State", SqlDbType.Structured).Value = modelrequest.State;
				cmd.Parameters.AddWithValue("@ZipCode", SqlDbType.Structured).Value = modelrequest.ZipCode;
				cmd.ExecuteNonQuery(); //When we work with post jo query likhi hai use execute karate hai
				sql.Close();
				return true;
			}
			
		}

		public bool DeleteCustomer(int customerId)
		{
			SqlConnection sql;
			using(sql = new SqlConnection(_sqlConnectionString))
			{
				
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spDeleteByCustomerId, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@CustomerId" , SqlDbType.Structured).Value = customerId;
				cmd.Parameters.Add("@UpdateRowCount",SqlDbType.Int);
				cmd.Parameters["@UpdateRowCount"].Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				int updaterow = (int)cmd.Parameters["@UpdateRowCount"].Value;
				sql.Close();
				return updaterow > 0;
			}
		}

		public bool UpdateCustomer(int customerId , InsertCustomerModel updateCustomer )
		{
			//Customers customer = new Customers();

			Models.Customers customers = new Models.Customers();
			customers = GetCustomerById(customerId);
			if(updateCustomer.FirstName == null ) 
			{
				updateCustomer.FirstName = customers.FirstName;
			}

			
			SqlConnection sql;
			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spUpdateCustomerById , sql );
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.Structured).Value = customerId;
				cmd.Parameters.AddWithValue("@FirstName", SqlDbType.Structured).Value = updateCustomer.FirstName; //Addwithvalue jo parameter sp expect kar ra hai wo provide karti hai
				cmd.Parameters.AddWithValue("@LastName", SqlDbType.Structured).Value = string.IsNullOrEmpty(updateCustomer.LastName) ? customers.LastName : updateCustomer.LastName;
				cmd.Parameters.AddWithValue("@Phone", SqlDbType.Structured).Value = updateCustomer.PhoneNo == null ?   customers.PhoneNo : updateCustomer.PhoneNo;
				cmd.Parameters.AddWithValue("@Email", SqlDbType.Structured).Value = string.IsNullOrEmpty(updateCustomer.EmailId) ? customers.EmailId : updateCustomer.EmailId; 
				cmd.Parameters.AddWithValue("@Street", SqlDbType.Structured).Value =string.IsNullOrEmpty(updateCustomer.Street) ? customers.Street : updateCustomer.Street;
				cmd.Parameters.AddWithValue("@City", SqlDbType.Structured).Value = updateCustomer.City == null ? customers.City : updateCustomer.City;
				cmd.Parameters.AddWithValue("@State", SqlDbType.Structured).Value = updateCustomer.State == null ? customers.State : updateCustomer.State;
				cmd.Parameters.AddWithValue("@ZipCode", SqlDbType.Structured).Value = updateCustomer.ZipCode == null ? customers.ZipCode : updateCustomer.ZipCode;

				//	cmd.Parameters.Add("@UpdateRowCount", SqlDbType.Int);

				cmd.ExecuteNonQuery();
				sql.Close();

				return true;

			}
		}
	}
}
