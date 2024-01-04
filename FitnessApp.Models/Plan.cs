using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("Plan")]
public class Plan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public string Hardness { get; set; }
    public byte[] Photo { get; set; }
}