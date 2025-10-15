using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Endpoints
{
    public static class TemperatureReaderEndpoints
    {
        /**
         * Endpoints for temperature readers 
         */
        public static void MapTemperatureReaderEndpoints(this IEndpointRouteBuilder app)
        {
            /**
             * List all existing temperature readers
             */
            app.MapGet("/api/temperature-readers", async (BmsDbContext db) =>
            {
                var readersDto = await db.TemperatureReaders
                    .Include(tr => tr.Room)
                    .Include(tr => tr.Readouts)
                    .Select(tr => new
                    {
                        tr.Id,
                        tr.Name,
                        Room = new { tr.Room.Id, tr.Room.Name },
                        Readouts = tr.Readouts.Select(r => new { r.Id, r.TemperatureC, r.ReadoutTime })
                    })
                    .ToListAsync();

                return Results.Ok(readersDto);
            })
                .WithTags("Temperature Readers")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Existing temperature reader details
             */
            app.MapGet("/api/temperature-readers/{id:int}", async (int id, BmsDbContext db) =>
            {
                var readerDto = await db.TemperatureReaders
                    .Include(tr => tr.Room)
                    .Include(tr => tr.Readouts)
                    .Where(tr => tr.Id == id)
                    .Select(tr => new
                    {
                        tr.Id,
                        tr.Name,
                        Room = new { tr.Room.Id, tr.Room.Name },
                        Readouts = tr.Readouts.Select(r => new { r.Id, r.TemperatureC, r.ReadoutTime })
                    })
                    .FirstOrDefaultAsync();

                return readerDto is not null ? Results.Ok(readerDto) : Results.NotFound();
            })
                .WithTags("Temperature Readers")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Add new temperature reader
             */
            app.MapPost("/api/temperature-readers", async (TemperatureReader reader, BmsDbContext db) =>
            {
                db.TemperatureReaders.Add(reader);
                await db.SaveChangesAsync();
                return Results.Created($"/api/temperature-readers/{reader.Id}", reader);
            })
                .WithTags("Temperature Readers")
                .Produces<TemperatureReader>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Update existing temperature reader
             */
            app.MapPut("/api/temperature-readers/{id:int}", async (int id, TemperatureReader updated, BmsDbContext db) =>
            {
                var reader = await db.TemperatureReaders.FindAsync(id);
                if (reader is null) return Results.NotFound();
                reader.Name = updated.Name;
                reader.RoomId = updated.RoomId;
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Temperature Readers")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Delete existing temperature reader
             */
            app.MapDelete("/api/temperature-readers/{id:int}", async (int id, BmsDbContext db) =>
            {
                var reader = await db.TemperatureReaders.FindAsync(id);
                if (reader is null) return Results.NotFound();
                db.TemperatureReaders.Remove(reader);
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Temperature Readers")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
