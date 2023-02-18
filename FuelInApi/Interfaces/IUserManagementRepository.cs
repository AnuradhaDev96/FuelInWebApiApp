using FuelInApi.Models;

namespace FuelInApi.Interfaces
{
    public interface IUserManagementRepository
    {
        SystemUser? GetSystemUser(string email);

        VehicleRegistry? GetVehicleRegistryByPlateNumber(string plateNumber);

        bool CheckVehicleOwnerExistByPlateNumber(string plateNumber);

        bool CheckSystemUserExistByEmail(string email);

        bool CreateFuelInVehicle(FuelInVehicle vehicle);

        Task<int?> CreateSystemUser(SystemUser user);

        Task<int?> CreateFuelInVehicleOwner(FuelInVehicleOwner owner);
    }
}
