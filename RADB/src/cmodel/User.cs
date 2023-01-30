using System;
using System.Collections.Generic;
using System.Drawing;
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

        string _UserPic { get; set; }
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
                    return ((float)TotalTruePoints / TotalPoints);
                return 0f;
            }
        }

        public int? Rank { get; set; }
        public int TotalRanked { get; set; }

        float GetTop() { return (((float)Rank / TotalRanked) * 100f); }
        public string GetRank(bool useCustomLanguage = true)
        {
            if (Untracked)
                return "Untracked";
            if (TotalPoints < RA.MIN_POINTS)
                return "Needs at least " + RA.MIN_POINTS + " points.";
            if (Rank != null && TotalRanked > 0)
                return Rank.ToNumber(useCustomLanguage) + " / " + TotalRanked.ToNumber(useCustomLanguage) + " (Top " + GetTop().ToNumber() + "%)";
            return "-";
        }

        public int TotalSoftcorePoints { get; set; }

        string GetSoftTop() { return "-"; }
        public string GetSoftRank()
        {
            if (Untracked)
                return "Untracked";
            if (TotalSoftcorePoints < RA.MIN_POINTS)
                return "Needs at least " + RA.MIN_POINTS + " points.";
            return "Unknown";
        }

        string _AverageCompletion { get; set; }
        public string AverageCompletion
        {
            get { return _AverageCompletion = _AverageCompletion ?? "0.00%"; }
            set { _AverageCompletion = value; }
        }

        //Achievements Won By Others
        public int ContribCount { get; set; }
        //Points Awarded to Others
        public int ContribYield { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        public bool UserWallActive { get; set; }

        public IEnumerable<GameProgress> PlayedGames { get; set; }
        public Game LastGame { get; set; }

        public string LastGameTitle()
        {
            if (LastGame.IsNull())
                return "";

            return LastGame.Title;
        }

        string _RichPresenceMsg;
        public string RichPresenceMsg
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_RichPresenceMsg) || _RichPresenceMsg == "Unknown")
                    return "";
                return _RichPresenceMsg;
            }
            set { _RichPresenceMsg = value; }
        }

        public User()
        {
            UserPicBitmap = RA.DefaultIcon;
            LastGame = new Game();
            PlayedGames = new List<GameProgress>();
        }

        public bool Invalid { get { return ID == 0 || string.IsNullOrWhiteSpace(Name); } }
        public bool RankInvalid { get { return Untracked || Rank == null || TotalPoints < RA.MIN_POINTS; } }
    }
}
