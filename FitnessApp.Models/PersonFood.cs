using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table(nameof(PersonFood))]
public class PersonFood
{
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    public int FoodId { get; set; }
    public Food Food { get; set; } = null!;

    public DateTime Date { get; set; }
    public double CaloriesGained { get; set; }
}