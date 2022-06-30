using System;
using System.Collections.Generic;
using System.Linq;

namespace RADB
{
    public class UserProgress
    {
        public string UserName { get; set; }
        public int GameID { get; set; }
        public int NumPossibleAchievements { get; set; }
        public int PossibleScore { get; set; }
        public int NumAchieved { get; set; }
        public int ScoreAchieved { get; set; }
        public int NumAchievedHardcore { get; set; }
        public int ScoreAchievedHardcore { get; set; }

        public bool SameProgress(UserProgress otherUser)
        {
            if (this.UserName == otherUser.UserName && this.GameID == otherUser.GameID && this.NumAchieved == otherUser.NumAchieved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
