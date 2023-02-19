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
        public DbSet<FuelOrder> FuelOrders { get; set; }
        public DbSet<FuelTokenRequest> FuelTokenRequests { get; set; }

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

            modelBuilder.Entity<FuelOrder>()
                .Property(a => a.OrderStatus)
                .HasConversion(
                    x => x.ToString(),
                    x => (FuelOrderStatus)Enum.Parse(typeof(FuelOrderStatus), x));

            modelBuilder.Entity<FuelOrder>()
                .Property(a => a.FuelType)
                .HasConversion(
                    x => x.ToString(),
                    x => (PurchaseFuelType)Enum.Parse(typeof(PurchaseFuelType), x));

            modelBuilder.Entity<FuelTokenRequest>()
                .Property(a => a.RequestedFuelType)
                .HasConversion(
                    x => x.ToString(),
                    x => (PurchaseFuelType)Enum.Parse(typeof(PurchaseFuelType), x));
            #endregion

            #region One to one relations
            //modelBuilder.Entity<FuelInVehicle>()
            //    .HasKey(i => new { i.Id });

            modelBuilder.Entity<FuelInVehicleOwner>()
                .HasOne(ac => ac.Vehicle)
                .WithOne(a => a.Owner)
                .HasForeignKey<FuelInVehicle>(a => a.OwnerId);
            #endregion

            #region One to many relations
            modelBuilder.Entity<FuelOrder>()
                .HasKey(i => new { i.Id });

            modelBuilder.Entity<FuelOrder>()
                .HasOne(fo => fo.FuelStation)
                .WithMany(fs => fs.FuelOrders)
                .HasForeignKey(fo => fo.FuelStationId);

            modelBuilder.Entity<FuelTokenRequest>()
                .HasKey(i => new { i.Id });

            modelBuilder.Entity<FuelTokenRequest>()
                .HasOne(fo => fo.FuelStation)
                .WithMany(fs => fs.FuelTokenRequests)
                .HasForeignKey(fo => fo.FuelStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FuelTokenRequest>()
                .HasOne(fo => fo.FuelOrder)
                .WithMany(fs => fs.FuelTokenRequests)
                .HasForeignKey(fo => fo.FuelOrderId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
    
}
