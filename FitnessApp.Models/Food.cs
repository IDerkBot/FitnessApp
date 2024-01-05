using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table(nameof(Food))]
public class Food
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public IEnumerable<PersonFood> PersonFoods { get; set; }
}