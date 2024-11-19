namespace VehicleRentalAPI.DTO
{
    public class RentalDTO
    {
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public DateTime RetalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
