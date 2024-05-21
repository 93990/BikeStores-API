using API.BikeStores.Contracts;
using API.BikeStores.Models;
using API.Pitstop.Products;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace API.BikeStores.Services
{
	public class OrderService:IOrderService
	{
		private readonly IConfiguration _configuration;
		private readonly string _sqlConnectionString;
		public OrderService(IConfiguration configuration)
		{
			_configuration = configuration;
			_sqlConnectionString = configuration["PitstopSqlConnectionString"];
		}

		public IEnumerable<Models.Orders> GetAllOrders()
		{
			var lstorders = new List<Models.Orders>();
			SqlConnection sql;
			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spGetAllOders, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
						lstorders.Add(new Models.Orders
					{
						Order_id = (int)reader["order_id"],
						Customer_id = (int)reader["customer_id"],
						Order_Status = (byte)reader["order_status"],
						Order_date = (DateTime)reader["order_date"],
						Required_date = (DateTime)reader["required_date"],
						Shipped_date = reader["Shipped_date"] != DBNull.Value ? (DateTime?)reader["Shipped_Date"] : null,
							Staff_id = (int)reader["staff_id"],
						Store_id = (int)reader["store_id"]
					}) ;
				}
				sql.Close();
			}
			return lstorders;
		}

		public Models.Orders GetOrderById(int OrderId)
		{

			Orders Order = new Orders();
			SqlConnection sql;
			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spGetOrder, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@OrderId", SqlDbType.Int).Value = OrderId;
				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{

				//	Order.Order_id = (int)reader["order_id"];
					Order.Customer_id = (int)reader["customer_id"];
					Order.Order_Status = (byte)reader["order_status"];
					Order.Order_date = (DateTime)reader["order_date"];
					Order.Required_date = (DateTime)reader["required_date"];
					Order.Shipped_date = reader["Shipped_date"] != DBNull.Value ? (DateTime?)reader["Shipped_Date"] : null;
					Order.Staff_id = (int)reader["staff_id"];
					Order.Store_id = (int)reader["store_id"];
					};
				}
				sql.Close();
				return Order;
		}

		public bool InsertOrder(InsertOrderModel insertorder)
		{
			int noorderpresent = 0;
			SqlConnection sql;
			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spInsertOrder, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@CustomerId", SqlDbType.Structured).Value = insertorder.Customer_id; //Addwithvalue jo parameter sp expect kar ra hai wo provide karti hai
				cmd.Parameters.AddWithValue("@OrderStatus", SqlDbType.Structured).Value =insertorder.Order_Status;
				cmd.Parameters.AddWithValue("@OrderDate", SqlDbType.Structured).Value = insertorder.Order_date;
				cmd.Parameters.AddWithValue("@RequiredDate", SqlDbType.Structured).Value = insertorder.Required_date;
				cmd.Parameters.AddWithValue("@ShippedDate", SqlDbType.Structured).Value = insertorder.Shipped_date;
				cmd.Parameters.AddWithValue("@StaffId", SqlDbType.Structured).Value = insertorder.Staff_id;
				cmd.Parameters.AddWithValue("@StoreId", SqlDbType.Structured).Value =insertorder.Store_id;
				
				cmd.ExecuteNonQuery(); //When we work with post jo query likhi hai use execute karate hai
				sql.Close();
				return true;
			}


		}
		public bool DeleteOrder(int orderid)
		{
			SqlConnection sql;
			using(sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spDeleteOrder, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("OrderId" , SqlDbType.Structured).Value = orderid;
				cmd.Parameters.Add("@UpdateRowCount", SqlDbType.Int);
				cmd.Parameters["@UpdateRowCount"].Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				int updaterow = (int)cmd.Parameters["@UpdateRowCount"].Value;
				sql.Close();
				return true;
			}
		}
		public bool UpdateOrder(int orderid, InsertOrderModel updateorder)
		{
			Models.Orders order = new Models.Orders();
			order = GetOrderById(orderid);
			SqlConnection sql;
			using (sql = new SqlConnection(_sqlConnectionString))
			{
				sql.Open();
				SqlCommand cmd = new SqlCommand(Constants.spUpdateOrder, sql);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@OrderId", SqlDbType.Structured).Value = orderid;
				cmd.Parameters.AddWithValue("CustomerId", SqlDbType.Structured).Value = updateorder.Customer_id == null ? order.Customer_id : updateorder.Customer_id;
				cmd.Parameters.AddWithValue("@OrderStatus", SqlDbType.Structured).Value = updateorder.Order_Status == null ? order.Order_Status : updateorder.Order_Status;
				cmd.Parameters.AddWithValue("@OrderDate", SqlDbType.Structured).Value = updateorder.Order_date == null ? order.Order_date : updateorder.Order_date;
				cmd.Parameters.AddWithValue("@RequiredDate", SqlDbType.Structured).Value = updateorder.Required_date == null ? order.Required_date : updateorder.Required_date;
				cmd.Parameters.AddWithValue("@ShippedDate", SqlDbType.Structured).Value = updateorder.Shipped_date == null ? order.Shipped_date : updateorder.Shipped_date;
				cmd.Parameters.AddWithValue("@StaffId", SqlDbType.Structured).Value = updateorder.Staff_id == null ? order.Staff_id : updateorder.Staff_id;
				cmd.Parameters.AddWithValue("@StoreId", SqlDbType.Structured).Value = updateorder.Store_id == null ? order.Store_id : updateorder.Store_id;
				cmd.ExecuteNonQuery();
				sql.Close();
				return true;
			}
		}


	}
}
