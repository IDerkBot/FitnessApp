using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("Feedback")]
public class Feedback
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Message { get; set; }
    public int Rating { get; set; }
}