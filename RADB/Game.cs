using System;
using System.Collections.Generic;

namespace RADB
{
    public class Game
    {
        List<string> Achievements { get; set; }
        public string Console { get; set; }

        public string ConsoleID { get; set; }
        public string ConsoleName { get; set; }
        public string Developer { get; set; }
        public string Flags { get; set; }
        public string ForumTopicID { get; set; }

        public string GameIcon { get; set; }
        public string GameTitle { get; set; }

        public string Genre { get; set; }
        public int ID { get; set; }
        public string ImageBoxArt { get; set; }
        public string ImageIcon { get; set; }
        public string ImageIngame { get; set; }
        public string ImageTitle { get; set; }

        public bool IsFinal { get; set; }
        public int NumAchievements { get; set; }
        public int NumDistinctPlayersCasual { get; set; }
        public int NumDistinctPlayersHardcore { get; set; }

        public string Publisher { get; set; }
        public string Released { get; set; }
        public string RichPresencePatch { get; set; }
        public string Title { get; set; }
    }
}
