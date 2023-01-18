using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using GNX;

namespace RADB
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Motto { get; set; }

        public string Status { get; set; }

        public DateTime? MemberSince { get; set; }

        public dynamic LastActivity { get; set; }
        public DateTime? Lastupdate { get; set; }

        public int Permissions { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        public bool Untracked { get; set; }

        public string AccountType
        {
            get
            {
                switch (Permissions)
                {
                    case -2: return "Spam";
                    case -1: return "Banned";
                    case 0: return "Unregistered";
                    case 1: return "Registered";
                    case 2: return "Junior Developer";
                    case 3: return "Developer";
                    case 4: return "Admin";
                    default: return "Invalid permission";
                }
            }
        }

        private string _UserPic { get; set; }
        public string UserPic { get { return _UserPic; } set { _UserPic = value.Replace(@"/UserPic/", ""); } }
        public DownloadFile UserPicFile { get { return new DownloadFile(RA.USER_HOST + UserPic, Folder.User + UserPic); } }
        public Bitmap UserPicBitmap { get; set; }

        public void SetUserPicBitmap()
        {
            if (UserPicBitmap != RA.DefaultIcon) { return; }
            UserPicBitmap = Picture.Create(UserPicFile.Path, RA.ErrorIcon).Bitmap;
        }

        public int TotalPoints { get; set; }
        public int TotalTruePoints { get; set; }

        public float RetroRatio
        {
            get
            {
                if (TotalTruePoints > 0)
                    return ((float)TotalTruePoints / (float)TotalPoints);
                else
                    return 0;
            }
        }

        public int? Rank { get; set; }
        public int TotalRanked { get; set; }

        private float GetTop() { return (((float)Rank / (float)TotalRanked) * 100f); }
        public string GetRank(bool useGroupSeparator = true)
        {
            if (Untracked)
                return "Untracked";
            else if (TotalPoints < RA.MIN_POINTS)
                return "Needs at least " + RA.MIN_POINTS + " points.";
            else if (Rank != null && TotalRanked > 0)
                return Rank.ToNumber(languageNumber: useGroupSeparator) + " / " + TotalRanked.ToNumber(languageNumber: useGroupSeparator) + " (Top " + GetTop().ToNumber() + "%)";
            else
                return "-";
        }

        public int TotalSoftcorePoints { get; set; }

        private string GetSoftTop() { return "-"; }
        public string GetSoftRank(bool useGroupSeparator = true)
        {
            if (Untracked)
                return "Untracked";
            else if (TotalSoftcorePoints < RA.MIN_POINTS)
                return "Needs at least " + RA.MIN_POINTS + " points.";
            else
                return "Unknown";
        }

        public string AverageCompletion { get; set; }

        //Achievements Won By Others
        public int ContribCount { get; set; }
        //Points Awarded to Others
        public int ContribYield { get; set; }

        public string RichPresenceMsg { get; set; }
        public int UserWallActive { get; set; }

        public Game LastGame { get; set; }

        public string LastGameString()
        {
            string message = "-";
            if (RichPresenceMsg.Empty() == false && RichPresenceMsg != "Unknown")
            {
                if (LastGame.NotNull())
                {
                    message = LastGame.Title + " (" + LastGame.ConsoleName + ")";
                }
            }
            return message;
        }

        public string LastGameDescString()
        {
            string message = "-";
            if (RichPresenceMsg.Empty() == false && RichPresenceMsg != "Unknown")
            {
                message = RichPresenceMsg;
            }
            return message;
        }

        public User()
        {
            UserPicBitmap = RA.DefaultIcon;
        }
    }
}
