namespace FitnessApp.Models;

public class Plan
{
    private string _duration;

    public int Id { get; set; }

    public byte[] Photo { get; set; }

    public bool IsJoined { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Duration
    {
        get => _duration + " days";
        set => _duration = value;
    }

    public string Hardness { get; set; }
}