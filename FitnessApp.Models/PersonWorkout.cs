using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table(nameof(PersonWorkout))]
public class PersonWorkout
{
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    public int WorkoutId { get; set; }
    public Workout Workout { get; set; } = null!;

    public int MinutesOfWork { get; set; }
    public DateTime Date { get; set; }
    public double CaloriesLost { get; set; }
}