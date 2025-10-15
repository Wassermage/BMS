using BMS.Data;
using BMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Endpoints
{
    public static class TemperatureReadoutEndpoints
    {
        /**
         * Endpoints for Temperature Readouts
         */
        public static void MapTemperatureReadoutEndpoints(this IEndpointRouteBuilder app)
        {
            /**
             * Get all existing temperature readouts
             */
            app.MapGet("/api/temperature-readouts", async (BmsDbContext db) =>
            {
                var readoutsDto = await db.TemperatureReadouts
                    .Include(r => r.TemperatureReader)
                    .ThenInclude(tr => tr.Room)
                    .Select(r => new
                    {
                        r.Id,
                        r.TemperatureC,
                        r.ReadoutTime,
                        Reader = new { r.TemperatureReader.Id, r.TemperatureReader.Name },
                        Room = new { r.TemperatureReader.Room.Id, r.TemperatureReader.Room.Name }
                    })
                    .ToListAsync();

                return Results.Ok(readoutsDto);
            })
                .WithTags("Temperature Readouts")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Get a single temperature readout with a given id
             */
            app.MapGet("/api/temperature-readouts/{id:int}", async (int id, BmsDbContext db) =>
            {
                var readoutDto = await db.TemperatureReadouts
                    .Include(r => r.TemperatureReader)
                    .ThenInclude(tr => tr.Room)
                    .Where(r => r.Id == id)
                    .Select(r => new
                    {
                        r.Id,
                        r.TemperatureC,
                        r.ReadoutTime,
                        Reader = new { r.TemperatureReader.Id, r.TemperatureReader.Name },
                        Room = new { r.TemperatureReader.Room.Id, r.TemperatureReader.Room.Name }
                    })
                    .FirstOrDefaultAsync();

                return readoutDto is not null ? Results.Ok(readoutDto) : Results.NotFound();
            })
                .WithTags("Temperature Readouts")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Add new temperature readout
             */
            app.MapPost("/api/temperature-readouts", async (TemperatureReadout readout, BmsDbContext db) =>
            {
                db.TemperatureReadouts.Add(readout);
                await db.SaveChangesAsync();
                return Results.Created($"/api/temperature-readouts/{readout.Id}", readout);
            })
                .WithTags("Temperature Readouts")
                .Produces<TemperatureReadout>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Update existing temperature readout
             */
            app.MapPut("/api/temperature-readouts/{id:int}", async (int id, TemperatureReadout updated, BmsDbContext db) =>
            {
                var readout = await db.TemperatureReadouts.FindAsync(id);
                if (readout is null) return Results.NotFound();
                readout.TemperatureC = updated.TemperatureC;
                readout.ReadoutTime = updated.ReadoutTime;
                readout.TemperatureReaderId = updated.TemperatureReaderId;
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Temperature Readouts")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            /**
             * Delete existing temperature readout
             */
            app.MapDelete("/api/temperature-readouts/{id:int}", async (int id, BmsDbContext db) =>
            {
                var readout = await db.TemperatureReadouts.FindAsync(id);
                if (readout is null) return Results.NotFound();
                db.TemperatureReadouts.Remove(readout);
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Temperature Readouts")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
