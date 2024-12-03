using EvilInc.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using EvilInc.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNpgsql<EvilIncDbContext>(builder.Configuration["EvilIncDbConnectionString"]);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/minions", (EvilIncDbContext db) => {
    return db.Minions.Select( m => 
        new MinionDTO{ 
            Name = m.Name,
            PowerLevel = m.PowerLevel 
        }
    ).ToList();
});

app.MapGet("/minions/{id}", (EvilIncDbContext db, int id) =>
{
    var minion = db.Minions.Include(m => m.Plots).FirstOrDefault(m => m.Id == id);
    if (minion == null) return Results.NotFound();
    return Results.Ok(new MinionDetailsDTO
    {
        Name = minion.Name,
        Description = minion.Description,
        PowerLevel = minion.PowerLevel,
        Plots = minion.Plots.Select(p => p.Name).ToList()
    });
});

app.MapDelete("/plots/{id}", async (EvilIncDbContext db, int id) =>
{
    var plot = await db.Plots.FindAsync(id);
    if (plot == null) return Results.NotFound();
    db.Plots.Remove(plot);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/plots/{plotId}/assign/{minionId}", async (EvilIncDbContext db, int plotId, int minionId) =>
{
    var plot = await db.Plots.Include(p => p.Minions).FirstOrDefaultAsync(p => p.Id == plotId);
    var minion = await db.Minions.FindAsync(minionId);
    if (plot == null || minion == null) return Results.NotFound();
    plot.Minions.Add(minion);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();