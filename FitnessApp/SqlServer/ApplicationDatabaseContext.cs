using System.Data.Entity;
using FitnessApp.Models;

namespace FitnessApp.SqlServer
{
    public class ApplicationDatabaseContext : DbContext
    {
        public string ConnectionString { get; set; } = "metadata=res://*/Entities.Database.csdl|res://*/Entities.Database.ssdl|res://*/Entities.Database.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\\mssqllocaldb;initial catalog=FitnessApp;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework&quot;";

        public ApplicationDatabaseContext()
        {
            Database.Connection.ConnectionString = ConnectionString;
            Database.Create();
            Database.Connection.Open();
        }
        
        public DbSet<AdminModel> Admin { get; set; }
        public DbSet<ChallengeModel> Challenge { get; set; }
        public DbSet<DayModel> Day { get; set; }
        public DbSet<FeedbackModel> Feedback { get; set; }
        public DbSet<ImageModel> Image { get; set; }
        public DbSet<PlanModel> Plan { get; set; }
        public DbSet<UserModel> User { get; set; }
    }
}