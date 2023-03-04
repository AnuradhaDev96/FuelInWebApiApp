using FuelInApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelInApi.Models
{
    public class FuelTokenRequest
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ScheduledFillingDate { get; set; }
        public DateTime? TolerenceUntil { get; set; }
        public DateTime? PaymentDoneOn { get; set; }
        public DateTime? FuelCollectedOn { get; set; }
        public int RequestedQuotaInLitres { get; set; }
        public PurchaseFuelType RequestedFuelType { get; set; }

        [ForeignKey("FuelStationId")]
        public int FuelStationId { get; set; }
        public FuelStation FuelStation { get; set; }

        [ForeignKey("DriverId")]
        public int DriverId { get; set; }
        public SystemUser Driver { get; set; }

        [ForeignKey("FuelOrderId")]
        public int FuelOrderId { get; set; }
        public FuelOrder FuelOrder { get; set; }
    }
}
