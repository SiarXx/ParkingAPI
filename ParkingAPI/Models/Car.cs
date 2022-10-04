using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
       
        [Required]
        public string Registration { get; set; } = string.Empty;
        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        public Person? Owner { get; set; }
    }
}
