namespace ParkingAPI
{
    public class ParkingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public bool isOccupied { get; set; } = false;
        public bool isReserved { get; set; } = false;
    }
}
