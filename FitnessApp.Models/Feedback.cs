namespace FitnessApp.Models;

public class Feedback
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;

    public string Message { get; set; }
}