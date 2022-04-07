using System;
using System.Collections.Generic;
using System.Linq;

namespace RADB
{
    public class Achievement
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public int ConsoleID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public string BadgeName { get; set; }
        public int DisplayOrder { get; set; }

        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string MemAddr { get; set; }
        public int NumAwarded { get; set; }
        public int NumAwardedHardcore { get; set; }

        public int TrueRatio { get; set; }

        public string BadgeFile()
        {
            return Folder.Achievements(ConsoleID, GameID) + BadgeName + RA.Format_BadgesLocal;
        }

        public string BadgeURL()
        {
            return RA.URL_Badges + BadgeName + RA.Format_Badges;
        }
    }
}
