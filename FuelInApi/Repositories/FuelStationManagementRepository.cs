using FuelInApi.Data;
using FuelInApi.Interfaces;
using FuelInApi.Models;

namespace FuelInApi.Repositories
{
    public class FuelStationManagementRepository : IFuelStationManagementInterface
    {
        private DataContext _context;
        public FuelStationManagementRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckFuelStationExistByLicenseId(string licenseId)
        {
            return _context.FuelStations.Any(f => f.LicenseId == licenseId);
        }

        public bool CheckManagerExistForFuelStationByLicenseId(string licenseId)
        {
            var fuelStation = _context.FuelStations.Where(s => s.LicenseId == licenseId).FirstOrDefault();
            if (fuelStation == null)
                return false;

            if (fuelStation.ManagerUserId == null || fuelStation.ManagerUserId <= 0)
                return false;

            return true;
        }

        public bool CreateFuelStationByAdmin(FuelStation data)
        {
            _context.FuelStations.Add(data);
            return Save();

        }

        public FuelStation? GetFuelStationByLicenseId(string licenseId)
        {
            return _context.FuelStations.Where(f => f.LicenseId == licenseId).FirstOrDefault();
        }

        public ICollection<FuelStation> GetFuelStations()
        {
            return _context.FuelStations.ToList();
        }

        public bool UpdateFuelStation(FuelStation data)
        {
            _context.FuelStations.Update(data);
            return Save();
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
