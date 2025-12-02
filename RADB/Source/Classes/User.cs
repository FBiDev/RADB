using System;
using System.Collections.Generic;
using System.Drawing;
using App.Core;
using App.Core.Desktop;
using App.File.Json;

namespace RADB
{
    public class User
    {
        public User()
        {
            UserPicBitmap = RA.DefaultIcon;
            LastGame = new Game();
            PlayedGames = new List<GameProgress>();
        }

        public int ID { get; set; }
        [JsonProperty("User")]
        public string Name { get; set; }

        public string Motto { get; set; }

        public string Status { get; set; }

        public DateTime? MemberSince { get; set; }

        public string MemberSinceString
        {
            get { return MemberSince.ToString("dd MMM yyyy, HH:mm"); }
        }

        public dynamic LastActivity { get; set; }

        public DateTime? Lastupdate { get; set; }

        public string LastupdateString
        {
            get { return Lastupdate.ToString("dd MMM yyyy, HH:mm"); }
        }

        [JsonConverter(JsonType.Boolean)]
        public bool Untracked { get; set; }

        public int Permissions { get; set; }

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

        public string UserPic
        {
            get { return Name + PictureFormat.Png.ToStringHex(); }
        }

        public DownloadFile UserPicFile
        {
            get { return new DownloadFile(RA.UserPicBaseUrl + UserPic, Folder.User + UserPic); }
        }

        public Bitmap UserPicBitmap { get; set; }

        public int TotalPoints { get; set; }

        public int TotalTruePoints { get; set; }

        public string TotalPointsString
        {
            get { return TotalPoints.ToNumber() + " (" + TotalTruePoints.ToNumber() + ")"; }
        }

        public float RetroRatio
        {
            get { return TotalTruePoints > 0 ? ((float)TotalTruePoints / TotalPoints) : 0f; }
        }

        public string RetroRatioString
        {
            get { return RetroRatio.ToNumber(); }
        }

        [JsonProperty("Rank")]
        public int? RankValue { get; set; }
        [JsonProperty("RankInt")]
        public int Rank
        {
            get { return RankValue ?? 0; }
            set { RankValue = value; }
        }

        public int TotalRanked { get; set; }

        private string RankTop
        {
            get { return (((float)Rank / TotalRanked) * 100f).ToNumber(); }
        }

        public string RankString
        {
            get
            {
                if (Untracked)
                {
                    return "Untracked";
                }

                if (TotalPoints < RA.MinimumPoints)
                {
                    return "Needs at least " + RA.MinimumPoints + " points.";
                }

                if (Rank > 0 && TotalRanked > 0)
                {
                    return "#" + Rank.ToNumber() + " / " + TotalRanked.ToNumber() + " (Top " + RankTop + "%)";
                }

                return "-";
            }
        }

        public int TotalSoftcorePoints { get; set; }

        public string TotalSoftcorePointsString
        {
            get { return TotalSoftcorePoints.ToString(); }
        }

        private string RankSoftTop
        {
            get { return "-"; }
        }

        public string RankSoft
        {
            get
            {
                if (Untracked)
                {
                    return "Untracked";
                }

                if (TotalSoftcorePoints < RA.MinimumPoints)
                {
                    return "Needs at least " + RA.MinimumPoints + " points.";
                }

                // Need Soft Rank and SoftTop
                return "Unknown";
            }
        }

        public float AverageCompletion { get; set; }

        public string AverageCompletionString
        {
            get { return AverageCompletion.ToNumber() + "%"; }
        }

        // Achievements Won By Others
        public int ContribCount { get; set; }

        // Points Awarded to Others
        public int ContribYield { get; set; }

        [JsonConverter(JsonType.Boolean)]
        public bool UserWallActive { get; set; }

        public IEnumerable<GameProgress> PlayedGames { get; set; }

        public Game LastGame { get; set; }

        public int LastGameID { get; set; }

        public string LastGameTitle
        {
            get { return LastGame == null ? string.Empty : LastGame.Title; }
        }

        public Bitmap LastGameImage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LastGame.ImageIcon))
                {
                    return null;
                }

                LastGame.SetImageIconBitmap();
                return LastGame.ImageIconBitmap;
            }
        }

        private string RichPresenceValue { get; set; }

        public string RichPresenceMsg
        {
            get { return string.IsNullOrWhiteSpace(RichPresenceValue) || RichPresenceValue == "Unknown" ? string.Empty : RichPresenceValue; }
            set { RichPresenceValue = value; }
        }

        public bool Invalid
        {
            get { return ID == 0; }
        }

        public bool RankInvalid
        {
            get { return Untracked || Rank == 0 || TotalPoints < RA.MinimumPoints; }
        }

        public int RankLength
        {
            get
            {
                if (RankInvalid || Rank <= 0)
                {
                    return 0;
                }

                return ("#" + Rank.ToNumber()).Length;
            }
        }

        public void SetUserPicBitmap()
        {
            if (UserPicBitmap != RA.DefaultIcon)
            {
                return;
            }

            UserPicBitmap = BitmapExtension.SuperFastLoad(UserPicFile.Path);
        }
    }
}