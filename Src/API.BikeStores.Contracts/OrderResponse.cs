using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BikeStores.Contracts
{
	public class OrderResponse
	{
		public IEnumerable<Order>? Orders { get; set; }
	}
}
