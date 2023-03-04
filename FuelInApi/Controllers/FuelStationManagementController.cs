using AutoMapper;
using FuelInApi.Interfaces;
using FuelInApi.Models;
using FuelInApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuelInApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelStationManagementController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IFuelStationManagementInterface _fuelStationManagementInterface;
        private readonly IMapper _mapper;

        public FuelStationManagementController(IMailService mailService, IFuelStationManagementInterface fuelStationManagementInterface,
             IMapper mapper)
        {
            _mailService = mailService;
            _fuelStationManagementInterface = fuelStationManagementInterface;
            _mapper = mapper;
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

        [HttpPost("FuelStation/FuelOrder/{fuelStationManagerUserId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateFuelOrderRecord(int fuelStationManagerUserId, [FromBody] CreateFuelOrderDto data)
        {
            var fuelStationOfLoggedInManager = _fuelStationManagementInterface.GetFuelStationByManagerId(fuelStationManagerUserId);
            
            if (fuelStationOfLoggedInManager == null)
                return NotFound("Fuel station cannot be found under given manager id");

            var createData = _mapper.Map<FuelOrder>(data);

            createData.FuelStationId = fuelStationOfLoggedInManager.Id;
            createData.OrderStatus = Models.Enums.FuelOrderStatus.PaymentDone;

            if (!_fuelStationManagementInterface.CreateFuelOrderByFuelStationId(createData))
            {
                return NotFound("Something went wrong while saving fuel order");
            }
            

            return Ok("Fuel order created successfully under fuel station of logged in manager");
        }

        //[HttpGet("FuelStation/FuelOrder")]
        //[ProducesResponseType(200, Type = (typeof(IEnumerable<FuelOrdersSummaryDto>)))]
        //public IActionResult GetFuelOrdersSummary()
        //{
        //    var summaryList = new List<FuelOrdersSummaryDto>();

        //    var fuelOrders = _fuelStationManagementInterface.GetFuelOrders();

        //    foreach (var order in fuelOrders)
        //    {
        //        var fuelStationOfOrder = _fuelStationManagementInterface.GetFuelStationById(order.FuelStationId);

        //        var newFuelOrderSummary = new FuelOrdersSummaryDto
        //        {
        //            Id = order.Id,
        //            ExpectedDeliveryDate = order.ExpectedDeliveryDate,
        //            OrderQuantityInLitres = order.OrderQuantityInLitres,
        //            OrderStatus = order.OrderStatus,
        //            FuelType = order.FuelType,

        //            FuelStationId = order.FuelStationId,
        //            LocalAuthority = fuelStationOfOrder.LocalAuthority,
        //            Address = fuelStationOfOrder.Address,
        //            PopulationDensity = fuelStationOfOrder.PopulationDensity,
        //        };

        //        summaryList.Add(newFuelOrderSummary);
        //    }
        //    summaryList.OrderByDescending(s => s.PopulationDensity);
        //    return Ok(summaryList);
        //}

        [HttpGet("FuelStation/FuelOrder")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<FuelOrder>)))]
        public IActionResult GetFuelOrdersSummary()
        {

            var fuelOrders = _fuelStationManagementInterface.GetFuelOrders();

            foreach (var order in fuelOrders)
            {
                var fuelStationOfOrder = _fuelStationManagementInterface.GetFuelStationById(order.FuelStationId);

                order.FuelStation = fuelStationOfOrder;
            }
            return Ok(fuelOrders);
        }


        [HttpPost("FuelTokenRequest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateFuelTokenRequestRecord([FromBody] CreateFuelTokenDto data)
        {
            // Get fuel order for expected 
            if (_fuelStationManagementInterface.IsFuelOrderExistForGivenExpectedFillingDateByStationId(
                expectedFillingDate: data.ScheduledFillingDate, fillingStationId: data.FuelStationId))
            {
                var matchingFuelOrder = _fuelStationManagementInterface.GetFuelOrderExistForGivenExpectedFillingDateByStationId(
                    expectedFillingDate: data.ScheduledFillingDate, fillingStationId: data.FuelStationId);

                var createData = _mapper.Map<FuelTokenRequest>(data);

                createData.TolerenceUntil = data.ScheduledFillingDate.AddHours(3);
                createData.FuelOrderId = matchingFuelOrder.Id;

                if (!_fuelStationManagementInterface.CreateFuelTokenRequestByDriverId(createData))
                {
                    return NotFound("Something went wrong while saving fuel token");
                }


                return Ok("Fuel token created successfully for driver id");
            }
            else {
                return NotFound("No fuel order for given expected date");
            }
            
        }

        [HttpGet("FuelTokenRequest/{driverId}")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<FuelTokenRequest>)))]
        public IActionResult GetFuelTokenRequestsByDriverId(int driverId)
        {

            var requests = _fuelStationManagementInterface.FuelTokenRequestsByDriverId(driverId);

            //foreach (var order in fuelOrders)
            //{
            //    var fuelStationOfOrder = _fuelStationManagementInterface.GetFuelStationById(order.FuelStationId);

            //    order.FuelStation = fuelStationOfOrder;
            //}
            return Ok(requests);
        }

        [HttpPut("FuelOrder/{orderId}")]
        public async Task<IActionResult> ConfirmFuelOrderByManager(int orderId)
        {
            try
            {
                var fuelOrder = _fuelStationManagementInterface.GetFuelOrderById(orderId);

                if (fuelOrder == null)
                    return NotFound("Fuel order cannot be found");

                fuelOrder.OrderStatus = Models.Enums.FuelOrderStatus.DeliveryConfirmed;

                if (!_fuelStationManagementInterface.UpdateFuelOrder(fuelOrder))
                {
                    return NotFound("Fuel order cannot be updated");
                }

                var newMail = new MailRequestDto
                {
                    ToEmail = "anusampath9470@gmail.com",
                    Body = "The fuel order of your token is confirmed.",
                    Subject = "Order confirmation to customers"
                };

                await _mailService.SendEmailAsync(newMail);

                return Ok("Fuel order confirmed and mail sent to customers");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }

    }
}
