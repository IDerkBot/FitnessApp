using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table(nameof(JoinedChallenge))]
public class JoinedChallenge
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int ChallengeId { get; set; }
    public Challenge Challenge { get; set; } = null!;
    
    public DateTime JoinedDate { get; set; }
    public double Progress { get; set; }
}