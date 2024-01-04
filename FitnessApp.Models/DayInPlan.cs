using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("DayInPlan")]
public class DayInPlan
{
    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;
    
    public int DayNumber { get; set; }

    public string? BreakfastDescription { get; set; }

    public string? LunchDescription { get; set; }

    public string? DinnerDescription { get; set; }

    public string? WorkoutDescription { get; set; }
}