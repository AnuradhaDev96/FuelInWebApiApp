namespace FuelInApi.Models.Dtos
{
    public class CreateFuelOrderDto
    {
        public DateTime ExpectedDeliveryDate { get; set; }
        public int OrderQuantityInLitres { get; set; }
        public string OrderStatus { get; set; }
        public string FuelType { get; set; }
    }
}
