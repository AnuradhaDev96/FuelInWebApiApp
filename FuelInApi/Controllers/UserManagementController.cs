using FuelInApi.Interfaces;
using FuelInApi.Models;
using FuelInApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuelInApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementRepository _userManagementInterface;

        public UserManagementController(IUserManagementRepository userManagementInterface)
        {
            _userManagementInterface = userManagementInterface;
        }

        [HttpGet("Users/GetUser/{email}")]
        [ProducesResponseType(200, Type = (typeof(SystemUser)))]
        public IActionResult GetUser(string email)
        {
            var data = _userManagementInterface.GetSystemUser(email);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(data);
        }

        [HttpPost("FuelInVehicleOwner")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateFuelInVehicleOwnerRecord([FromBody] CreateFuelInVehicleOwnerDto data)
        {
            if (data == null)
                return BadRequest(ModelState);

            data.PlateNumber = data.PlateNumber.Trim().Replace(" ", "");
            data.PlateNumber = data.PlateNumber.Replace("-", "");
            data.PlateNumber = data.PlateNumber.ToUpper();

            if (_userManagementInterface.CheckVehicleOwnerExistByPlateNumber(data.PlateNumber))
            {
                return NotFound("Driver already exists in the system");
            }

            var vehicleRegistry = _userManagementInterface.GetVehicleRegistryByPlateNumber(data.PlateNumber);
            
            if (vehicleRegistry == null)
                return NotFound("Plate number does not exist in National vehicle registry");

            var newSystemUserRecord = new SystemUser
            {
                FullName = data.FullName,
                Email = data.Email,
                Role = Models.Enums.UserRole.Driver,
            };

            var createdId = await _userManagementInterface.CreateSystemUser(newSystemUserRecord);

            if (createdId == null || createdId <= 0)
            {
                return NotFound("User cannot be created");
            }

            var newVehcileOwner = new FuelInVehicleOwner
            {
                PlateNumber = data.PlateNumber,
                SystemUserId = (int)createdId
            };

            var createdVehicleOwnerId = await _userManagementInterface.CreateFuelInVehicleOwner(newVehcileOwner);

            if (createdVehicleOwnerId == null || createdVehicleOwnerId <= 0)
            {
                return NotFound("Vehicle owner cannot be created");
            }

            var newFuelInVehicle = new FuelInVehicle
            {
                PlateNumber = data.PlateNumber,
                OwnerId = (int)createdVehicleOwnerId,
                VehicleClass = vehicleRegistry.VehicleClass,
                FuelType = vehicleRegistry.FuelType
            };

            if (!_userManagementInterface.CreateFuelInVehicle(newFuelInVehicle))
            {
                return NotFound("Vehicle cannot be created");
            }

            return Ok("User registered successfully");
        }
    }
}
