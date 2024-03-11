using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Services
{
    public class AccessControlGroupService
    {
        private IDbContextFactory<BmsDbContext> _dbContextFactory;
        public AccessControlGroupService(IDbContextFactory<BmsDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void AddGroup(AccessControlGroup group)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.AccessControlGroups.Add(group);
                context.SaveChanges();
            }
        }
        public AccessControlGroup GetGroup(int  id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var group = context.AccessControlGroups.SingleOrDefault(g => g.Id == id);
                return group;
            }
        }
        public AccessControlGroup GetGroup(string groupName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var group = context.AccessControlGroups.SingleOrDefault(g => g.Name == groupName);
                return group;
            }
        }

        public void RemoveGroup(AccessControlGroup group)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Remove(group);
                context.SaveChanges();
            }
        }
        public void RemoveGroup(int groupId)
        {
            AccessControlGroup group = GetGroup(groupId);
            if (group == null)
            {
                throw new Exception("Remove failed. Group with given ID does not exist.");
            }
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Remove(group);
                context.SaveChanges();
            }
        }
    }
}
