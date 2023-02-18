using FuelInApi.Interfaces;
using FuelInApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuelInApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelStationManagementController : ControllerBase
    {
        private readonly IUserManagementRepository _userManagementInterface;
        private readonly IFuelStationManagementInterface _fuelStationManagementInterface;

        public FuelStationManagementController(IUserManagementRepository userManagementInterface, IFuelStationManagementInterface fuelStationManagementInterface)
        {
            _userManagementInterface = userManagementInterface;
            _fuelStationManagementInterface = fuelStationManagementInterface;
        }

        [HttpPost("FuelStation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateFuelStationRecord([FromBody] FuelStation data)
        {
            if (_fuelStationManagementInterface.CheckFuelStationExistByLicenseId(data.LicenseId))
            {
                return NotFound("Fuel station already exist for the given license id");
            }

            if (!_fuelStationManagementInterface.CreateFuelStationByAdmin(data))
            {
                return NotFound("Fuel station could not be created");
            }

            return Ok("Fuel station created without a manager");
        }

        [HttpGet("FuelStation")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<FuelStation>)))]
        public IActionResult GetFuelStations()
        {
            var data = _fuelStationManagementInterface.GetFuelStations();

            return Ok(data);
        }
    }
}
