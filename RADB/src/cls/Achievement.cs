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
        public string DescriptionComplete
        {
            get
            {
                return Title + Environment.NewLine + Description;
            }
        }
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

        private string BadgeFormatLocal { get { return ".png"; } }
        private string BadgeFormatURL { get { return ".png"; } }

        public string BadgeFile()
        {
            return Folder.Achievements(ConsoleID, GameID) + BadgeName + BadgeFormatLocal;
        }

        public string BadgeURL()
        {
            return RA.BADGE_HOST + BadgeName + BadgeFormatURL;
        }
    }
}
