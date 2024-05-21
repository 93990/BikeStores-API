using API.Pitstop.Products;
using Microsoft.Data.SqlClient;

namespace API.BikeStores.Services
{
	public class StoresService:IStoresService	
	{

		private readonly IConfiguration _configuration;
		private readonly string _sqlConnectionString;
		public StoresService(IConfiguration configuration)
		{
			_configuration = configuration;
			_sqlConnectionString = configuration["PitstopSqlConnectionString"];
		}
		public IEnumerable<Models.Stores> GetAllStores()
		{
			var store = new List<Models.Stores>();
			SqlConnection sql;
			using(sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spGetAllStores, sql);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					store.Add(new Models.Stores
					{
						StoreId = (int)reader["store_id"],
						StoreName = reader["store_name"].ToString(),
						State = reader["state"].ToString(),
						Street = reader["street"].ToString(),
						Email = reader["email"].ToString(),
						PhoneNo = reader["phone_no"]!= DBNull.Value ? (int)reader["phone_no"] : 0,

						ZipCode = reader["zip_code"].ToString()

					}) ;
					
				}
				sql.Close();
				return store;

			}
		}

	}
}
