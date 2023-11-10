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
        public string MemberSinceString { get { return MemberSince.ToString("dd MMM yyyy, HH:mm"); } }

        public dynamic LastActivity { get; set; }
        public DateTime? Lastupdate { get; set; }
        public string LastupdateString { get { return Lastupdate.ToString("dd MMM yyyy, HH:mm"); } }

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
        public string UserPic
        {
            get { return _UserPic; }
            set
            {
                if (LastActivity != null)
                    _UserPic = value = LastActivity.User.ToString() + PictureFormat.Png.ToStringHex();
            }
        }
        public DownloadFile UserPicFile { get { return new DownloadFile(RA.USER_HOST + UserPic, Folder.User + UserPic); } }
        public Bitmap UserPicBitmap { get; set; }
        public void SetUserPicBitmap()
        {
            if (UserPicBitmap != RA.DefaultIcon) { return; }
            UserPicBitmap = BitmapExtension.SuperFastLoad(UserPicFile.Path, RA.ErrorIcon);
        }

        public int TotalPoints { get; set; }
        public int TotalTruePoints { get; set; }

        public string TotalPointsString
        {
            get { return TotalPoints + " (" + TotalTruePoints + ")"; }
        }

        public float RetroRatio
        {
            get { return TotalTruePoints > 0 ? ((float)TotalTruePoints / TotalPoints) : 0f; }
        }

        public string RetroRatioString
        {
            get { return string.Format("{0:N2}", RetroRatio); }
        }

        [JsonProperty("Rank")]
        public int? _Rank { get; set; }
        [JsonProperty("RankInt")]
        public int Rank { get { return _Rank ?? 0; } set { _Rank = value; } }

        public int TotalRanked { get; set; }

        string RankTop
        {
            get { return string.Format("{0:N2}", (((float)Rank / TotalRanked) * 100f)); }
        }

        public string RankString
        {
            get
            {
                if (Untracked)
                    return "Untracked";
                if (TotalPoints < RA.MIN_POINTS)
                    return "Needs at least " + RA.MIN_POINTS + " points.";
                if (Rank > 0 && TotalRanked > 0)
                    return Rank + " / " + TotalRanked + " (Top " + RankTop + "%)";
                return "-";
            }
        }

        public int TotalSoftcorePoints { get; set; }

        public string TotalSoftcorePointsString
        {
            get { return TotalSoftcorePoints.ToString(); }
        }

        string RankSoftTop { get { return "-"; } }
        public string RankSoft
        {
            get
            {
                if (Untracked)
                    return "Untracked";
                if (TotalSoftcorePoints < RA.MIN_POINTS)
                    return "Needs at least " + RA.MIN_POINTS + " points.";
                //Need Soft Rank and SoftTop
                return "Unknown";
            }
        }

        public float AverageCompletion { get; set; }

        public string AverageCompletionString
        {
            get { return string.Format("{0:N2}", AverageCompletion) + "%"; }
        }

        //Achievements Won By Others
        public int ContribCount { get; set; }
        //Points Awarded to Others
        public int ContribYield { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        public bool UserWallActive { get; set; }

        public IEnumerable<GameProgress> PlayedGames { get; set; }
        public Game LastGame { get; set; }

        public string LastGameTitle
        {
            get { return LastGame == null ? "" : LastGame.Title; }
        }

        public Bitmap LastGameImage
        {
            get { return string.IsNullOrWhiteSpace(LastGame.ImageIcon) ? null : LastGame.ImageIconBitmap; }
        }

        string _RichPresenceMsg { get; set; }
        public string RichPresenceMsg
        {
            get { return string.IsNullOrWhiteSpace(_RichPresenceMsg) || _RichPresenceMsg == "Unknown" ? "" : _RichPresenceMsg; }
            set { _RichPresenceMsg = value; }
        }

        public User()
        {
            UserPicBitmap = RA.DefaultIcon;
            LastGame = new Game();
            PlayedGames = new List<GameProgress>();
        }

        public bool Invalid { get { return ID == 0; } }
        public bool RankInvalid { get { return Untracked || Rank == 0 || TotalPoints < RA.MIN_POINTS; } }
        public int RankLength { get { return RankInvalid == false && Rank > 0 ? Rank.ToString().Length : 0; } }
    }
}
