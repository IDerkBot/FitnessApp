using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("Feedback")]
public class Feedback
{
    public int Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;

    public string Message { get; set; }
}