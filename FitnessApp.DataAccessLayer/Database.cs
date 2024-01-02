﻿using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using FitnessApp.Models;

namespace FitnessApp.DataAccessLayer;

public static class Database
{
    // [IMPORTANT] Add your server name to ServerDetails Class.
    private static readonly ApplicationDatabaseContext _database = new();

    public static int AccountId { get; set; }

    public static string AccountType { get; set; }

    ///////////////////////////// Helper Functions /////////////////////////////

    public static string EncryptPassword(string password)
    {
        string hash = "f0le@rn";
        byte[] data = Encoding.UTF8.GetBytes(password);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
            string encryptedPassword;
            using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider())
            {
                triDes.Key = keys;
                triDes.Mode = CipherMode.ECB;
                triDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = triDes.CreateEncryptor();
                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                encryptedPassword = Convert.ToBase64String(results, 0, results.Length);
            }

            return encryptedPassword;
        }
    }

    private static string GenerateRandomPassword()
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        int length = 7;
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }

        return res.ToString();
    }

    private static void SendUserEmail(string email, string name)
    {
        MailMessage message = new MailMessage();

        // Reciever's Email
        message.To.Add(email);

        // Email Subject
        message.Subject = "Welcome To FitnessApp";

        // Sender's Email
        message.From = new MailAddress("fitness.weightlossapp@gmail.com", "Fitness App");

        // Email Body
        message.IsBodyHtml = true;
        string htmlBody = "<body>" +
                          "<img src=https://bit.ly/2PI1mx4>" +
                          "<p style=\"float: left; \">" +
                          "<img src=https://bit.ly/2STDZ62 height=\"100px\" width=\"100px\" hspace=\"5\" style=\"border - right: 1px solid black;\">" +
                          "</p> " +
                          "<p style=\"padding: 20px; \"> " +
                          " <font size=\"5px\" color=\"#0F88A8\">" +
                          "<b> Welcome " + name + " , </b>" +
                          "</font>" +
                          "<br>" +
                          "Thank you for choosing FitnessApp!" +
                          "<br> <br> " +
                          "Ready for <b>RESHAPING</b> &#9889;" +
                          "<br><br>" +
                          " <font size=\"2 px\">" +
                          "feel free to contact us  &#9786; " +
                          ",<br> " +
                          "<a href=\"fitness.weightlossapp @gmail.com\">" +
                          "fitness.weightlossapp@gmail.com</font>" +
                          "</p>" +
                          "</div>" +
                          "</body>";
        message.Body = htmlBody;


        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential("fitness.weightlossapp@gmail.com", "m3leshyFitness21");
        smtp.Send(message);
    }

    public static void SendAdminEmail(string email, string randomPassword)
    {
        MailMessage message = new MailMessage();
        // Reciever's Email
        message.To.Add(email);

        // Email Subject
        message.Subject = "Welcome To FitnessApp";

        // Sender's Email
        message.From = new MailAddress("fitness.weightlossapp@gmail.com", "Fitness App");

        // Email Body
        message.IsBodyHtml = true;
        string htmlBody = "<body>" +
                          "<img src=https://bit.ly/2PI1mx4> " +
                          "<p style=\"float: left; \">" +
                          "<img src=https://bit.ly/2STDZ62 height=\"100px\" width=\"100px\" hspace=\"5\" style=\"border - right: 1px solid black;\">" +
                          "</p> " +
                          "<p style=\"padding: 20px; \">  " +
                          "<font size=\"5px\" color=\"#0F88A8\">" +
                          "<b> Welcome  </b>" +
                          "</font>" +
                          "<br>" +
                          "<b> We are so grateful for having you ! &#9786; </b>" +
                          "<br> <br>" +
                          "<font size =\"3px\"> Please change your password as soon as possible : </font> " +
                          randomPassword +
                          "<br> <br> <br>" +
                          "<em>Best regards,</em>" +
                          "<br>FitnessApp Team </p>" +
                          "</div> " +
                          "</body>";

        message.Body = htmlBody;


        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential("fitness.weightlossapp@gmail.com", "m3leshyFitness21");
        smtp.Send(message);
    }

    ///////////////////////////// User Functions /////////////////////////////

    public static bool IsUserFound(string email, string password)
    {
        return _database.User.Any(user => user.Email == email && user.Password == password);
    }


    public static bool IsEmailTaken(string email)
    {
        return _database.User.Any(x => x.Email == email);
    }

    public static void AddUser(byte[] profilePhoto,
        string firstName, string lastName,
        string email, string password,
        string gender, DateTime birthDate,
        double weight, double height,
        double targetWeight, double kilosToLosePerWeek,
        double workoutsPerWeek, double workoutHoursPerDay)
    {
        _database.User.Add(new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            Gender = gender,
            BirthDate = birthDate,
            Weight = weight,
            Height = height,
            TargetWeight = targetWeight,
            KilosToLosePerWeek = kilosToLosePerWeek,
            WorkoutsPerWeek = workoutsPerWeek,
            WorkoutHoursPerDay = workoutHoursPerDay
        });
        _database.SaveChanges();

        // Sending email to gmails only
        if (email.Contains("gmail"))
            SendUserEmail(email, firstName + " " + lastName);
    }

    public static User? GetUserData(int userId)
    {
        return _database.User.FirstOrDefault(x => x.Id == userId);
    }

    public static void UpdateUserProfile(User currentUser)
    {
        // connection.Open();
        //
        //
        // query = "UPDATE [User] " +
        //         "SET Photo = @Photo, " +
        //         "Height = @Height, " +
        //         "KilosToLosePerWeek = @KilosToLosePerWeek, " +
        //         "WorkoutsPerWeek = @WorkoutsPerWeek, " +
        //         "TargetWeight = @TargetWeight, " +
        //         "WorkoutHoursPerDay = @WorkoutHoursPerDay " +
        //         "WHERE PK_UserID = @UserId";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@Photo", currentUser.ProfilePhoto.ByteArray);
        // command.Parameters.AddWithValue("@Height", currentUser.Height);
        // command.Parameters.AddWithValue("@TargetWeight", currentUser.TargetWeight);
        // command.Parameters.AddWithValue("@KilosToLosePerWeek", currentUser.KilosToLosePerWeek);
        // command.Parameters.AddWithValue("@WorkoutsPerWeek", currentUser.WorkoutsPerWeek);
        // command.Parameters.AddWithValue("@WorkoutHoursPerDay", currentUser.WorkoutHoursPerDay);
        // command.Parameters.AddWithValue("@UserId", currentUser.Id);
        // command.ExecuteNonQuery();
        //
        //
        // query = "INSERT INTO [UserWeight] " +
        //         "VALUES (@UserId, @AddedWeight, GETDATE())";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", currentUser.Id);
        // command.Parameters.AddWithValue("@AddedWeight", currentUser.Weight);
        // command.ExecuteNonQuery();
        //
        //
        // connection.Close();
    }

    public static void UpdateUserAccount(User currentUser)
    {
        // connection.Open();
        //
        // query = "UPDATE [Account] " +
        //         "SET FirstName = @FirstName, " +
        //         "LastName = @LastName, " +
        //         "Email = @Email " +
        //         "WHERE PK_AccountID = @UserId";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@FirstName", currentUser.FirstName);
        // command.Parameters.AddWithValue("@LastName", currentUser.LastName);
        // command.Parameters.AddWithValue("@Email", currentUser.Email);
        // command.Parameters.AddWithValue("@UserId", currentUser.Id);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    public static void UpdateUserPassword(User currentUser)
    {
        // connection.Open();
        //
        // query = "UPDATE [Account] " +
        //         "SET Password = @Password " +
        //         "WHERE PK_AccountID = @UserId";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@Password", currentUser.Password);
        // command.Parameters.AddWithValue("@UserId", currentUser.Id);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    public static void SaveFeedback(int userID, int rating, string feedback)
    {
        // connection.Open();
        //
        // query = "INSERT INTO [Feedback] (FK_Feedback_UserID, FeedbackBody, RatingValue) " +
        //         "VALUES (@userID, @@feedbackBody, @ratingValue)";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", userID);
        // command.Parameters.AddWithValue("@feedbackBody", feedback);
        // command.Parameters.AddWithValue("@ratingValue", rating);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    ///////////////////////////// Challenges Functions /////////////////////////////

    public static List<Challenge> GetAllChallenges(int accountId)
    {
        // // Remove All Overdue Challenges before reading data
        // RemoveOverdueChallenges();
        //
        // List<Challenge> allChallenges = new List<Challenge>();
        //
        // connection.Open();
        //
        // query = "SELECT [Challenge].*,[JoinedChallenge].* " +
        //         "FROM [Challenge] Left JOIN [JoinedChallenge] " +
        //         "ON [Challenge].PK_ChallengeID = [JoinedChallenge].FK_JoinedChallenge_ChallengeID " +
        //         "AND FK_JoinedChallenge_UserID = " + accountID;
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     Challenge temp = new Challenge((int)dataReader["PK_ChallengeID"]);
        //
        //     if (dataReader["Photo"] != DBNull.Value)
        //         temp.Photo.ByteArray = (byte[])dataReader["Photo"];
        //
        //     temp.Name = dataReader["Name"].ToString();
        //     temp.Description = dataReader["Description"].ToString();
        //     temp.TargetMinutes = (int)dataReader["TargetMinutes"];
        //     temp.Reward = dataReader["Reward"].ToString();
        //     temp.DueDate = dataReader["DueDate"].ToString().ToString().Split(' ')[0];
        //     temp.WorkoutType = (int)dataReader["FK_Challenge_WorkoutID"];
        //
        //     if (dataReader["FK_JoinedChallenge_UserID"] != DBNull.Value)
        //         temp.IsJoined = true;
        //     else
        //         temp.IsJoined = false;
        //
        //     allChallenges.Add(temp);
        // }
        //
        // connection.Close();
        //
        // return allChallenges;
        return null;
    }

    public static List<Challenge> GetJoinedChallenges(int accountID)
    {
        // // Remove All Overdue Challenges before reading data
        // RemoveOverdueChallenges();
        //
        // List<Challenge> joinedChallenges = new List<Challenge>();
        //
        // connection.Open();
        //
        // query = "SELECT [Challenge].*,[JoinedChallenge].* " +
        //         "FROM [Challenge] RIGHT JOIN [JoinedChallenge] " +
        //         "ON [Challenge].PK_ChallengeID = [JoinedChallenge].FK_JoinedChallenge_ChallengeID " +
        //         "WHERE FK_JoinedChallenge_UserID = " + accountID;
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     Challenge temp = new Challenge();
        //
        //     temp.Id = (int)dataReader["PK_ChallengeID"];
        //     temp.Name = dataReader["Name"].ToString();
        //     temp.Description = dataReader["Description"].ToString();
        //     temp.TargetMinutes = (int)dataReader["TargetMinutes"];
        //     temp.Reward = dataReader["Reward"].ToString();
        //     temp.DueDate = dataReader["DueDate"].ToString().Split(' ')[0];
        //     temp.WorkoutType = (int)dataReader["FK_Challenge_WorkoutID"];
        //     temp.Progress = (int)dataReader["Progress"];
        //     temp.IsJoined = true;
        //
        //     joinedChallenges.Add(temp);
        // }
        //
        // connection.Close();
        //
        // return joinedChallenges;
        return null;
    }

    public static void JoinChallenge(int accountID, int ChallengeID)
    {
        // connection.Open();
        // query = "INSERT INTO [JoinedChallenge] " +
        //         "(FK_JoinedChallenge_UserID, FK_JoinedChallenge_ChallengeID, JoiningDate, Progress) " +
        //         "VALUES (" + accountID + ", " + ChallengeID + ", GETDATE(), 0)";
        //
        // command = new SqlCommand(query, connection);
        // command.ExecuteReader();
        //
        // connection.Close();
    }

    public static void UnjoinChallenge(int accountID, int ChallengeID)
    {
        // connection.Open();
        // query = "DELETE [JoinedChallenge] " +
        //         "WHERE [JoinedChallenge].FK_JoinedChallenge_UserID = " + accountID + " " +
        //         "AND [JoinedChallenge].FK_JoinedChallenge_ChallengeID = " + ChallengeID;
        //
        // command = new SqlCommand(query, connection);
        // command.ExecuteReader();
        //
        // connection.Close();
    }

    public static void RemoveOverdueChallenges()
    {
        // connection.Open();
        //
        // query = "DELETE [JoinedChallenge] " +
        //         "FROM [Challenge] RIGHT JOIN [JoinedChallenge] " +
        //         "ON [Challenge].PK_ChallengeID = [JoinedChallenge].FK_JoinedChallenge_ChallengeID " +
        //         "WHERE [Challenge].DueDate < GETDATE()";
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        // dataReader.Close();
        //
        // query = "DELETE FROM[Challenge] WHERE[Challenge].DueDate < GETDATE()";
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        //
        // connection.Close();
    }

    public static void UpdateChallengesProgress(int accountID, string workout, double duration)
    {
        // connection.Open();
        //
        // query = "UPDATE [JoinedChallenge] " +
        //         "SET progress += @workoutDuration " +
        //         "FROM [Challenge] RIGHT JOIN [JoinedChallenge] " +
        //         "ON [Challenge].PK_ChallengeID = [JoinedChallenge].FK_JoinedChallenge_ChallengeID " +
        //         "RIGHT JOIN [Workout] " +
        //         "ON [Challenge].Fk_Challenge_WorkoutID = [Workout].PK_WorkoutID " +
        //         "WHERE FK_JoinedChallenge_UserID = @userID " +
        //         "AND GETDATE() BETWEEN JoiningDate AND DueDate " +
        //         "AND [Workout].[Name] = @workoutName";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@workoutDuration", duration);
        // command.Parameters.AddWithValue("@userID", accountID);
        // command.Parameters.AddWithValue("@workoutName", workout);
        // command.ExecuteReader();
        //
        // connection.Close();
    }


    ///////////////////////////// Plans Functions /////////////////////////////

    public static List<Plan> GetPlans(int accountID)
    {
        // connection.Open();
        // query = "SELECT [Plan].*,[User].PK_UserID " +
        //         "FROM [Plan] Left JOIN [User] " +
        //         "ON [Plan].PK_PlanID = [User].FK_User_PlanID " +
        //         "AND PK_UserID = @userID";
        //
        // List<Plan> plansModels = new List<Plan>();
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", accountID);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     Plan temp = new Plan();
        //
        //     temp.Id = (int)dataReader["PK_PlanID"];
        //
        //     if (dataReader["Photo"] != DBNull.Value)
        //         temp.Photo.ByteArray = (byte[])dataReader["Photo"];
        //
        //     temp.Name = dataReader["Name"].ToString();
        //     temp.Description = dataReader["Description"].ToString();
        //     temp.Duration = dataReader["Duration"].ToString();
        //     temp.Hardness = dataReader["Hardness"].ToString();
        //
        //     if (dataReader["PK_UserID"] != DBNull.Value)
        //         temp.IsJoined = true;
        //     else
        //         temp.IsJoined = false;
        //
        //     plansModels.Add(temp);
        // }
        //
        // connection.Close();
        //
        // return plansModels;
        return null;
    }

    public static List<Day> GetPlanDays(int planID)
    {
        // connection.Open();
        // query = "SELECT * FROM [Day] " +
        //         "WHERE [Day].FK_Day_PlanID = " + planID + " " +
        //         "ORDER BY [Day].DayNumber";
        //
        // List<Day> Days = new List<Day>();
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     Day temp = new Day();
        //
        //     temp.DayNumber = (int)dataReader["DayNumber"];
        //     //temp.image
        //     temp.BreakfastDescription = dataReader["BreakfastDescription"].ToString();
        //     temp.LunchDescription = dataReader["LunchDescription"].ToString();
        //     temp.DinnerDescription = dataReader["DinnerDescription"].ToString();
        //     temp.WorkoutDescription = dataReader["WorkoutDescription"].ToString();
        //
        //     Days.Add(temp);
        // }
        //
        // connection.Close();
        //
        // return Days;
        return null;
    }

    public static bool IsInPlan(int accountID)
    {
        // connection.Open();
        // query = "SELECT FK_User_PlanID " +
        //         "FROM [USER] " +
        //         "WHERE PK_UserID = " + accountID;
        //
        // command = new SqlCommand(query, connection);
        //
        // if (command.ExecuteScalar() != DBNull.Value)
        // {
        //     connection.Close();
        //     return true;
        // }
        // else
        // {
        //     connection.Close();
        //     return false;
        // }
        return false;
    }

    public static void JoinPlan(int accountID, int planID)
    {
        // connection.Open();
        // query = "UPDATE [User] " +
        //         "SET FK_User_PlanID = " + planID + ", " +
        //         "PlanJoiningDate = CONVERT(date, GETDATE()) " +
        //         "WHERE [User].PK_UserID = " + accountID;
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        // dataReader.Close();
        //
        // query = "INSERT INTO JoinedPlan (FK_JoinedPlan_UserID, DayNumber) VALUES (@userID, 1)";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", accountID);
        // dataReader = command.ExecuteReader();
        //
        // connection.Close();
    }

    public static void UnjoinPlan(int accountID)
    {
        // connection.Open();
        // query = "UPDATE [User] " +
        //         "SET FK_User_PlanID = NULL, " +
        //         "PlanJoiningDate = NULL " +
        //         "WHERE [User].PK_UserID = " + accountID;
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        // dataReader.Close();
        //
        // query = "DELETE FROM JoinedPlan WHERE FK_JoinedPlan_UserID = @userID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", accountID);
        // dataReader = command.ExecuteReader();
        //
        // connection.Close();
    }


    ///////////////////////////// Joined Plan Functions /////////////////////////////


    public static int GetJoinedPlanID(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT FK_User_PlanID " +
        //         "FROM [User] " +
        //         "WHERE PK_UserID = @accountID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // int planID = (int)command.ExecuteScalar();
        //
        // connection.Close();
        //
        // return planID;
        return 0;
    }

    public static string GetJoinedPlanName(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT Name " +
        //         "FROM [Plan] INNER JOIN [User] " +
        //         "ON PK_PlanID = FK_User_PlanID " +
        //         "WHERE PK_UserID = @userID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", accountID);
        // string planName = (string)command.ExecuteScalar();
        //
        // connection.Close();
        //
        // return planName;
        return null;
    }

    public static int GetJoinedPlanDayNumber(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT DATEDIFF(day, PlanJoiningDate , GETDATE()) " +
        //         "FROM [User] " +
        //         "WHERE PK_UserID = @userID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", accountID);
        //
        // int dayNumber = (int)command.ExecuteScalar() + 1;
        //
        // connection.Close();
        //
        // return dayNumber;
        return 0;
    }


    public static string GetDayBreakfastDescription(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT BreakfastDescription " +
        //         "FROM Day INNER JOIN[User] " +
        //         "ON FK_Day_PlanID = FK_User_PlanID " +
        //         "WHERE PK_UserID = @accountID " +
        //         "AND DayNumber = DATEDIFF(day, PlanJoiningDate, GETDATE()) + 1";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // string breakfastDescription = command.ExecuteScalar().ToString();
        //
        // connection.Close();
        //
        // return breakfastDescription;
        return null;
    }

    public static string GetDayLunchDescription(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT LunchDescription " +
        //         "FROM Day INNER JOIN[User] " +
        //         "ON FK_Day_PlanID = FK_User_PlanID " +
        //         "WHERE PK_UserID = @accountID " +
        //         "AND DayNumber = DATEDIFF(day, PlanJoiningDate, GETDATE()) + 1";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // string lunchDescription = command.ExecuteScalar().ToString();
        //
        // connection.Close();
        //
        // return lunchDescription;
        return null;
    }

    public static string GetDayDinnerDescription(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT DinnerDescription " +
        //         "FROM Day INNER JOIN[User] " +
        //         "ON FK_Day_PlanID = FK_User_PlanID " +
        //         "WHERE PK_UserID = @accountID " +
        //         "AND DayNumber = DATEDIFF(day, PlanJoiningDate, GETDATE()) + 1";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // string dinnerDescription = command.ExecuteScalar().ToString();
        //
        // connection.Close();
        //
        // return dinnerDescription;
        return null;
    }

    public static string GetDayWorkoutDescription(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT WorkoutDescription " +
        //         "FROM Day INNER JOIN[User] " +
        //         "ON FK_Day_PlanID = FK_User_PlanID " +
        //         "WHERE PK_UserID = @accountID " +
        //         "AND DayNumber = DATEDIFF(day, PlanJoiningDate, GETDATE()) + 1";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // string WorkoutDescription = command.ExecuteScalar().ToString();
        //
        // connection.Close();
        //
        // return WorkoutDescription;
        return null;
    }


    public static bool GetDayBreakfastStatus(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT IsBreakfastDone " +
        //         "FROM[JoinedPlan] INNER JOIN[User] " +
        //         "ON FK_JoinedPlan_UserID = PK_UserID " +
        //         "WHERE FK_JoinedPlan_UserID = @accountID " +
        //         "AND DayNumber = DATEDIFF(day, PlanJoiningDate, GETDATE()) + 1";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // bool isBreakfastChecked = Convert.ToBoolean(command.ExecuteScalar());
        //
        // connection.Close();
        //
        // return isBreakfastChecked;
        return false;
    }

    public static bool GetDayLunchStatus(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT IsLunchDone " +
        //         "FROM[JoinedPlan] INNER JOIN[User] " +
        //         "ON FK_JoinedPlan_UserID = PK_UserID " +
        //         "WHERE FK_JoinedPlan_UserID = @accountID " +
        //         "AND DayNumber = DATEDIFF(day, PlanJoiningDate, GETDATE()) + 1";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // bool isLunchChecked = Convert.ToBoolean(command.ExecuteScalar());
        //
        // connection.Close();
        //
        // return isLunchChecked;
        return false;
    }

    public static bool GetDayDinnerStatus(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT IsDinnerDone " +
        //         "FROM[JoinedPlan] INNER JOIN[User] " +
        //         "ON FK_JoinedPlan_UserID = PK_UserID " +
        //         "WHERE FK_JoinedPlan_UserID = @accountID " +
        //         "AND DayNumber = DATEDIFF(day, PlanJoiningDate, GETDATE()) + 1";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // bool isDinnerChecked = Convert.ToBoolean(command.ExecuteScalar());
        //
        // connection.Close();
        //
        // return isDinnerChecked;
        return false;
    }

    public static bool GetDayWorkoutStatus(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT IsWorkoutsDone " +
        //         "FROM[JoinedPlan] INNER JOIN[User] " +
        //         "ON FK_JoinedPlan_UserID = PK_UserID " +
        //         "WHERE FK_JoinedPlan_UserID = @accountID " +
        //         "AND DayNumber = DATEDIFF(day, PlanJoiningDate, GETDATE()) + 1";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // bool isWorkoutsChecked = Convert.ToBoolean(command.ExecuteScalar());
        //
        // connection.Close();
        //
        // return isWorkoutsChecked;
        return false;
    }


    public static void UpdatePlanDayNumber(int accountID, int dayNumber)
    {
        // connection.Open();
        //
        // query = "SELECT DayNumber " +
        //         "FROM [JoinedPlan] " +
        //         "WHERE FK_JoinedPlan_UserID = @userID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", accountID);
        //
        // if ((int)command.ExecuteScalar() != dayNumber)
        // {
        //     query = "UPDATE [JoinedPlan] " +
        //             "SET DayNumber   = @dayNumber, " +
        //             "IsBreakfastDone = 0, " +
        //             "IsLunchDone     = 0, " +
        //             "IsDinnerDone    = 0, " +
        //             "IsWorkoutsDone  = 0 " +
        //             "WHERE FK_JoinedPlan_UserID = @userID";
        //
        //     command = new SqlCommand(query, connection);
        //     command.Parameters.AddWithValue("@dayNumber", dayNumber);
        //     command.Parameters.AddWithValue("@userID", accountID);
        //
        //     dataReader = command.ExecuteReader();
        // }
        //
        // connection.Close();
    }

    public static void UpdateDayBreakfastStatus(bool checkedBreakfast, int accountID)
    {
        // connection.Open();
        //
        // query = "UPDATE [JoinedPlan] " +
        //         "SET IsBreakfastDone = @checkedBreakfast " +
        //         "WHERE FK_JoinedPlan_UserID = @accountID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@checkedBreakfast", checkedBreakfast);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    public static void UpdateDayLunchStatus(bool checkedLunch, int accountID)
    {
        // connection.Open();
        //
        // query = "UPDATE [JoinedPlan] " +
        //         "SET IsLunchDone = @checkedLunch " +
        //         "WHERE FK_JoinedPlan_UserID = @accountID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@checkedLunch", checkedLunch);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    public static void UpdateDayDinnerStatus(bool checkedDinner, int accountID)
    {
        // connection.Open();
        //
        // query = "UPDATE [JoinedPlan] " +
        //         "SET IsDinnerDone = @checkedDinner " +
        //         "WHERE FK_JoinedPlan_UserID = @accountID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@checkedDinner", checkedDinner);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    public static void UpdateDayWorkoutStatus(bool checkedWorkout, int accountID)
    {
        // int planDay = GetJoinedPlanDayNumber(accountID);
        //
        // connection.Open();
        //
        // query = "UPDATE [JoinedPlan] " +
        //         "SET IsWorkoutsDone = @checkedWorkout " +
        //         "WHERE FK_JoinedPlan_UserID = @accountID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@checkedWorkout", checkedWorkout);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }


    ///////////////////////////// Weight Functions /////////////////////////////

    public static List<double> GetWeightValues(int accountID)
    {
        // List<double> weightValues = new List<double>();
        //
        // connection.Open();
        //
        // SqlCommand CommandString =
        //     new SqlCommand(
        //         "SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID = @userID ORDER BY Timestamp DESC",
        //         connection);
        // CommandString.CommandType = CommandType.Text;
        // CommandString.Parameters.AddWithValue("@userID", accountID);
        // dataReader = CommandString.ExecuteReader();
        //
        // for (int i = 0; dataReader.Read() && i < 10; i++)
        // {
        //     weightValues.Add((double)dataReader["Weight"]);
        // }
        //
        // connection.Close();
        //
        // // Reverse List
        // weightValues.Reverse();
        //
        // return weightValues;
        return null;
    }

    public static List<string> GetWeightDateValues(int accountID)
    {
        // List<string> dateValues = new List<string>();
        //
        // connection.Open();
        //
        // SqlCommand CommandString =
        //     new SqlCommand(
        //         "SELECT FORMAT(Timestamp, 'MMM yy') AS [Timestamp] FROM UserWeight WHERE FK_UserWeight_UserID = @userID ORDER BY Timestamp DESC",
        //         connection);
        // CommandString.CommandType = CommandType.Text;
        // CommandString.Parameters.AddWithValue("@userID", accountID);
        //
        // dataReader = CommandString.ExecuteReader();
        //
        // for (int i = 0; dataReader.Read() && i < 10; i++)
        // {
        //     dateValues.Add((string)dataReader["Timestamp"]);
        // }
        //
        // connection.Close();
        //
        // // Reverse List
        // dateValues.Reverse();
        //
        // return dateValues;
        return null;
    }

    public static void AddNewWeight(double NewWeight, int accountID)
    {
        // connection.Open();
        //
        // query = "INSERT INTO [UserWeight] " +
        //         "VALUES (@userID, @AddedWeight, GETDATE())";
        // command = new SqlCommand(query, connection);
        // command.Parameters.Add(new SqlParameter("@userID", accountID));
        // command.Parameters.Add(new SqlParameter("@AddedWeight", NewWeight));
        // command.ExecuteReader();
        //
        // connection.Close();
    }

    public static double GetTotalWeightLostPerDuration(int accountID, string duration)
    {
        // double weightLost = 0;
        // List<double> weights = new List<double>();
        //
        // connection.Open();
        //
        // query = "SELECT Weight " +
        //         "FROM [UserWeight] WHERE FK_UserWeight_UserID = @id " +
        //         "AND DATEPART(" + duration + ", Timestamp) = DATEPART(" + duration + ", GETDATE()) " +
        //         "ORDER BY Timestamp";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@id", accountID);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     weights.Add((double)(dataReader["Weight"]));
        // }
        //
        //
        // connection.Close();
        //
        // for (int i = 0; i < (weights.Count - 1); i++)
        // {
        //     weightLost += (weights[i] - weights[i + 1]);
        // }
        //
        //
        // return Math.Round(weightLost, 2);
        return 0;
    }

    public static double GetAverageWeightLostPerDuration(int accountID, string duration)
    {
        // double weightLost = 0;
        // List<double> weights = new List<double>();
        //
        // connection.Open();
        //
        // query = "SELECT Weight " +
        //         "FROM [UserWeight] WHERE FK_UserWeight_UserID = @id " +
        //         "AND DATEPART(" + duration + ", Timestamp) = DATEPART(" + duration + ", GETDATE()) " +
        //         "ORDER BY Timestamp";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@id", accountID);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     weights.Add((double)(dataReader["Weight"]));
        // }
        //
        //
        // connection.Close();
        //
        // for (int i = 0; i < (weights.Count - 1); i++)
        // {
        //     weightLost += (weights[i] - weights[i + 1]);
        // }
        //
        // return Math.Round((weightLost / weights.Count), 2);
        return 0;
    }


    ///////////////////////////// Motivational Quote Functions /////////////////////////////

    public static string GetMotivationalQuote()
    {
        // connection.Open();
        //
        // query = "SELECT Quote " +
        //         "FROM [MotivationalQuote] " +
        //         "WHERE PK_MotivationalQuoteID = DATEPART(DAY,GETDATE())";
        //
        // command = new SqlCommand(query, connection);
        //
        // string Quote = command.ExecuteScalar().ToString();
        //
        // connection.Close();
        //
        // return Quote;
        return null;
    }


    ///////////////////////////// Calories Functions /////////////////////////////

    public static double GetCaloriesGainedToday(int accountID)
    {
        // double caloriesGained = 0;
        //
        // connection.Open();
        //
        // query = "SELECT SUM(CaloriesGained) " +
        //         "FROM UserFood " +
        //         "WHERE FK_UserFood_UserID = @id " +
        //         "AND CONVERT (date, Timestamp) = CONVERT (date, GETDATE())";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@id", accountID);
        //
        // if (command.ExecuteScalar() != DBNull.Value)
        //     caloriesGained = (double)command.ExecuteScalar();
        //
        // connection.Close();
        //
        // return caloriesGained;
        return 0;
    }

    public static double GetCaloriesLostToday(int accountID)
    {
        // double caloriesLost = 0;
        //
        // connection.Open();
        //
        // query = "SELECT SUM(CaloriesLost) " +
        //         "FROM UserWorkout " +
        //         "WHERE FK_UserWorkout_UserID = @id " +
        //         "AND CONVERT (date, Timestamp) = CONVERT (date, GETDATE())";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@id", accountID);
        //
        // if (command.ExecuteScalar() != DBNull.Value)
        //     caloriesLost = (double)command.ExecuteScalar();
        //
        // connection.Close();
        //
        // return caloriesLost;
        return 0;
    }


    ///////////////////////////// Food/Workout Functions /////////////////////////////

    public static List<String> GetAllFood()
    {
        // List<String> food = new List<string>();
        //
        // connection.Open();
        //
        // query = "SELECT Name " +
        //         "FROM [Food]";
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     food.Add(dataReader["Name"].ToString());
        // }
        //
        // connection.Close();
        //
        // return food;
        return null;
    }

    public static List<String> GetAllWorkouts()
    {
        // List<String> workouts = new List<string>();
        //
        // connection.Open();
        // query = "SELECT Name " +
        //         "FROM [Workout]";
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     workouts.Add(dataReader["Name"].ToString());
        // }
        //
        // connection.Close();
        //
        // return workouts;
        return null;
    }


    public static int GetFoodID(string foodName)
    {
        // connection.Open();
        //
        // query = "SELECT PK_FoodID " +
        //         "FROM [Food] " +
        //         "WHERE Name = @foodName";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@foodName", foodName);
        // int id = (int)command.ExecuteScalar();
        //
        // connection.Close();
        //
        // return id;
        return 0;
    }

    public static int GetWorkoutID(string workoutName)
    {
        // connection.Open();
        //
        // query = "SELECT PK_WorkoutID " +
        //         "FROM [Workout] " +
        //         "WHERE Name = @name;";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@name", workoutName);
        // int id = (int)command.ExecuteScalar();
        //
        // connection.Close();
        //
        // return id;
        return 0;
    }


    public static void AddFood(string foodName, double quantity, int accountID)
    {
        // int foodID = GetFoodID(foodName);
        // double totalCaloriesGained = 0;
        //
        // connection.Open();
        //
        // query = "SELECT Type " +
        //         "FROM [Food] " +
        //         "WHERE Name = @foodName";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@foodName", foodName);
        //
        // switch (command.ExecuteScalar().ToString())
        // {
        //     case "Protein":
        //         totalCaloriesGained = 4 * quantity;
        //         break;
        //
        //     case "Fats":
        //         totalCaloriesGained = 9 * quantity;
        //         break;
        //
        //     case "Carbohydrates":
        //         totalCaloriesGained = 4 * quantity;
        //         break;
        // }
        //
        //
        // query = "INSERT INTO [UserFood] (FK_UserFood_UserID, FK_UserFood_FoodID, CaloriesGained,Timestamp) " +
        //         "VALUES (@userID, @foodID, @calories, GETDATE())";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", accountID);
        // command.Parameters.AddWithValue("@foodID", foodID);
        // command.Parameters.AddWithValue("@calories", totalCaloriesGained);
        // command.ExecuteReader();
        //
        // connection.Close();
    }

    public static void AddWorkout(string workoutName, double duration, User currentUser)
    {
        // int workoutID = GetWorkoutID(workoutName);
        // double totalCaloriesLost = 0;
        //
        // if (currentUser.Gender == "Female")
        // {
        //     totalCaloriesLost = ((currentUser.Age * 0.074) - (currentUser.Weight * 0.05741)
        //         + (100 * 0.4472) - 20.4022) * (duration / 4.184);
        // }
        // else
        // {
        //     totalCaloriesLost = ((currentUser.Age * 0.2017) - (currentUser.Weight * 0.09036)
        //         + (100 * 0.6309) - 55.0969) * (duration / 4.184);
        // }
        //
        // connection.Open();
        //
        // query = "INSERT INTO UserWorkout (FK_UserWorkout_UserID, FK_UserWorkout_WorkoutID, " +
        //         "MinutesOfWork, CaloriesLost, Timestamp) " +
        //         "VALUES(@userID, @workoutID, @duration, @calories, GETDATE())";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@userID", currentUser.Id);
        // command.Parameters.AddWithValue("@workoutID", workoutID);
        // command.Parameters.AddWithValue("@duration", duration);
        // command.Parameters.AddWithValue("@calories", Math.Round(totalCaloriesLost, 2));
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }


    ///////////////////////////// Admin Functions /////////////////////////////


    public static Admin GetAdminData(int adminID)
    {
        // Admin currentAdmin = new Admin();
        //
        // connection.Open();
        //
        // query = "SELECT * " +
        //         "FROM [Account] " +
        //         "WHERE PK_AccountID = @adminID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@adminID", adminID);
        // dataReader = command.ExecuteReader();
        // dataReader.Read();
        //
        // currentAdmin.FirstName = dataReader["FirstName"].ToString();
        // currentAdmin.LastName = dataReader["LastName"].ToString();
        // currentAdmin.Email = dataReader["Email"].ToString();
        // currentAdmin.Password = dataReader["Password"].ToString();
        //
        // connection.Close();
        //
        // return currentAdmin;
        return null;
    }

    public static void UpdateAdminAccount(Admin currentAdmin)
    {
        // connection.Open();
        //
        // query = "UPDATE [Admin] " +
        //         "SET FirstName = @FirstName, " +
        //         "LastName = @LastName, " +
        //         "Email = @Email " +
        //         "WHERE PK_AdminID = @AdminId";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@FirstName", currentAdmin.FirstName);
        // command.Parameters.AddWithValue("@LastName", currentAdmin.LastName);
        // command.Parameters.AddWithValue("@Email", currentAdmin.Email);
        // command.Parameters.AddWithValue("@AdminId", currentAdmin.Id);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    public static void UpdateAdminPassword(Admin currentAdmin)
    {
        // connection.Open();
        //
        // query = "UPDATE Account " +
        //         "SET Password = @newPassword, " +
        //         "Type = @type " +
        //         "WHERE PK_AccountID = @accountID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@newPassword", currentAdmin.Password);
        // command.Parameters.AddWithValue("@type", "Admin");
        // command.Parameters.AddWithValue("@accountID", currentAdmin.Id);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    public static void AddNewAdmin(string firstName, string lastName, string email)
    {
        // connection.Open();
        //
        // string password = GenerateRandomPassword();
        //
        // // Insert Account Info
        // query = "INSERT INTO [Account] (FirstName, LastName, Email, Password, Type) " +
        //         "VALUES (@firstName, @lastName, @email, @password, @accountType)";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@firstName", firstName);
        // command.Parameters.AddWithValue("@lastName", lastName);
        // command.Parameters.AddWithValue("@email", email);
        // command.Parameters.AddWithValue("@password", EncryptPassword(password));
        // command.Parameters.AddWithValue("@accountType", "Admin*");
        // command.ExecuteNonQuery();
        //
        // connection.Close();


        // Sending email to gmails only
        // if (email.Contains("gmail"))
        //     SendAdminEmail(email, password);
    }

    public static List<int> GetAppRatingValues()
    {
        // connection.Open();
        //
        // List<int> ratingList = new List<int>();
        //
        // for (int i = 1; i <= 5; i++)
        // {
        //     query = "SELECT COUNT(FK_Feedback_UserID) " +
        //             "FROM [Feedback] " +
        //             "WHERE RatingValue = @ratingValue";
        //
        //     command = new SqlCommand(query, connection);
        //     command.Parameters.AddWithValue("@ratingValue", i);
        //
        //     ratingList.Add((int)command.ExecuteScalar());
        // }
        //
        // connection.Close();
        //
        // return ratingList;
        return null;
    }

    public static List<Feedback> GetFeedbacks()
    {
        return null;
        // List<Feedback> allFeedbacks = new List<Feedback>();
        //
        // connection.Open();
        //
        // query = "SELECT FirstName, LastName, FeedbackBody " +
        //         "FROM [Account] RIGHT JOIN [Feedback] " +
        //         "ON PK_AccountID  = FK_Feedback_UserID";
        //
        // command = new SqlCommand(query, connection);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     if (!string.IsNullOrWhiteSpace(dataReader["FeedbackBody"].ToString()))
        //     {
        //         Feedback temp = new Feedback();
        //
        //         temp.FirstName = dataReader["FirstName"].ToString();
        //         temp.LastName = dataReader["LastName"].ToString();
        //         temp.Feedback = dataReader["FeedbackBody"].ToString();
        //
        //         allFeedbacks.Add(temp);
        //     }
        // }
        //
        // connection.Close();
        //
        // return allFeedbacks;
    }

    public static void DeleteFeedback(string feedbackBody)
    {
        // connection.Open();
        //
        // query = "DELETE FROM [Feedback] " +
        //         "WHERE FeedbackBody LIKE @feedbackBody";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@feedbackBody", feedbackBody);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }

    public static int GetAppUsersNumber()
    {
        // connection.Open();
        //
        // query = "SELECT COUNT(*) " +
        //         "FROM [User]";
        //
        // command = new SqlCommand(query, connection);
        // int appUsersNumber = (int)command.ExecuteScalar();
        //
        // connection.Close();
        //
        // return appUsersNumber;
        return 0;
    }

    public static bool IsNewAdmin(int accountID)
    {
        // connection.Open();
        //
        // query = "SELECT Type " +
        //         "FROM [Account] " +
        //         "WHERE PK_AccountID = @accountID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        //
        // dataReader = command.ExecuteReader();
        // dataReader.Read();
        // string type = (string)dataReader["Type"];
        //
        // connection.Close();
        //
        // if (type == "Admin*")
        //     return true;
        // else
        //     return false;
        return false;
    }


    public static void AddNewChallenge(byte[] photo, string name, string description, int targetMinutes,
        string reward, DateTime? dueDate, int workoutID)
    {
        // connection.Open();
        //
        // query =
        //     "INSERT INTO [Challenge] (Photo, Name, Description, TargetMinutes, Reward, DueDate, Fk_Challenge_WorkoutID) " +
        //     "VALUES (@Photo, @Name, @Description, @TargetMinutes, @Reward, @DueDate, @WorkoutID)";
        //
        // command = new SqlCommand(query, connection);
        //
        // if (photo == null)
        //     command.Parameters.Add("@Photo", SqlDbType.Image).Value = DBNull.Value;
        // else
        //     command.Parameters.AddWithValue("@Photo", photo);
        //
        // command.Parameters.AddWithValue("@Name", name);
        // command.Parameters.AddWithValue("@Description", description);
        // command.Parameters.AddWithValue("@TargetMinutes", targetMinutes);
        // command.Parameters.AddWithValue("@Reward", reward);
        // command.Parameters.AddWithValue("@DueDate", dueDate);
        // command.Parameters.AddWithValue("@WorkoutID", workoutID);
        // command.ExecuteReader();
        //
        // connection.Close();
    }

    public static void DeleteChallenge(int challengeID)
    {
        // connection.Open();
        //
        // query = "DELETE [JoinedChallenge] " +
        //         "FROM [Challenge] RIGHT JOIN [JoinedChallenge] " +
        //         "ON [Challenge].PK_ChallengeID = [JoinedChallenge].FK_JoinedChallenge_ChallengeID " +
        //         "WHERE [Challenge].PK_ChallengeID = @challengeID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@challengeID", challengeID);
        // command.ExecuteNonQuery();
        //
        //
        // query = "DELETE FROM [Challenge] " +
        //         "WHERE [Challenge].PK_ChallengeID = @challengeID";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@challengeID", challengeID);
        // command.ExecuteNonQuery();
        //
        // connection.Close();
    }


    public static List<User> SearchForUser(string search)
    {
        return null;
        // List<User> allUsers = new List<User>();
        //
        // connection.Open();
        //
        // query = "SELECT PK_UserID, Photo, FirstName, LastName, Email " +
        //         "FROM [User] INNER JOIN [Account] " +
        //         "ON PK_UserID = PK_AccountID " +
        //         "WHERE FirstName LIKE '%' + @search + '%' " +
        //         "OR LastName LIKE '%' + @search + '%' " +
        //         "OR Email LIKE '%' + @search + '%'";
        //
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@search", search);
        // dataReader = command.ExecuteReader();
        //
        // while (dataReader.Read())
        // {
        //     User temp = new User();
        //
        //     temp.Id = (int)dataReader["PK_UserID"];
        //
        //     if (dataReader["Photo"] != DBNull.Value)
        //         temp.ProfilePhoto.ByteArray = (byte[])dataReader["Photo"];
        //
        //     temp.FirstName = dataReader["FirstName"].ToString();
        //     temp.LastName = dataReader["LastName"].ToString();
        //     temp.Email = dataReader["Email"].ToString();
        //
        //     allUsers.Add(temp);
        // }
        //
        // connection.Close();
        //
        // return allUsers;
    }

    public static void DeleteUser(int accountID)
    {
        // connection.Open();
        //
        // query = "DELETE FROM [Feedback] WHERE FK_Feedback_UserID = @accountID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        //
        // query = "DELETE FROM UserWorkout WHERE FK_UserWorkout_UserID = @accountID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        //
        // query = "DELETE FROM [JoinedChallenge] WHERE FK_JoinedChallenge_UserID = @accountID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        //
        // query = "DELETE FROM [JoinedPlan] WHERE FK_JoinedPlan_UserID = @accountID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        //
        // query = "DELETE FROM [UserWeight] WHERE FK_UserWeight_UserID = @accountID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        //
        // query = "DELETE FROM [UserFood] WHERE FK_UserFood_UserID = @accountID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        //
        // query = "DELETE FROM [User] WHERE PK_UserID = @accountID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        //
        // query = "DELETE FROM [Account] WHERE PK_AccountID = @accountID";
        // command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@accountID", accountID);
        // command.ExecuteNonQuery();
        //
        //
        // connection.Close();
    }
}