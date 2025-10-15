using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Endpoints
{
    public static class MaintenanceRequestEndpoints
    {
        /**
         * Endpoints for Maintenance Requests
         */
        public static void MapMaintenanceRequestEndpoints(this IEndpointRouteBuilder app)
        {
            /**
             * Get all maintenance requests
             */
            app.MapGet("/api/maintenance-requests", async (BmsDbContext db) =>
            {
                var requests = await db.MaintenanceRequests
                    .Include(r => r.CreatedBy)
                    .Include(r => r.AssignedTo)
                    .Select(r => new
                    {
                        r.Id,
                        r.Title,
                        r.Description,
                        r.Status,
                        r.StatusFormatted,
                        r.CreatedDate,
                        r.CreatedDateFormatted,
                        CreatedBy = r.CreatedBy != null ? new
                        {
                            r.CreatedBy.Id,
                            r.CreatedBy.FirstName,
                            r.CreatedBy.LastName
                        } : null,
                        AssignedTo = r.AssignedTo != null ? new
                        {
                            r.AssignedTo.Id,
                            r.AssignedTo.FirstName,
                            r.AssignedTo.LastName
                        } : null
                    })
                    .ToListAsync();

                return Results.Ok(requests);
            })
                .WithTags("Maintenance Requests")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Existing maintenance request details
             */
            app.MapGet("/api/maintenance-requests/{id:int}", async (int id, BmsDbContext db) =>
            {
                var request = await db.MaintenanceRequests
                    .Include(r => r.CreatedBy)
                    .Include(r => r.AssignedTo)
                    .Where(r => r.Id == id)
                    .Select(r => new
                    {
                        r.Id,
                        r.Title,
                        r.Description,
                        r.Status,
                        r.StatusFormatted,
                        r.CreatedDate,
                        r.CreatedDateFormatted,
                        CreatedBy = r.CreatedBy != null ? new
                        {
                            r.CreatedBy.Id,
                            r.CreatedBy.FirstName,
                            r.CreatedBy.LastName
                        } : null,
                        AssignedTo = r.AssignedTo != null ? new
                        {
                            r.AssignedTo.Id,
                            r.AssignedTo.FirstName,
                            r.AssignedTo.LastName
                        } : null
                    })
                    .FirstOrDefaultAsync();

                return request is not null ? Results.Ok(request) : Results.NotFound();
            })
                .WithTags("Maintenance Requests")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Create new maintenance request
             */
            app.MapPost("/api/maintenance-requests", async (MaintenanceRequest request, BmsDbContext db) =>
            {
                if (request.CreatedDate == default)
                    request.CreatedDate = DateTime.Now;

                db.MaintenanceRequests.Add(request);
                await db.SaveChangesAsync();

                return Results.Created($"/api/maintenance-requests/{request.Id}", request);
            })
                .WithTags("Maintenance Requests")
                .Produces<MaintenanceRequest>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Update existing maintenance request
             */
            app.MapPut("/api/maintenance-requests/{id:int}", async (int id, MaintenanceRequest updated, BmsDbContext db) =>
            {
                var existing = await db.MaintenanceRequests.FindAsync(id);
                if (existing is null)
                    return Results.NotFound();

                existing.Title = updated.Title;
                existing.Description = updated.Description;
                existing.Status = updated.Status;
                existing.AssignedToId = updated.AssignedToId;

                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Maintenance Requests")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Delete existing maintenance request
             */
            app.MapDelete("/api/maintenance-requests/{id:int}", async (int id, BmsDbContext db) =>
            {
                var existing = await db.MaintenanceRequests.FindAsync(id);
                if (existing is null)
                    return Results.NotFound();

                db.MaintenanceRequests.Remove(existing);
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Maintenance Requests")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
