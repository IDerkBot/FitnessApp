using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("User")]
public class User
{
    public int Id { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
    public byte Access { get; set; } = 0;

    public User()
    {
        
    }

    public User(int id)
    {
        
    }
}