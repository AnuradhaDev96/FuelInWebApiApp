using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelInApi.Models
{
    public class FuelInVehicleOwner
    {
        [Key]
        public int Id { get; set; }
        public string PlateNumber { get; set; }


        [ForeignKey("SystemUserId")]
        public int SystemUserId { get; set; }
        public SystemUser SystemUser { get; set; }

        //One to one relationship
        public FuelInVehicle Vehicle { get; set; }
    }
}
