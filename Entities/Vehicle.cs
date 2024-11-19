namespace VehicleRentalAPI.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public decimal DaliyRate { get; set; }
        public bool Available { get; set; }
    }
}
