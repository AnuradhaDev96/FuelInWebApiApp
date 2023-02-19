using FuelInApi.Models;

namespace FuelInApi.Interfaces
{
    public interface IFuelStationManagementInterface
    {
        FuelStation? GetFuelStationByManagerId(int managerUserId);

        FuelStation? GetFuelStationByLicenseId(string licenseId);

        FuelStation? GetFuelStationById(int id);

        ICollection<FuelStation> GetFuelStations();

        ICollection<FuelOrder> GetFuelOrders();
        
        ICollection<FuelTokenRequest> FuelTokenRequestsByDriverId(int driverId);

        bool CreateFuelStationByAdmin(FuelStation data);

        bool UpdateFuelStation(FuelStation data);

        bool CheckFuelStationExistByLicenseId(string licenseId);

        bool CheckManagerExistForFuelStationByLicenseId(string licenseId);

        bool CreateFuelOrderByFuelStationId(FuelOrder fuelOrder);

        bool CreateFuelTokenRequestByDriverId(FuelTokenRequest token);


        bool IsFuelOrderExistForGivenExpectedFillingDateByStationId(DateTime expectedFillingDate, int fillingStationId);
        FuelOrder? GetFuelOrderExistForGivenExpectedFillingDateByStationId(DateTime expectedFillingDate, int fillingStationId);
    }
}
