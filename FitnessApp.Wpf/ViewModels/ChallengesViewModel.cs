using FitnessApp.DataAccessLayer;
using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels
{
    class ChallengesViewModel
    {
        private List<Challenge> allChallenges;
        private readonly List<JoinedChallenge> uncompletedJoinedChallenges = new();
        private readonly List<JoinedChallenge> completedJoinedChallenges = new();


        public ChallengesViewModel() { }

        public void AllChallengesViewModel(int accountID)
        {
            allChallenges = App.Database.GetChallengesByUserId(accountID).ToList();
        }

        public void JoinedChallengesViewModel(int accountID)
        {

            List<JoinedChallenge> joinedChallenges = App.Database.GetJoinedChallenges(accountID);

            foreach (var item in joinedChallenges)
            {
                // Classify Challenges
                if (item.Progress < item.Challenge.TargetMinutes)
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

        public List<JoinedChallenge> UncompletedJoinedChallenges
        {
            get => uncompletedJoinedChallenges;
            set { }
        }

        public List<JoinedChallenge> CompletedJoinedChallenges
        {
            get => completedJoinedChallenges;
            set { }
        }
    }
}
