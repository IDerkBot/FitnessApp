using FitnessApp.SqlServer;

namespace FitnessApp.Models
{
    public class AdminModel
    {
        public AdminModel() { }

        public AdminModel(int adminId)
        {
            AdminModel temp = Database.GetAdminData(adminId);

            Id = adminId;
            FirstName = temp.FirstName;
            LastName = temp.LastName;
            Email = temp.Email;
            Password = temp.Password;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
