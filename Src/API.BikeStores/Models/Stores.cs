using Org.BouncyCastle.Tls;

namespace API.BikeStores.Models
{
	public class Stores
	{

		public int StoreId { get; set; }

		public string StoreName { get; set; }

		public int PhoneNo { get; set; }

		public string Email { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string ZipCode { get; set; }
	}
}
