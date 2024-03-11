using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Services
{
    public class TemperatureReadoutService
    {
        private IDbContextFactory<BmsDbContext> _dbContextFactory;
        public TemperatureReadoutService(IDbContextFactory<BmsDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void AddTemperatureReadout(TemperatureReadout readout)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.TemperatureReadouts.Add(readout);
                context.SaveChanges();
            }
        }
        public List<TemperatureReadout> GetTemperatureReadoutsByRoom(Room room)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<TemperatureReadout> readouts = context.TemperatureReadouts.Where(readout => readout.TemperatureReader.Room.Id == room.Id).OrderByDescending(readout => readout.readoutTime).Take(10).ToList();
                return readouts;
            }
        }
    }
}
