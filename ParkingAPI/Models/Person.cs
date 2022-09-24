using System.Text.Json.Serialization;

namespace ParkingAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = "string";
        public string Surname { get; set; } = "string";
        public DateTime DateOfBirth { get; set; }
    }
}
