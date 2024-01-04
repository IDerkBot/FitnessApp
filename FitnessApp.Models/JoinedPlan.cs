using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table(nameof(JoinedPlan))]
public class JoinedPlan
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;

    public int DayNumber { get; set; }
    public bool IsBreakfastDone { get; set; }
    public bool IsLunchDone { get; set; }
    public bool IsDinnerDone { get; set; }
    public bool IsWorkoutsDone { get; set; }
}