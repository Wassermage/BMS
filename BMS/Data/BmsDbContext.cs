using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Data
{
    public class BmsDbContext : DbContext
    {
        public BmsDbContext(DbContextOptions<BmsDbContext> options) : base(options) { }
        public DbSet<AccessControlGroup> AccessControlGroups { get; set; }
        public DbSet<AccessControlGroupRoom> AccessControlGroupRooms { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<TemperatureReader> TemperatureReaders { get; set; }
        public DbSet<TemperatureReadout> TemperatureReadouts { get; set; }
    }
}
