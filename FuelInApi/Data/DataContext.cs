using FuelInApi.Models;
using FuelInApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FuelInApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<VehicleRegistry> VehicleRegistry { get; set; }
        public DbSet<FuelInVehicleOwner> FuelInVehicleOwners { get; set; }
        public DbSet<FuelInVehicle> FuelInVehicles { get; set; }

        public DbSet<FuelStation> FuelStations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Enum mapping
            modelBuilder.Entity<SystemUser>()
                .Property(a => a.Role)
                .HasConversion(
                    x => x.ToString(),
                    x => (UserRole)Enum.Parse(typeof(UserRole), x));

            modelBuilder.Entity<VehicleRegistry>()
                .Property(a => a.FuelType)
                .HasConversion(
                    x => x.ToString(),
                    x => (VehicleFuelType)Enum.Parse(typeof(VehicleFuelType), x));

            modelBuilder.Entity<VehicleRegistry>()
                .Property(a => a.VehicleClass)
                .HasConversion(
                    x => x.ToString(),
                    x => (VehicleClass)Enum.Parse(typeof(VehicleClass), x));

            modelBuilder.Entity<FuelInVehicle>()
                .Property(a => a.FuelType)
                .HasConversion(
                    x => x.ToString(),
                    x => (VehicleFuelType)Enum.Parse(typeof(VehicleFuelType), x));

            modelBuilder.Entity<FuelInVehicle>()
                .Property(a => a.VehicleClass)
                .HasConversion(
                    x => x.ToString(),
                    x => (VehicleClass)Enum.Parse(typeof(VehicleClass), x));
            #endregion

            #region One to one relations
            //modelBuilder.Entity<FuelInVehicle>()
            //    .HasKey(i => new { i.Id });

            modelBuilder.Entity<FuelInVehicleOwner>()
                .HasOne(ac => ac.Vehicle)
                .WithOne(a => a.Owner)
                .HasForeignKey<FuelInVehicle>(a => a.OwnerId);
            #endregion
        }
    }
    
}
