using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table(nameof(JoinedChallenge))]
public class JoinedChallenge
{
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
    
    public int ChallengeId { get; set; }
    public Challenge Challenge { get; set; } = null!;
    
    public DateTime JoinedDate { get; set; }
    public double Progress { get; set; }
}