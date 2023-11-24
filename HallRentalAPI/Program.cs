using HallRentalAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<HallDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HallDbConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    API_Debug_Interface();
}

void API_Debug_Interface()
{
    Console.Write("\nEnable API debug Swagger interface ( [y] for yes; [n] for no; ): ");

    string? debug_interface = Console.ReadLine();
    debug_interface = debug_interface == null ? "n" : debug_interface;

    Console.Write("\n\n");

    if (debug_interface.ToLower() == "y")
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else if(debug_interface.ToLower() == "n")
    {

    }
    else
    {
        Console.Clear();
        API_Debug_Interface();
    }

    Console.Clear();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
