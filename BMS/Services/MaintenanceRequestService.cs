using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BMS.Services
{
    public class MaintenanceRequestService
    {
        private IDbContextFactory<BmsDbContext> _dbContextFactory;
        public MaintenanceRequestService(IDbContextFactory<BmsDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void AddMaintenanceRequest(MaintenanceRequest request)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.MaintenanceRequests.Add(request);
                context.SaveChanges();
            }
        }
        public MaintenanceRequest GetMaintenanceRequest(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var request = context.MaintenanceRequests.Include(request => request.CreatedBy).Include(request => request.AssignedTo).SingleOrDefault(r => r.Id == id);
                return request;
            }
        }
        public List<MaintenanceRequest> GetMaintenanceRequests()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<MaintenanceRequest> requests = context.MaintenanceRequests.OrderBy(request => request.CreatedDate).Take(10).Include(request => request.CreatedBy).Include(request => request.AssignedTo).ToList();
                return requests;
            }
        }
        public List<MaintenanceRequest> GetMaintenanceRequests(int assignedToId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<MaintenanceRequest> requests = context.MaintenanceRequests.Where(request => request.AssignedToId == assignedToId).OrderBy(request => request.CreatedDate).Take(10).Include(request => request.CreatedBy).Include(request => request.AssignedTo).ToList();
                return requests;
            }
        }
        public void UpdateMaintenanceRequest(MaintenanceRequest request, RequestStatus status, Employee? assignedTo)
        {
            request.Status = status;
            if(assignedTo != null)
            {
                request.AssignedTo = assignedTo;
            }
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Update(request);
                context.SaveChanges();
            }
        }
        public void UpdateMaintenanceRequest(int requestId, RequestStatus status, Employee? assignedTo)
        {
            MaintenanceRequest request = GetMaintenanceRequest(requestId);
            if (request == null)
            {
                throw new Exception("Update failed. MaintenanceRequest with given ID does not exist.");
            }
            if (assignedTo != null)
            {
                request.AssignedTo = assignedTo;
            }
            request.Status = status;
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Update(request);
                context.SaveChanges();
            }
        }
    }
}
