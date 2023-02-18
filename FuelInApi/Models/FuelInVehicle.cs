using FuelInApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelInApi.Models
{
    public class FuelInVehicle
    {
        [Key]
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public VehicleFuelType FuelType { get; set; }
        public VehicleClass VehicleClass { get; set; }

        //One to one relationship. Key is in this side
        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }
        public FuelInVehicleOwner Owner { get; set; }
    }
}
