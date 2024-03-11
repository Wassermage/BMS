using BMS.Data.Models;
using BMS.Data;
using Microsoft.EntityFrameworkCore;

namespace BMS.Services
{
    public class EmployeeService
    {
        private IDbContextFactory<BmsDbContext> _dbContextFactory;
        public EmployeeService(IDbContextFactory<BmsDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void AddEmployee(Employee employee)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }
        public Employee GetEmployee(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var employee = context.Employees.SingleOrDefault(e => e.Id == id);
                return employee;
            }
        }
        public IEnumerable<Employee> GetEmployees()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<Employee> employees = context.Employees.ToList();
                return employees;
            }
        }
        public void RemoveEmployee(Employee employee)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Remove(employee);
                context.SaveChanges();
            }
        }
        public void RemoveEmployee(int employeeId)
        {
            Employee employee = GetEmployee(employeeId);
            if (employee == null)
            {
                throw new Exception("Remove failed. Employee with given ID does not exist.");
            }
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Remove(employee);
                context.SaveChanges();
            }
        }
    }
}
