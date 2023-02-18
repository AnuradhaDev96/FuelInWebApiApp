using FuelInApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FuelInApi.Models
{
    public class SystemUser
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }
    }
}
