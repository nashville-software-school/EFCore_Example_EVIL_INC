using Microsoft.EntityFrameworkCore;
using EvilInc.Models;

public class EvilIncDbContext : DbContext
{
    public DbSet<Minion> Minions { get; set; }
    public DbSet<Plot> Plots { get; set; }

    public EvilIncDbContext(DbContextOptions<EvilIncDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Seed data
        modelBuilder.Entity<Minion>().HasData(
            new Minion { Id = 1, Name = "Pikwik", Description = "Small and mischievous", PowerLevel = 3 },
            new Minion { Id = 2, Name = "Devin", Description = "Funny and musical", PowerLevel = 5 },
            new Minion { Id = 3, Name = "Glarth", Description = "Leader of the group", PowerLevel = 7 }
        );

        modelBuilder.Entity<Plot>().HasData(
            new Plot { Id = 1, Name = "Steal the Moon", Description = "A daring heist to shrink and steal the moon." },
            new Plot { Id = 2, Name = "Take Over the World", Description = "Classic villainous domination plan." }
        );

        // Link Minions and Plots
        modelBuilder.Entity<Minion>()
            .HasMany(m => m.Plots)
            .WithMany(p => p.Minions)
            .UsingEntity(j => j.HasData(
                new { MinionsId = 1, PlotsId = 1 },
                new { MinionsId = 2, PlotsId = 2 },
                new { MinionsId = 3, PlotsId = 1 }
            ));
    }
}

