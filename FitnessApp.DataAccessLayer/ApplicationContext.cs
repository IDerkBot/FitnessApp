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
        modelBuilder.Entity<JoinedChallenge>().HasKey(x => new { x.UserId, x.ChallengeId });
        modelBuilder.Entity<JoinedPlan>().HasKey(x => new { x.UserId, x.PlanId });
        modelBuilder.Entity<PersonFood>().HasKey(x => new { x.PersonId, x.FoodId });
        modelBuilder.Entity<PersonWeight>().HasKey(x => new { x.PersonId, x.DateTime });
        modelBuilder.Entity<PersonWorkout>().HasKey(x => new { x.PersonId, x.WorkoutId, x.Date });
        modelBuilder.Entity<DayInPlan>().HasKey(x => new { x.PlanId, x.DayNumber });
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