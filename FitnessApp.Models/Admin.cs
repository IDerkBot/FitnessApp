using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models;

[Table("Admin")]
public class Admin
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;

    public string Email { get; set; }

    public string Password { get; set; }

    public Admin()
    {
        
    }
    
    public Admin(int id)
    {
        Id = id;
    }
}