using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using GNX;
using System.Globalization;

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
        public int Untracked { get; set; }

        public string AccountType
        {
            get
            {
                switch (Permissions)
                {
                    case 0: return "Unregistered";
                    case 1: return "Registered";
                    case 2: return "Junior Developer";
                    case 3: return "Developer";
                    case 4: return "Admin";
                    default: return "User";
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

        public string RetroRatio
        {
            get
            {
                if (TotalTruePoints > 0)
                    return ((float)TotalTruePoints / (float)TotalPoints).ToNumber();
                else
                    return 0.ToNumber();
            }
        }

        public int? Rank { get; set; }
        public int TotalRanked { get; set; }

        private string GetTop() { return (((float)Rank / (float)TotalRanked) * 100).ToNumber(); }
        public string GetRank
        {
            get
            {
                if (Untracked == 1)
                    return "Untracked";
                else if (TotalPoints < 250)
                    return "Needs at least 250 points.";
                else if (Rank != null && TotalRanked > 0)
                    return Rank.ToNumber(languageNumber: true) + " / " + TotalRanked.ToNumber(languageNumber: true) + " (Top " + GetTop() + "%)";
                else
                    return "-";
            }
        }

        public int TotalSoftcorePoints { get; set; }

        private string GetSoftTop() { return "-"; }
        public string GetSoftRank { get { return "-"; } }

        public string AverageCompletion { get; set; }

        //Achievements Won By Others
        public int ContribCount { get; set; }
        //Points Awarded to Others
        public int ContribYield { get; set; }

        public string RichPresenceMsg { get; set; }
        public int UserWallActive { get; set; }

        public Game LastGame { get; set; }

        public User()
        {
            UserPicBitmap = RA.DefaultIcon;
        }
    }
}
