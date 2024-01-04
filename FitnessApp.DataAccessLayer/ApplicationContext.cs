using FitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.DataAccessLayer;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<Day> Days { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<PersonWeight> PersonWeights { get; set; }
    
    // JoinedChallenge (Challenge, Person)
}