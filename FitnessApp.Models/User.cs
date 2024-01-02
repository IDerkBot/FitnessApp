namespace FitnessApp.Models;

public class User
{
    public int Id { get; set; }

    public byte[] ProfilePhoto { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;

    public string Email { get; set; }

    public string Password { get; set; }

    public string Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public int Age => (DateTime.Now - BirthDate).Days / 365;

    public double Weight { get; set; }

    public double Height { get; set; }

    public double TargetWeight { get; set; }

    public double KilosToLosePerWeek { get; set; }

    public double WorkoutsPerWeek { get; set; }

    public double WorkoutHoursPerDay { get; set; }
}