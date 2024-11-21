using GlobalCommunity.Models;
using Microsoft.EntityFrameworkCore;

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Seed для LocalCommunity
    modelBuilder.Entity<LocalCommunity>().HasData(
        new LocalCommunity
        {
            Id = 1,
            Name = "Tech Enthusiasts",
            Description = "A community for people passionate about technology",
            DateStarted = DateTime.Now.AddYears(-5)
        },
        new LocalCommunity
        {
            Id = 2,
            Name = "Travelers",
            Description = "A community for people who love to travel",
            DateStarted = DateTime.Now.AddYears(-3)
        }
    );
}