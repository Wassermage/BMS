using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Endpoints
{
    public static class AccessControlGroupEndpoints
    {
        public static void MapAccessControlGroupEndpoints(this IEndpointRouteBuilder app)
        {
            /**
             * List all existing groups
             */
            app.MapGet("/api/access-control-groups", async (BmsDbContext db) =>
            {
                var groupsDto = await db.AccessControlGroups
                    .Include(g => g.Employees)
                    .Include(g => g.AccessControlGroupRooms)
                        .ThenInclude(r => r.Room)
                    .Select(g => new
                    {
                        g.Id,
                        g.Name,
                        g.AllowedEntryHour,
                        Employees = g.Employees.Select(e => new
                        {
                            e.Id,
                            e.FirstName,
                            e.LastName,
                            e.JobTitle
                        }),
                        Rooms = g.AccessControlGroupRooms.Select(r => new
                        {
                            r.RoomId,
                            r.Room.Name
                        })
                    })
                    .ToListAsync();

                return Results.Ok(groupsDto);
            })
                .WithTags("Access Control Groups")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Existing group details
             */
            app.MapGet("/api/access-control-groups/{id:int}", async (int id, BmsDbContext db) =>
            {
                var groupDto = await db.AccessControlGroups
                    .Include(g => g.Employees)
                    .Include(g => g.AccessControlGroupRooms)
                        .ThenInclude(r => r.Room)
                    .Where(g => g.Id == id)
                    .Select(g => new
                    {
                        g.Id,
                        g.Name,
                        g.AllowedEntryHour,
                        Employees = g.Employees.Select(e => new
                        {
                            e.Id,
                            e.FirstName,
                            e.LastName,
                            e.JobTitle
                        }),
                        Rooms = g.AccessControlGroupRooms.Select(r => new
                        {
                            r.RoomId,
                            r.Room.Name
                        })
                    })
                    .FirstOrDefaultAsync();

                return groupDto is not null ? Results.Ok(groupDto) : Results.NotFound();
            })
                .WithTags("Access Control Groups")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Add new group
             */
            app.MapPost("/api/access-control-groups", async (AccessControlGroup group, BmsDbContext db) =>
            {
                db.AccessControlGroups.Add(group);
                await db.SaveChangesAsync();
                return Results.Created($"/api/access-control-groups/{group.Id}", group);
            })
                .WithTags("Access Control Groups")
                .Produces<AccessControlGroup>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Update existing group
             */
            app.MapPut("/api/access-control-groups/{id:int}", async (int id, AccessControlGroup updated, BmsDbContext db) =>
            {
                var group = await db.AccessControlGroups.FindAsync(id);
                if (group is null) return Results.NotFound();
                group.Name = updated.Name;
                group.AllowedEntryHour = updated.AllowedEntryHour;
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Access Control Groups")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Delete existing group
             */
            app.MapDelete("/api/access-control-groups/{id:int}", async (int id, BmsDbContext db) =>
            {
                var group = await db.AccessControlGroups.FindAsync(id);
                if (group is null) return Results.NotFound();
                db.AccessControlGroups.Remove(group);
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Access Control Groups")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
