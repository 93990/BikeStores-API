using API.BikeStores.Contracts;
using API.BikeStores.Managers;
using Microsoft.AspNetCore.Mvc;

namespace API.BikeStores.Controllers
{
	[ApiVersion("1")]
	[Route("api/v{version:ApiVersion}/")]
	[ApiController]
	public class StaffController:ControllerBase
	{
		

		
			private readonly ILogger<StaffController> _logger;
			private readonly IStaffManager _staffManager;

			public StaffController(ILogger<StaffController> logger, IStaffManager staffManager)
			{
				_logger = logger;
				_staffManager = staffManager;
			}

			[HttpGet]
			[Route("staff")]
			public ActionResult<StaffResponse> GetAllStaff()
			{
				_logger.Log(LogLevel.Information, "StaffController, Get(): Get all Staff called.");
				var response = _staffManager.GetAllStaff();
				return Ok(response);
			}
		[HttpGet]
		[Route("staff/{StaffId}")]
		public ActionResult<Staff> GetStaffById(int StaffId)
		{
			_logger.Log(LogLevel.Information, "StaffController, Get(): Get all Staffby id called.");
			var response = _staffManager.GetStaffById(StaffId);
			if(response == null)
			{
				return StatusCode(404, "Staff Not found");
			}
			return Ok(response);
		}	
		
	}
}
