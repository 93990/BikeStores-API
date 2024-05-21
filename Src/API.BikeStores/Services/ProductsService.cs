using API.Pitstop.Products.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Pitstop.Products.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IConfiguration _configuration;
        public ProductsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<Models.Product> GetAll()
        {
            var lstProducts = new List<Product>();

         


            SqlConnection sqlCon;

            var SqlconString = _configuration["PitstopSqlConnectionString"];

            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sqlCmnd = new SqlCommand(Constants.SpGetAllProducts, sqlCon);
                sqlCmnd.CommandType = CommandType.StoredProcedure;

                ////parameters for sp
                //sql_cmnd.Parameters.AddWithValue("@ProductCode", SqlDbType.NVarChar).Value = "";
                //sql_cmnd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = "";

                ////nonquery
                //sql_cmnd.ExecuteNonQuery();

                var reader = sqlCmnd.ExecuteReader();
                while (reader.Read())
                {
                    lstProducts.Add(new Models.Product()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductCode = reader["ProductCode"].ToString(),
                        ProductName = reader["ProductName"].ToString()
                    });
                }
                sqlCon.Close();
            }

            return lstProducts;
        }
    }
}
