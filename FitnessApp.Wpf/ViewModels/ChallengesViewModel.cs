using FitnessApp.DataAccessLayer;
using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels
{
    class ChallengesViewModel
    {
        private List<Challenge> allChallenges;
        private List<Challenge> uncompletedJoinedChallenges = new List<Challenge>();
        private List<Challenge> completedJoinedChallenges = new List<Challenge>();


        public ChallengesViewModel() { }

        public void AllChallengesViewModel(int accountID)
        {
            allChallenges = Database.GetAllChallenges(accountID);
        }

        public void JoinedChallengesViewModel(int accountID)
        {

            List<Challenge> joinedChallenges = Database.GetJoinedChallenges(accountID);

            foreach (var item in joinedChallenges)
            {
                // Classify Challenges
                if (item.Progress < item.TargetMinutes)
                    uncompletedJoinedChallenges.Add(item);
                else
                    completedJoinedChallenges.Add(item);
            }

        }

        public List<Challenge> AllChallenges
        {
            get => allChallenges;
            set { }
        }

        public List<Challenge> UncompletedJoinedChallenges
        {
            get => uncompletedJoinedChallenges;
            set { }
        }

        public List<Challenge> CompletedJoinedChallenges
        {
            get => completedJoinedChallenges;
            set { }
        }
    }
}
