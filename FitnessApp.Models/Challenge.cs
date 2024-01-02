namespace FitnessApp.Models;

public class Challenge
{
    public int Id { get; set; }

    public byte[] Photo { get; set; }

    public bool IsJoined { get; set; }

    public int Progress { get; set; }

    public int WorkoutType { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string DueDate { get; set; }

    public int TargetMinutes { get; set; }

    public string StyledTargetMinutes => TargetMinutes + " minutes";

    public string Reward { get; set; }
}