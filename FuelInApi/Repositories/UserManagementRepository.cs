using FuelInApi.Data;
using FuelInApi.Interfaces;
using FuelInApi.Models;

namespace FuelInApi.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private DataContext _context;
        public UserManagementRepository(DataContext context)
        {
            _context = context;
        }

        public SystemUser? GetSystemUser(string email)
        {
            return _context.SystemUsers.FirstOrDefault(x => x.Email == email);
        }

        public async Task<int?> CreateSystemUser(SystemUser user)
        {
            var newSystemUser = await _context.SystemUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return newSystemUser.Entity.Id;
        }

        public async Task<int?> CreateFuelInVehicleOwner(FuelInVehicleOwner owner)
        {
            var newVehicleOwner = await _context.FuelInVehicleOwners.AddAsync(owner);
            await _context.SaveChangesAsync();
            return newVehicleOwner.Entity.Id;
        }

        public VehicleRegistry? GetVehicleRegistryByPlateNumber(string plateNumber)
        {
            return _context.VehicleRegistry.Where(v => v.PlateNumber == plateNumber).FirstOrDefault();
        }

        public bool CreateFuelInVehicle(FuelInVehicle vehicle)
        {
            _context.FuelInVehicles.Add(vehicle);
            return Save();
        }

        public bool CheckVehicleOwnerExistByPlateNumber(string plateNumber)
        {
            var existingRecord = _context.FuelInVehicleOwners.Where(vo => vo.PlateNumber.Equals(plateNumber)).FirstOrDefault();
            if (existingRecord != null && existingRecord.SystemUserId > 0)
            {
                return true;
            }
            return false;
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
