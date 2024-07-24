using HM3;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var hotels = new List<Hotel>();

app.MapGet("/hotels", async (DataContext context) =>
    await context.Hotels.ToListAsync());

app.MapGet("/hotels/{id}", async (int id, DataContext db) => {
    var hotel = await db.Hotels.FindAsync(id);
    if (hotel == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(hotel);
});

app.MapPost("/hotels", async (Hotel hotel, DataContext db) => {
    db.Hotels.Add(hotel);
    await db.SaveChangesAsync();
    return Results.Created($"/hotels/{hotel.Id}", hotel);
});

app.MapPut("/hotels", async (Hotel hotel, DataContext db) => {
    var existingHotel = await db.Hotels.FindAsync(hotel.Id);
    if (existingHotel == null)
    {
        return Results.NotFound();
    }

    existingHotel.Name = hotel.Name;
    existingHotel.Latitude = hotel.Latitude;
    existingHotel.Longitude = hotel.Longitude;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/hotels/{id}", async (int id, DataContext db) => {
    var hotel = await db.Hotels.FindAsync(id);
    if (hotel == null)
    {
        return Results.NotFound();
    }

    db.Hotels.Remove(hotel);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

public class Hotel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Latitude { get; set; }
    public required string Longitude { get; set; } 
}
