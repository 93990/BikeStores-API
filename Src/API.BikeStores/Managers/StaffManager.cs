using API.BikeStores.Contracts;
using API.BikeStores.Models;
using API.BikeStores.Services;
using Microsoft.AspNetCore.Http.Metadata;

namespace API.BikeStores.Managers
{

	public class StaffManager:IStaffManager
	{
		private readonly IStaffService _staffService;

		public StaffManager(IStaffService StaffService)
		{
			_staffService = StaffService;
		}

		public StaffResponse GetAllStaff()
		{
			var lststaff = _staffService.GetAllStaff();
			BikeStores.Contracts.StaffResponse staffResponse = new StaffResponse();
			staffResponse.Staff = lststaff.Select(c =>new BikeStores.Contracts.Staff
			{
				StaffId = c.StaffId,
				FirstName = c.FirstName,
				LastName = c.LastName,
				Phone = c.Phone,
				Email = c.Email,
				Active = c.Active,
				ManagerId = c.ManagerId,
				StoreId	= c.StoreId
			});
			return staffResponse;
		}
		public Contracts.Staff GetStaffById(int StaffId)
		{
			Models.Staff Staff = _staffService.GetStaffById(StaffId);

			Contracts.Staff StaffResponse = new Contracts.Staff();

			{
				StaffResponse.FirstName = Staff.FirstName;
				StaffResponse.LastName = Staff.LastName;
				StaffResponse.Phone = Staff.Phone;
				StaffResponse.Active = Staff.Active;
				StaffResponse.Email = Staff.Email;
				Staff.ManagerId = Staff.ManagerId;
				//StaffResponse.StaffId = Staff.StaffId;
				StaffResponse.StoreId = Staff.StoreId;

			}
			return StaffResponse;
		}
	}
}
