using FuelInApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelInApi.Models
{
    public class FuelOrder
    {
        [Key]
        public int Id { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }
        public int OrderQuantityInLitres { get; set; }
        public FuelOrderStatus OrderStatus { get; set; }
        public PurchaseFuelType FuelType { get; set; }

        [ForeignKey("FuelStationId")]
        public int FuelStationId { get; set; }
        public FuelStation FuelStation { get; set; }

        public ICollection<FuelTokenRequest> FuelTokenRequests { get; set; }
    }
}
