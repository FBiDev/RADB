using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;

namespace RADB
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Motto { get; set; }

        public string Status { get; set; }

        public DateTime MemberSince { get; set; }

        public dynamic LastActivity { get; set; }
        public DateTime Lastupdate { get; set; }

        public int Permissions { get; set; }
        public int Untracked { get; set; }

        public string AccountType
        {
            get
            {
                switch (Permissions)
                {
                    case 1: return "Registered";
                    case 3: return "Developer";
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
        public int TotalSoftcorePoints { get; set; }
        public int TotalTruePoints { get; set; }

        public string RetroRatio
        { get { return ((float)TotalTruePoints / (float)TotalPoints).ToString("n2"); } }


        public int? Rank { get; set; }
        public int TotalRanked { get; set; }

        private string GetTop() { return (((float)Rank / (float)TotalRanked) * 100).ToString("n2"); }
        public string GetRank { get { if (Rank != null) { return Rank + " / " + TotalRanked + " (Top " + GetTop() + "%)"; } return "Untracked"; } }

        private string GetSoftTop() { return (((float)Rank / (float)TotalRanked) * 100).ToString("n2"); }
        public string GetSoftRank { get { if (Rank != null) { return Rank + " / " + TotalRanked + " (Top " + GetSoftTop() + "%)"; } return "Untracked"; } }


        public User()
        {
            UserPicBitmap = RA.DefaultIcon;
        }
    }
}
