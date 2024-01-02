using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("Day")]
public class Day
{
    public int Id { get; set; }
    public int DayNumber { get; set; }

    public string BreakfastDescription { get; set; }

    public string LunchDescription { get; set; }

    public string DinnerDescription { get; set; }

    public string WorkoutDescription { get; set; }
}