using Microsoft.EntityFrameworkCore;
using ParkingAPI.Models;

namespace ParkingAPI.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Person> Persons { get; set; }

    }
}
