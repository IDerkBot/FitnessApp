using FitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.DataAccessLayer;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // KEYS
        modelBuilder.Entity<JoinedChallenge>().HasKey(x => new { x.PersonId, x.ChallengeId });
        modelBuilder.Entity<JoinedPlan>().HasKey(x => new { x.PersonId, x.PlanId });
        modelBuilder.Entity<PersonFood>().HasKey(x => new { x.PersonId, x.FoodId });
        modelBuilder.Entity<PersonWeight>().HasKey(x => new { x.PersonId, x.DateTime });
        modelBuilder.Entity<PersonWorkout>().HasKey(x => new { x.PersonId, x.WorkoutId, x.Date });
        modelBuilder.Entity<DayInPlan>().HasKey(x => new { x.PlanId, x.DayNumber });
        modelBuilder.Entity<Person>()
            .HasOne(x => x.User)
            .WithOne(x => x.Person)
            .HasForeignKey<User>(u => u.PersonId);
        modelBuilder.Entity<User>()
            .HasOne(x => x.Person)
            .WithOne(x => x.User)
            .HasForeignKey<Person>(p => p.UserId);
        
        // DELETE
        
        // JoinedChallenges
        modelBuilder.Entity<JoinedChallenge>()
            .HasOne(x => x.Challenge)
            .WithMany(x => x.JoinedChallenges)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<JoinedChallenge>()
            .HasOne(x => x.Person)
            .WithMany(x => x.JoinedChallenges)
            .OnDelete(DeleteBehavior.Cascade);
        
        // JoinedPlans
        modelBuilder.Entity<JoinedPlan>()
            .HasOne(x => x.Plan)
            .WithMany(x => x.JoinedPlans)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<JoinedPlan>()
            .HasOne(x => x.Person)
            .WithMany(x => x.JoinedPlans)
            .OnDelete(DeleteBehavior.Cascade);

        // PersonFoods
        modelBuilder.Entity<PersonFood>()
            .HasOne(x => x.Person)
            .WithMany(x => x.PersonFoods)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<PersonFood>()
            .HasOne(x => x.Food)
            .WithMany(x => x.PersonFoods)
            .OnDelete(DeleteBehavior.Cascade);

        // PersonWeights
        modelBuilder.Entity<PersonWeight>()
            .HasOne(x => x.Person)
            .WithMany(x => x.PersonWeights)
            .OnDelete(DeleteBehavior.Cascade);

        // PersonWorkouts
        modelBuilder.Entity<PersonFood>()
            .HasOne(x => x.Person)
            .WithMany(x => x.PersonFoods)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<PersonFood>()
            .HasOne(x => x.Food)
            .WithMany(x => x.PersonFoods)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Person
        modelBuilder.Entity<Person>()
            .HasOne(x => x.User)
            .WithOne(x => x.Person)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<User>()
            .HasOne(x => x.Person)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);

    }

    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<DayInPlan> DayInPlans { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<PersonWeight> PersonWeights { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<JoinedChallenge> JoinedChallenges { get; set; }
    public DbSet<JoinedPlan> JoinedPlans { get; set; }
    public DbSet<PersonFood> PersonFoods { get; set; }
    public DbSet<PersonWorkout> PersonWorkouts { get; set; }
    public DbSet<Workout> Workouts { get; set; }
}