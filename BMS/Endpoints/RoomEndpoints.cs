using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Endpoints
{
    public static class RoomEndpoints
    {
        public static void MapRoomEndpoints(this IEndpointRouteBuilder app)
        {
            /**
             * Get all rooms
             */
            app.MapGet("/api/rooms", async (BmsDbContext db) =>
            {
                var roomsDto = await db.Rooms
                    .Include(r => r.TemperatureReaders)
                    .Include(r => r.AccessControlGroupRooms)
                    .ThenInclude(acgr => acgr.AccessControlGroup)
                    .Select(r => new
                    {
                        r.Id,
                        r.Name,
                        TemperatureReaders = r.TemperatureReaders.Select(tr => new
                        {
                            tr.Id,
                            tr.Name
                        }),
                        AccessGroups = r.AccessControlGroupRooms.Select(acgr => new
                        {
                            acgr.AccessControlGroup.Id,
                            acgr.AccessControlGroup.Name
                        })
                    })
                    .ToListAsync();

                return Results.Ok(roomsDto);
            })
                .WithTags("Rooms")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Get room details
             */
            app.MapGet("/api/rooms/{id:int}", async (int id, BmsDbContext db) =>
            {
                var roomDto = await db.Rooms
                    .Include(r => r.TemperatureReaders)
                    .Include(r => r.AccessControlGroupRooms)
                        .ThenInclude(acgr => acgr.AccessControlGroup)
                    .Where(r => r.Id == id)
                    .Select(r => new
                    {
                        r.Id,
                        r.Name,
                        TemperatureReaders = r.TemperatureReaders.Select(tr => new
                        {
                            tr.Id,
                            tr.Name
                        }),
                        AccessGroups = r.AccessControlGroupRooms.Select(acgr => new
                        {
                            acgr.AccessControlGroup.Id,
                            acgr.AccessControlGroup.Name
                        })
                    })
                    .FirstOrDefaultAsync();

                return roomDto is not null ? Results.Ok(roomDto) : Results.NotFound();
            })
                .WithTags("Rooms")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Add new room
             */
            app.MapPost("/api/rooms", async (Room room, BmsDbContext db) =>
            {
                db.Rooms.Add(room);
                await db.SaveChangesAsync();
                return Results.Created($"/api/rooms/{room.Id}", room);
            })
                .WithTags("Rooms")
                .Produces<Room>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Update existing room
             */
            app.MapPut("/api/rooms/{id:int}", async (int id, Room updated, BmsDbContext db) =>
            {
                var room = await db.Rooms.FindAsync(id);
                if (room is null) return Results.NotFound();
                room.Name = updated.Name;
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Rooms")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            app.MapDelete("/api/rooms/{id:int}", async (int id, BmsDbContext db) =>
            {
                var room = await db.Rooms.FindAsync(id);
                if (room is null) return Results.NotFound();
                db.Rooms.Remove(room);
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Rooms")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
