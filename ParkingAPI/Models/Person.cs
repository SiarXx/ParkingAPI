using System.Text.Json.Serialization;

namespace ParkingAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        [JsonIgnore]
        public ICollection<ParkingSpot> ReservedSpots { get; set; }

    }
}
