using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("PersonWeight")]
public class PersonWeight
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public double Weight { get; set; }

    public Person Person { get; set; }
}