using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Services
{
    public class RoomService
    {
        private IDbContextFactory<BmsDbContext> _dbContextFactory;
        public RoomService(IDbContextFactory<BmsDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void AddRoom(Room room)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                context.Rooms.Add(room);
                context.SaveChanges();
            }
        }
        public IEnumerable<Room> GetAllRooms()
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<Room> rooms = context.Rooms.Include(room => room.TemperatureReaders).ToList();
                return rooms;
            }
        }
        //public Task<IEnumerable<Room>> GetAllRoomsAsync()
        //{
        //    using (var context = _dbContextFactory.CreateDbContext())
        //    {
        //        IEnumerable<Room> rooms = context.Rooms.ToList();
        //        return Task.FromResult(rooms);
        //    }
        //}
        public Room GetRoom(int id)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var room = context.Rooms.Include(room => room.TemperatureReaders).SingleOrDefault(r => r.Id == id);
                return room;
            }
        }
        //// Testowo
        //public Task<Room> GetRoomByIdAsync(int id)
        //{
        //    using (var context = _dbContextFactory.CreateDbContext())
        //    {
        //        var room = context.Rooms.SingleOrDefault(r => r.Id == id);
        //        return Task.FromResult(room);
        //    }
        //}
        public void RemoveRoom(Room room)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Remove(room);
                context.SaveChanges();
            }
        }
        public void RemoveRoom(int roomId)
        {
            var room = GetRoom(roomId);
            if (room == null)
            {
                throw new Exception("Remove failed. Room with given ID does not exist.");
            }
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Remove(room);
                context.SaveChanges();
            }
        }
        public void UpdateRoomById(int id, string name)
        {
            var room = GetRoom(id);
            if(room == null)
            {
                throw new Exception("Update failed. Room with given ID does not exist.");
            }
            room.Name = name;
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Update(room);
                context.SaveChanges();
            }
        }
    }
}
