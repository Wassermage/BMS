using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
        {
            /**
             * List all existing employes
             */
            app.MapGet("/api/employees", async (BmsDbContext db) =>
            {
                var employeesDto = await db.Employees
                    .Include(e => e.AccessControlGroup)
                    .Select(e => new
                    {
                        e.Id,
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        e.HireDate,
                        AccessControlGroup = new
                        {
                            e.AccessControlGroup.Id,
                            e.AccessControlGroup.Name
                        }
                    })
                    .ToListAsync();

                return Results.Ok(employeesDto);
            })
                .WithTags("Employees")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Existing employee details
             */
            app.MapGet("/api/employees/{id:int}", async (int id, BmsDbContext db) =>
            {
                var empDto = await db.Employees
                    .Include(e => e.AccessControlGroup)
                    .Where(e => e.Id == id)
                    .Select(e => new
                    {
                        e.Id,
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        e.HireDate,
                        AccessControlGroup = new
                        {
                            e.AccessControlGroup.Id,
                            e.AccessControlGroup.Name
                        }
                    })
                    .FirstOrDefaultAsync();

                return empDto is not null ? Results.Ok(empDto) : Results.NotFound();
            })
                .WithTags("Employees")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Add new employee
             */
            app.MapPost("/api/employees", async (Employee emp, BmsDbContext db) =>
            {
                db.Employees.Add(emp);
                await db.SaveChangesAsync();
                return Results.Created($"/api/employees/{emp.Id}", emp);
            })
                .WithTags("Employees")
                .Produces<Employee>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
            /**
             * Update existing employee
             */
            app.MapPut("/api/employees/{id:int}", async (int id, Employee updated, BmsDbContext db) =>
            {
                var emp = await db.Employees.FindAsync(id);
                if (emp is null) return Results.NotFound();
                emp.FirstName = updated.FirstName;
                emp.LastName = updated.LastName;
                emp.JobTitle = updated.JobTitle;
                emp.HireDate = updated.HireDate;
                emp.AccessControlGroupId = updated.AccessControlGroupId;
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Employees")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Delete existing employee
             */
            app.MapDelete("/api/employees/{id:int}", async (int id, BmsDbContext db) =>
            {
                var emp = await db.Employees.FindAsync(id);
                if (emp is null) return Results.NotFound();
                db.Employees.Remove(emp);
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Employees")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
