namespace API.BikeStores.Models
{
	public class InsertOrderModel
	{
		public int? Customer_id { get; set; }

		public int? Order_Status { get; set; }

		public DateTime? Order_date { get; set; }

		public DateTime? Required_date { get; set; }

		public DateTime? Shipped_date { get; set; }

		public int? Store_id { get; set; }

		public int? Staff_id { get; set; }
	}
}
