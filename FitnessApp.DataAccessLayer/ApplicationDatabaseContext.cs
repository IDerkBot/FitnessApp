using FitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.DataAccessLayer;

public sealed class ApplicationDatabaseContext : DbContext
{
    public ApplicationDatabaseContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-LSPMPFI\\SQLEXPRESS;Database=FitnessApp;Trusted_Connection=True");
    }

    public DbSet<Admin> Admin { get; set; }
    public DbSet<Challenge> Challenge { get; set; }
    public DbSet<Day> Day { get; set; }
    public DbSet<Feedback> Feedback { get; set; }
    public DbSet<Plan> Plan { get; set; }
    public DbSet<User> User { get; set; }
}