using API.BikeStores.Contracts;
using API.BikeStores.Services;

namespace API.BikeStores.Managers
{
	public class StoresManager:IStoresManager
	{
		private readonly IStoresService _storeService;

		public StoresManager (IStoresService storesService)
		{
			_storeService = storesService;
		}

		public StoresResponse GetAllStores()
		{
			var stores = _storeService.GetAllStores();
			BikeStores.Contracts.StoresResponse storeResponse = new StoresResponse();
			storeResponse.Stores = (List<Stores>)stores.Select(c => new BikeStores.Contracts.Stores
			{
				StoreId = c.StoreId,
				State = c.State,
				StoreName = c.StoreName,
				ZipCode = c.ZipCode,
				PhoneNo = c.PhoneNo,
				City = c.City,
				Street = c.Street,
				Email = c.Email
			}
			);
			return storeResponse;
		}
	}
}
