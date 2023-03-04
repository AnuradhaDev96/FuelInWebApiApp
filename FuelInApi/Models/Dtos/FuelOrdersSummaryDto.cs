using FuelInApi.Models.Enums;

namespace FuelInApi.Models.Dtos
{
    public class FuelOrdersSummaryDto
    {
        //FuelOrder details
        public int Id { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }
        public int OrderQuantityInLitres { get; set; }
        public FuelOrderStatus OrderStatus { get; set; }
        public PurchaseFuelType FuelType { get; set; }

        //Fuel station details
        public int? FuelStationId { get; set; }
        public string LocalAuthority { get; set; }
        public string Address { get; set; }
        public double PopulationDensity { get; set; }
    }
}
