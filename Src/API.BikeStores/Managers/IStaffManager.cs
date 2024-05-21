using API.BikeStores.Contracts;

namespace API.BikeStores.Managers
{
	public interface IStaffManager
	{
		StaffResponse GetAllStaff();

		Staff GetStaffById(int StaffId);
	}
}
