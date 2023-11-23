using HallRentalAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// configuration of policy to allow our blazor component to acccess revelant resources
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7050", "https://localhost:7050")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
    .AllowAnyOrigin()
);
app.UseCors();
app.Run();
