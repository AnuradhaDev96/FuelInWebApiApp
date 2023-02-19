namespace FuelInApi.Models.Dtos
{
    public class CreateFuelTokenDto
    {
        public string Token { get; set; }
        public DateTime ScheduledFillingDate { get; set; }
        
        public int RequestedQuotaInLitres { get; set; }
        public string RequestedFuelType { get; set; }

        public int FuelStationId { get; set; }
        public int DriverId { get; set; }
    }
}
