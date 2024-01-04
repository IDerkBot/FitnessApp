using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("Challenge")]
public class Challenge
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int TargetMinutes { get; set; }
    public string Reward { get; set; }
    public string Condition { get; set; }
    public byte[] Photo { get; set; }
    public Workout Workout { get; set; }
    public string StyledTargetMinutes => TargetMinutes + " minutes";
}