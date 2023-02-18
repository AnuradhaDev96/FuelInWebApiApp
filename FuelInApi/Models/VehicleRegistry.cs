using FuelInApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FuelInApi.Models
{
    public class VehicleRegistry
    {
        [Key]
        public string PlateNumber { get; set; }
        public VehicleFuelType FuelType { get; set; }
        public VehicleClass VehicleClass { get; set; }
    }
}
