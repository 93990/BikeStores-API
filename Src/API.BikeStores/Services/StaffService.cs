using API.Pitstop.Products;
using Microsoft.Data.SqlClient;
using API.BikeStores.Models;

using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Data.Sql;
using Microsoft.SqlServer.Server;
using Microsoft.Data.SqlTypes;

namespace API.BikeStores.Services
{
	public class StaffService:IStaffService
	{
		private readonly IConfiguration _configuration;
		private readonly string _sqlConnectionString;
		public StaffService(IConfiguration configuration)
		{
			_configuration = configuration;
			_sqlConnectionString = configuration["PitstopSqlConnectionString"];
		}

		public IEnumerable<Models.Staff> GetAllStaff()
		{
			var lstStaff = new List<Models.Staff>();
			SqlConnection sql;
			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spGetAllStaff, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					lstStaff.Add(new Models.Staff
					{
						StaffId = (int)reader["staff_Id"],
						FirstName = reader["first_name"].ToString(),
						LastName = reader["last_name"].ToString(),
						Active = (byte)reader["active"],
						Phone = reader["phone"].ToString(),
						Email = reader["email"].ToString(),

						StoreId = (int)reader["store_id"],
						ManagerId = reader["manager_id"]!= DBNull.Value ? (int?)reader["manager_id"] : null
					});
				}
				sql.Close();
				return lstStaff;
			}


		}

		public Models.Staff GetStaffById(int StaffId)
		{
			Staff staff = new Staff();
				SqlConnection sql;
			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spGetStaffById, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@StaffId", StaffId);
				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					staff.FirstName = reader["first_name"].ToString();
					staff.LastName = reader["last_name"].ToString();
					staff.Active = (byte)reader["active"];
					staff.Email = reader["email"].ToString();
					staff.StoreId = (int)reader["store_id"];
					staff.Phone = reader["phone"].ToString();
					staff.ManagerId = reader["manager_id"] != DBNull.Value ? (int?)reader["manager_id"] : null;
					//Order.Shipped_date = reader["Shipped_date"] != DBNull.Value ? (DateTime?)reader["Shipped_Date"] : null;

				};

				sql.Close();
				return staff;
			}
			
		} 
			
	}

}

	



