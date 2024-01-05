using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("Person")]
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => FirstName + " " + LastName;
    public string Email { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; } = DateTime.Now;
    public int Age => (DateTime.Now - BirthDate).Days / 365;
    public double Weight { get; set; }
    public double Height { get; set; }
    public double TargetWeight { get; set; }
    public double KilosToLosePerWeek { get; set; }
    public double WorkoutsPerWeek { get; set; }
    public double WorkoutHoursPerDay { get; set; }
    public byte[]? ProfilePhoto { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public IEnumerable<JoinedChallenge> JoinedChallenges { get; set; }
    public IEnumerable<JoinedPlan> JoinedPlans { get; set; }
    public IEnumerable<PersonFood> PersonFoods { get; set; }
    public IEnumerable<PersonWeight> PersonWeights { get; set; }
    public IEnumerable<PersonWorkout> PersonWorkouts { get; set; }

    public Person()
    {
        
    }
    
    public Person(User user)
    {
        User = user;
    }
}