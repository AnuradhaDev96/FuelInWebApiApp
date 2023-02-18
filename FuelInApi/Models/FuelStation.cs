using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelInApi.Models
{
    public class FuelStation
    {
        [Key]
        public int Id { get; set; }

        public string District { get; set; }
        public string LocalAuthority { get; set; }
        public string LicenseId { get; set; }
        public string Address { get; set; }
        public double PopulationDensity { get; set; }

        [ForeignKey("ManagerUserId")]
        public int? ManagerUserId { get; set; }
        public SystemUser? ManagerUser { get; set; }

    }
}
