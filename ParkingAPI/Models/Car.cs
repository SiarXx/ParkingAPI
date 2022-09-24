using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        [Required]
        public string Registration { get; set; } = "string";
        public string Make { get; set; } = "string";
        public string Model { get; set; } = "string";
        public bool IsParked { get; set; }
        public Person Owner { get; set; }
        [ForeignKey("ParkedSpot")]
        public int? ParkedSpotId { get; set; }
        public ParkingSpot? ParkedSpot { get; set; }
    }
}
