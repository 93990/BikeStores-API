using API.BikeStores.Models;
//using API.BikeStores.Contracts;


namespace API.BikeStores.Services
{
	public interface IStaffService
	{
		IEnumerable<Staff> GetAllStaff();

		Staff GetStaffById(int StaffId);
	}
}
