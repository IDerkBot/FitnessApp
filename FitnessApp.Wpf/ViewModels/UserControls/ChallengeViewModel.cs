using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels.UserControls
{
    class ChallengeViewModel
    {
        private List<Challenge> allChallenges;
        private readonly List<JoinedChallenge> uncompletedJoinedChallenges = new();
        private readonly List<JoinedChallenge> completedJoinedChallenges = new();


        public ChallengeViewModel() { }

        public void AllChallengesViewModel(int accountId)
        {
            allChallenges = App.Database.GetChallengesByPersonId(accountId).ToList();
        }

        public void JoinedChallengesViewModel(int accountId)
        {

            List<JoinedChallenge> joinedChallenges = App.Database.GetJoinedChallenges(accountId);

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
