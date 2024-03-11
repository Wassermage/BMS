using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Services
{
    public class TemperatureReaderService
    {
        private IDbContextFactory<BmsDbContext> _dbContextFactory;
        public TemperatureReaderService(IDbContextFactory<BmsDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void AddTemperatureReader(TemperatureReader reader)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.TemperatureReaders.Add(reader);
                context.SaveChanges();
            }
        }
        public IEnumerable<TemperatureReader> GetTemperatureReaders()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<TemperatureReader> readers = context.TemperatureReaders.Include(reader => reader.Room).ToList();
                return readers;
            }
        }
    }
}
