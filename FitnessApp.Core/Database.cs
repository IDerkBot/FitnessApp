using FitnessApp.DataAccessLayer;
using FitnessApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FitnessApp.Core;

public class Database
{
    #region Variables

    private readonly ApplicationContext _context;
    
    public User? SignedUser { get; set; }
    public int AccountId { get; set; }

    public AccountAccess? AccountType { get; set; }
    
    public bool IsConnected { get; set; }

    #endregion

    #region Constructor

    public Database()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("AppSettings.json");

        var config = builder.Build();
        var connectionString = config.GetConnectionString("TestConnection");
        // var connectionString = config.GetConnectionString("Server");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));

        var options = optionsBuilder.UseSqlServer(connectionString).Options;
        // var options = optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(5,5,62))).Options;

        _context = new ApplicationContext(options);
        IsConnected = _context.IsConnected;
    }

    #endregion

    #region Methods

    #region Users

    #region Exists

    public bool UserExists(string login)
    {
        return _context.Users.Any(user => user.Login == login);
    }

    public bool UserExists(string login, string password)
    {
        return _context.Users.Any(user => user.Login == login && user.Password == password);
    }

    #endregion

    #region Get

    public IEnumerable<User> GetUsers()
    {
        return _context.Users;
    }

    public User? GetUserById(int id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    public User? GetUserByLogin(string login)
    {
        return _context.Users.FirstOrDefault(x => x.Login == login);
    }

    #endregion

    #region Add

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        // Sending email to gmails only
        // if (email.Contains("gmail"))
        //     SendUserEmail(email, firstName + " " + lastName);
    }

    public void AddUser(string login, string password, byte access = 0)
    {
        _context.Users.Add(new User
        {
            Login = login,
            Password = password,
            Access = access
        });
        _context.SaveChanges();

        // Sending email to gmails only
        // if (email.Contains("gmail"))
        //     SendUserEmail(email, firstName + " " + lastName);
    }

    #endregion

    #region Update

    public void UpdateUser(User currentUser)
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

    public void UpdateUserPassword(User? currentUser)
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

    #endregion

    #region Delete

    public void DeleteUser(int id)
    {
        var user = GetUserById(id);
        if(user == null) return;

        var person = GetPersonByUserId(id);
        if(person == null) return;
        
        // TODO Get JoinedChallenge
        // TODO Get JoinedPlan
        // TODO Get PersonFood
        // TODO Get PersonWorkout
        
        // TODO Delete JoinedChallenge
        // TODO Delete JoinedPlan
        // TODO Delete PersonFood
        // TODO Delete PersonWorkout
    }

    #endregion

    #region Search

    public IEnumerable<User> SearchForUser(string search)
    {
        return _context.Users.Where(x => x.Login.Contains(search));
    }

    #endregion

    #endregion

    #region Challenges

    #region Get

    public IEnumerable<Challenge> GetChallengesByPersonId(int userId)
    {
        return _context.JoinedChallenges.Where(x => x.PersonId == userId).Select(x => x.Challenge);
    }

    #endregion

    #region Add

    public void AddNewChallenge(byte[] photo, string name, string description, int targetMinutes,
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

    #endregion

    #region Update

    public void UpdateChallengesProgress(int userId, string workout, double duration)
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

    #endregion

    #region Delete

    public void DeleteChallenge(int id)
    {
        var challenge = _context.Challenges.FirstOrDefault(x => x.Id == id);
        if (challenge != null)
            _context.Challenges.Remove(challenge);
    }

    public void DeleteChallenge(Challenge challenge)
    {
        _context.Challenges.Remove(challenge);
    }

    #endregion

    ///////////////////////////////////////////////// SEARCH /////////////////////////////////////////////////


    /// <summary>
    /// Удалить законченные вызовы
    /// </summary>
    public void RemoveOverdueChallenges()
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

    #endregion

    #region JoinedChallenge
    
    ///////////////////////////////////////////////// EXISTS /////////////////////////////////////////////////
    
    #region Get

    public List<JoinedChallenge> GetJoinedChallenges(int accountId)
    {
        // Remove All Overdue Challenges before reading data
        RemoveOverdueChallenges();

        List<JoinedChallenge> joinedChallenges = new List<JoinedChallenge>();

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
        //     Challenge temp = new Challenge
        //     {
        //         Id = (int)dataReader["PK_ChallengeID"],
        //         Name = dataReader["Name"].ToString(),
        //         Description = dataReader["Description"].ToString(),
        //         TargetMinutes = (int)dataReader["TargetMinutes"],
        //         Reward = dataReader["Reward"].ToString(),
        //         DueDate = dataReader["DueDate"].ToString().Split(' ')[0],
        //         WorkoutType = (int)dataReader["FK_Challenge_WorkoutID"],
        //         Progress = (int)dataReader["Progress"],
        //         IsJoined = true
        //     };
        //
        //     joinedChallenges.Add(temp);
        // }
        //
        // connection.Close();

        return joinedChallenges;
    }

    #endregion
    
    #region Add

    public void JoinChallenge(int userId, int challengeId)
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
    
    public void JoinChallenge(User user, Challenge challenge)
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

    #endregion
    
    ///////////////////////////////////////////////// UPDATE /////////////////////////////////////////////////

    #region Delete

    public void LeaveChallenge(int accountId, int challengeId)
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

    #endregion
    
    ///////////////////////////////////////////////// SEARCH /////////////////////////////////////////////////

    #endregion JoinedChallenge

    #region Plans

    #region Exists

    /// <summary>
    /// Проверяет, есть ли присоединенные "Планы" для пользователя 
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    public bool HavePlans(int accountId)
    {
        return _context.JoinedPlans.Any(x => x.Person.UserId == accountId);
    }

    #endregion
    
    #region Get

    public IEnumerable<Plan> GetPlans()
    {
        return _context.Plans;
    }

    public IEnumerable<DayInPlan> GetPlanDays(int planId)
    {
        return _context.DayInPlans.Where(x => x.PlanId == planId);
    }
    
    public Plan? GetPlanById(int planId)
    {
        return _context.Plans.FirstOrDefault(x => x.Id == planId);
    }

    #endregion

    #region Add

    /// <summary>
    /// Присоединяет к плану
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="planId"></param>
    public void JoinPlan(int accountId, int planId)
    {
        var user = GetUserById(accountId);
        var plan = GetPlanById(planId);
        var person = GetPersonByUserId(accountId);
        
        if(user == null || plan == null || person == null) return;
        
        var joinedPlan = new JoinedPlan();

        joinedPlan.Person = person;
        joinedPlan.PersonId = person.Id;

        joinedPlan.Plan = plan;
        joinedPlan.PlanId = plan.Id;

        _context.JoinedPlans.Add(joinedPlan);
        _context.SaveChanges();
    }

    #endregion

    #region Leave

    public void LeavePlan(int accountId, int planId)
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

    #endregion

    #region Joined Plan

    #region Get

    public IEnumerable<Plan> GetJoinedPlanByUserId(int userId)
    {
        return _context.JoinedPlans.Where(x => x.Person.UserId == userId).Select(x => x.Plan);
    }

    public IEnumerable<Plan> GetJoinedPlanByName(string name)
    {
        return _context.JoinedPlans.Where(x => x.Plan.Name == name).Select(x => x.Plan);
    }

    public int GetJoinedPlanDayNumber(int userId)
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

    #endregion
    
    public string GetDayBreakfastDescription(int userId)
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

    public string GetDayLunchDescription(int userId)
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

    public string GetDayDinnerDescription(int userId)
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

    public string GetDayWorkoutDescription(int userId)
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
    
    
    public bool GetDayBreakfastStatus(int userId)
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

    public bool GetDayLunchStatus(int userId)
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

    public bool GetDayDinnerStatus(int userId)
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

    public bool GetDayWorkoutStatus(int userId)
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
    
    
    public void UpdatePlanDayNumber(int userId, int dayNumber)
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

    public void UpdateDayBreakfastStatus(bool checkedBreakfast, int userId)
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

    public void UpdateDayLunchStatus(bool checkedLunch, int userId)
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

    public void UpdateDayDinnerStatus(bool checkedDinner, int userId)
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

    public void UpdateDayWorkoutStatus(bool checkedWorkout, int userId)
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

    #endregion

    #endregion

    #region Persons

    #region Get

    public Person? GetPersonByUserId(int userId)
    {
        return _context.Persons.FirstOrDefault(x => x.User.Id == userId);
    }

    #endregion
    
    #region Add

    public void AddPerson(Person person)
    {
        _context.Persons.Add(person);
        _context.SaveChanges();
    }

    #endregion

    #region Update

    public void UpdatePersonProfile(Person? currentUser)
    {
        _context.SaveChanges();
    }

    #endregion

    #endregion

    #region Workout

    #region Get

    public IEnumerable<Workout> GetWorkouts()
    {
        return _context.Workouts;
    }
    
    public IEnumerable<Workout> GetWorkoutsById(int workoutId)
    {
        return _context.Workouts.Where(x => x.Id == workoutId);
    }

    #endregion

    #region Add

    public void AddWorkout(string workoutName, double duration, User? currentUser)
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

    #endregion

    #endregion Workout

    #region Food

    #region Get

    /// <summary>
    /// Получить все "продукты" из базы данных
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Food> GetFoods()
    {
        return _context.Foods;
    }

    /// <summary>
    /// Получить "продукт" по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Food? GetFoodById(int id)
    {
        return _context.Foods.FirstOrDefault(x => x.Id == id);
    }

    /// <summary>
    /// Получить "продукт" по имени
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Food? GetFoodByName(string name)
    {
        return _context.Foods.FirstOrDefault(x => x.Name == name);
    }

    /// <summary>
    /// Получить все записи из таблицы "Продукты" по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IEnumerable<Food> GetFoodsById(int id)
    {
        return _context.Foods.Where(x => x.Id == id);
    }
    
    /// <summary>
    /// Получить все записи из таблицы "Продукты" по имени
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<Food> GetFoodsByName(string name)
    {
        return _context.Foods.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    #endregion

    #region Add

    /// <summary>
    /// Добавляет запись в таблицу "Продукты"
    /// </summary>
    /// <param name="food"></param>
    public void AddFood(Food food)
    {
        _context.Foods.Add(food);
        _context.SaveChanges();
    }
    
    public void AddFood(string foodName, double quantity, int accountId)
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

    #endregion

    #endregion Food

    #region Feedback

    #region Get

    public IEnumerable<Feedback> GetFeedbacks()
    {
        return _context.Feedbacks.OrderByDescending(x => x.Id);
    }

    #endregion
    
    #region Add

    public void SaveFeedback(int userId, int rating, string feedback)
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

    #endregion

    #region Delete

    public void DeleteFeedback(string feedbackBody)
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

    #endregion

    #endregion Feedback

    public bool IsEmailTaken(string email)
    {
        return _context.Persons.Any(x => x.Email == email);
    }

    ///////////////////////////// Weight Functions /////////////////////////////

    public IEnumerable<double> GetWeightValues(int accountId)
    {
        var weights = _context.PersonWeights.Where(x => x.Person.UserId == accountId).OrderBy(x => x.DateTime)
            .Select(x => x.Weight);
        return weights;
    }

    public IEnumerable<string> GetWeightDates(int accountId)
    {
        return _context.PersonWeights.Where(x => x.Person.UserId == accountId).OrderBy(x => x.DateTime).Select(x => $"{x.DateTime.Day:D2}.{x.DateTime.Month:D2}.{x.DateTime.Year} {x.DateTime.Hour:D2}:{x.DateTime.Minute:D2}");
    }

    public void AddNewWeight(double newWeight, int accountId)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == accountId);
        if (user == null) return;

        var person = _context.Persons.FirstOrDefault(x => x.User.Id == accountId);
        if (person == null) return;

        person.Weight = newWeight;
        
        var personWeight = new PersonWeight
        {
            Person = person,
            PersonId = person.Id,
            DateTime = DateTime.Now,
            Weight = newWeight
        };

        _context.PersonWeights.Add(personWeight);
        _context.SaveChanges();
    }

    public double GetTotalWeightLostPerDuration(int accountId, string duration)
    {
        double weightLost;
        List<double> weights;

        var dateNow = DateTime.Today;
        
        switch (duration)
        {
            case "WEEK":
                var dateWeek = dateNow.Subtract(new TimeSpan(7,0,0,0));
                weights = _context.PersonWeights.Where(x => x.Person.UserId == accountId && x.DateTime >= dateWeek).Select(x => x.Weight).ToList();
                break;
            case "MONTH":
                var dateMonth = dateNow.Subtract(new TimeSpan(30,0,0,0));
                weights = _context.PersonWeights.Where(x => x.Person.UserId == accountId && x.DateTime >= dateMonth).Select(x => x.Weight).ToList();
                break;
            case "YEAR":
                var dateYear = dateNow.Subtract(new TimeSpan(365,0,0,0));
                weights = _context.PersonWeights.Where(x => x.Person.UserId == accountId && x.DateTime >= dateYear).Select(x => x.Weight).ToList();
                break;
            default:
                return 0;
        }

        if (weights.Count == 0) return 0;
        
        weightLost = weights.Max() - weights.Last();
        return Math.Round(weightLost, 2);
    }

    public double GetAverageWeightLostPerDuration(int accountId, string duration)
    {
        List<double> weights;

        var dateNow = DateTime.Today;
        
        switch (duration)
        {
            case "WEEK":
                var dateWeek = dateNow.Subtract(new TimeSpan(7,0,0,0));
                weights = _context.PersonWeights.Where(x => x.Person.UserId == accountId && x.DateTime >= dateWeek).Select(x => x.Weight).ToList();
                break;
            case "MONTH":
                var dateMonth = dateNow.Subtract(new TimeSpan(30,0,0,0));
                weights = _context.PersonWeights.Where(x => x.Person.UserId == accountId && x.DateTime >= dateMonth).Select(x => x.Weight).ToList();
                break;
            case "YEAR":
                var dateYear = dateNow.Subtract(new TimeSpan(365,0,0,0));
                weights = _context.PersonWeights.Where(x => x.Person.UserId == accountId && x.DateTime >= dateYear).Select(x => x.Weight).ToList();
                break;
            default:
                return 0;
        }

        if (weights.Count == 0) return 0;
        var weightLost = (weights.Max() - weights.Last()) / 2;
        return Math.Round(weightLost, 2);
    }

    ///////////////////////////// Motivational Quote Functions /////////////////////////////

    public string GetMotivationalQuote()
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

    public double GetCaloriesGainedToday(int accountId)
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

    public double GetCaloriesLostToday(int accountId)
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

    ///////////////////////////// Admin Functions ???????? /////////////////////////////

    public User GetAdminData(int adminId)
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

    public void UpdateAdminAccount(User currentAdmin)
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

    public void UpdateAdminPassword(User currentAdmin)
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

    public void AddNewAdmin(string firstName, string lastName, string email)
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

    public IEnumerable<int> GetAppRatingValues()
    {
        return _context.Feedbacks.Select(x => x.Rating);
        //
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
        // return null;
    }

    public int GetAppUsersNumber()
    {
        return _context.Users.Count(x => x.Access == 0);
    }

    public bool IsNewAdmin(int accountID)
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

    #endregion

    ///////////////////////////// Helper Functions /////////////////////////////

    public string EncryptPassword(string password)
    {
        // string hash = "f0le@rn";
        // byte[] data = Encoding.UTF8.GetBytes(password);
        // using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        // {
        //     byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
        //     string encryptedPassword;
        //     using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider())
        //     {
        //         triDes.Key = keys;
        //         triDes.Mode = CipherMode.ECB;
        //         triDes.Padding = PaddingMode.PKCS7;
        //         ICryptoTransform transform = triDes.CreateEncryptor();
        //         byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
        //         encryptedPassword = Convert.ToBase64String(results, 0, results.Length);
        //     }
        //
        //     return encryptedPassword;
        // }
        return null;
    }

    private string GenerateRandomPassword()
    {
        // const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        // StringBuilder res = new StringBuilder();
        // Random rnd = new Random();
        // int length = 7;
        // while (0 < length--)
        // {
        //     res.Append(valid[rnd.Next(valid.Length)]);
        // }
        //
        // return res.ToString();
        return null;
    }

    private void SendUserEmail(string email, string name)
    {
        // MailMessage message = new MailMessage();
        //
        // // Reciever's Email
        // message.To.Add(email);
        //
        // // Email Subject
        // message.Subject = "Welcome To FitnessApp";
        //
        // // Sender's Email
        // message.From = new MailAddress("fitness.weightlossapp@gmail.com", "Fitness App");
        //
        // // Email Body
        // message.IsBodyHtml = true;
        // string htmlBody = "<body>" +
        //                   "<img src=https://bit.ly/2PI1mx4>" +
        //                   "<p style=\"float: left; \">" +
        //                   "<img src=https://bit.ly/2STDZ62 height=\"100px\" width=\"100px\" hspace=\"5\" style=\"border - right: 1px solid black;\">" +
        //                   "</p> " +
        //                   "<p style=\"padding: 20px; \"> " +
        //                   " <font size=\"5px\" color=\"#0F88A8\">" +
        //                   "<b> Welcome " + name + " , </b>" +
        //                   "</font>" +
        //                   "<br>" +
        //                   "Thank you for choosing FitnessApp!" +
        //                   "<br> <br> " +
        //                   "Ready for <b>RESHAPING</b> &#9889;" +
        //                   "<br><br>" +
        //                   " <font size=\"2 px\">" +
        //                   "feel free to contact us  &#9786; " +
        //                   ",<br> " +
        //                   "<a href=\"fitness.weightlossapp @gmail.com\">" +
        //                   "fitness.weightlossapp@gmail.com</font>" +
        //                   "</p>" +
        //                   "</div>" +
        //                   "</body>";
        // message.Body = htmlBody;
        //
        //
        // SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        // smtp.EnableSsl = true;
        // smtp.Credentials = new System.Net.NetworkCredential("fitness.weightlossapp@gmail.com", "m3leshyFitness21");
        // smtp.Send(message);
    }

    public void SendAdminEmail(string email, string randomPassword)
    {
        // MailMessage message = new MailMessage();
        // // Reciever's Email
        // message.To.Add(email);
        //
        // // Email Subject
        // message.Subject = "Welcome To FitnessApp";
        //
        // // Sender's Email
        // message.From = new MailAddress("fitness.weightlossapp@gmail.com", "Fitness App");
        //
        // // Email Body
        // message.IsBodyHtml = true;
        // string htmlBody = "<body>" +
        //                   "<img src=https://bit.ly/2PI1mx4> " +
        //                   "<p style=\"float: left; \">" +
        //                   "<img src=https://bit.ly/2STDZ62 height=\"100px\" width=\"100px\" hspace=\"5\" style=\"border - right: 1px solid black;\">" +
        //                   "</p> " +
        //                   "<p style=\"padding: 20px; \">  " +
        //                   "<font size=\"5px\" color=\"#0F88A8\">" +
        //                   "<b> Welcome  </b>" +
        //                   "</font>" +
        //                   "<br>" +
        //                   "<b> We are so grateful for having you ! &#9786; </b>" +
        //                   "<br> <br>" +
        //                   "<font size =\"3px\"> Please change your password as soon as possible : </font> " +
        //                   randomPassword +
        //                   "<br> <br> <br>" +
        //                   "<em>Best regards,</em>" +
        //                   "<br>FitnessApp Team </p>" +
        //                   "</div> " +
        //                   "</body>";
        //
        // message.Body = htmlBody;
        //
        //
        // SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        // smtp.EnableSsl = true;
        // smtp.Credentials = new System.Net.NetworkCredential("fitness.weightlossapp@gmail.com", "m3leshyFitness21");
        // smtp.Send(message);
    }

    public void Authorization(User user)
    {
        SignedUser = user;
        AccountId = SignedUser.Id;
        AccountType = SignedUser.Access switch
        {
            0 => AccountAccess.User,
            1 => AccountAccess.Admin,
            _ => AccountType
        };
    }

    public void Logout()
    {
        SignedUser = null;
        AccountId = 0;
        AccountType = null;
    }
}

public enum AccountAccess
{
    User,
    Admin
}