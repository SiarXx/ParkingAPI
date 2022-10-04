using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ParkingAPI.Models
{
    public class ParkingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsOccupied { get; set; }
        public bool IsReserved { get; set; }
        public Car? ParkedCar { get; set; }
        [ForeignKey("Car")]
        public int? CarId { get; set; }

    }
}
