

using  API.BikeStores.Models;

namespace API.BikeStores.Services
{
	public interface IStoresService
	{
		 public IEnumerable<Stores> GetAllStores();

	}
}
