using BMS.Data;
using BMS.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? throw new NullReferenceException("No connection string found in config!");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<AccessControlGroupService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<MaintenanceRequestService>();
builder.Services.AddTransient<RoomService>();
builder.Services.AddTransient<TemperatureReaderService>();
builder.Services.AddTransient<TemperatureReadoutService>();
builder.Services.AddDbContextFactory<BmsDbContext>((DbContextOptionsBuilder options) => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
