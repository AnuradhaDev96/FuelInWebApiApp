using FuelInApi.Models;

namespace FuelInApi.Interfaces
{
    public interface IFuelStationManagementInterface
    {
        FuelStation? GetFuelStationByLicenseId(string licenseId);

        ICollection<FuelStation> GetFuelStations();

        bool CreateFuelStationByAdmin(FuelStation data);

        bool UpdateFuelStation(FuelStation data);

        bool CheckFuelStationExistByLicenseId(string licenseId);

        bool CheckManagerExistForFuelStationByLicenseId(string licenseId);
    }
}
