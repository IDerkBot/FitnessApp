using System;
using FitnessApp.SqlServer;

namespace FitnessApp.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(int userId)
        {
            UserModel temp = Database.GetUserData(userId);

            Id                     = userId;
            ProfilePhoto.ByteArray = temp.ProfilePhoto.ByteArray;
            FirstName              = temp.FirstName;
            LastName               = temp.LastName;
            Email                  = temp.Email;
            Password               = temp.Password;
            Gender                 = temp.Gender;
            BirthDate              = temp.BirthDate;
            Weight                 = temp.Weight;
            Height                 = temp.Height;
            TargetWeight           = temp.TargetWeight;
            KilosToLosePerWeek     = temp.KilosToLosePerWeek;
            WorkoutsPerWeek        = temp.WorkoutsPerWeek;
            WorkoutHoursPerDay     = temp.WorkoutHoursPerDay;
        }

        public int Id { get; set; }

        public ImageModel ProfilePhoto { get; set; } = new ImageModel { Default = @"..\..\Images\AccountCircleDefaultIcon.png" };

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
}
