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
                List<TemperatureReadout> readouts = context.TemperatureReadouts.Where(readout => readout.TemperatureReader.RoomId == room.Id).OrderByDescending(readout => readout.ReadoutTime).Take(20).Include(readout => readout.TemperatureReader.Room).ToList();
                return readouts;
            }
        }
        public List<TemperatureReadout> GetTemperatureReadoutsByRoom(int roomId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<TemperatureReadout> readouts = context.TemperatureReadouts.Where(readout => readout.TemperatureReader.RoomId == roomId).OrderByDescending(readout => readout.ReadoutTime).Take(20).Include(readout => readout.TemperatureReader.Room).ToList();
                return readouts;
            }
        }
    }
}
