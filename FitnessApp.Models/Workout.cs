using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table(nameof(Workout))]
public class Workout
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}