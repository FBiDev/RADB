using System;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
//
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RADB.Properties;
using GNX;

namespace RADB
{
    public class RA
    {
        #region _Main
        //HOSTS
        public const string HOST_URL = "https://retroachievements.org/";

        //https://s3-eu-west-1.amazonaws.com/i.retroachievements.org
        public const string IMAGE_HOST = "http://media.retroachievements.org/Images/";
        public const string BADGE_HOST = "http://media.retroachievements.org/Badge/";
        public const string USER_HOST = "http://media.retroachievements.org/UserPic/";

        //URLs
        public static string Game_URL(int gameID) { return HOST_URL + "game/" + gameID.ToString(); }
        public static string User_URL(string userName) { return HOST_URL + "user/" + userName; }

        //API
        const string API_HOST = HOST_URL + "API/";
        const string API_UserName = "RADatabase";
        const string API_Key = "GRaWk9onm4B0LSWSFaDt5a2dQE3N8Yme";

        public const string Login_URL = HOST_URL + "request/auth/login.php";

        const string API_URL_Consoles = "API_GetConsoleIDs.php";
        const string API_URL_GameList = "API_GetGameList.php";
        const string API_URL_GameExtend = "API_GetGameExtended.php";
        const string API_URL_UserProgress = "API_GetUserProgress.php";
        const string API_URL_UserInfo = "API_GetUserSummary.php";
        const string API_URL_UserCompletedGames = "API_GetUserCompletedGames.php";

        string AuthQS()
        { return "?z=" + API_UserName + "&y=" + API_Key; }

        string GetURL(string target, string parames = "")
        { return API_HOST + target + AuthQS() + "&" + parames; }

        public DownloadFile API_File_Consoles()
        {
            return new DownloadFile(GetURL(API_URL_Consoles),
                            Folder.Console + "Consoles.json");
        }

        public DownloadFile API_File_GameList(Console console)
        {
            return new DownloadFile(GetURL(API_URL_GameList, "i=" + console.ID),
                            (Folder.GameData + console.Name + ".json").Replace("/", "-"));
        }

        public DownloadFile API_File_GameExtend(Game game)
        {
            return new DownloadFile(GetURL(API_URL_GameExtend, "i=" + game.ID),
                            (Folder.GameDataExtend(game.ConsoleID) + game.ID + ".json"));
        }

        public DownloadFile API_File_UserProgress(string userName, int gameID)
        {
            return new DownloadFile(GetURL(API_URL_UserProgress, "u=" + userName + "&i=" + gameID),
                            (Folder.User + "UserProgress.json"));
        }

        public DownloadFile API_File_UserInfo(string userName)
        {
            return new DownloadFile(GetURL(API_URL_UserInfo, "u=" + userName),
                            (Folder.User + userName.ToLower() + "_Info.json"));
        }

        public DownloadFile API_File_UserCompletedGames(string userName)
        {
            return new DownloadFile(GetURL(API_URL_UserCompletedGames, "u=" + userName),
                            (Folder.User + userName.ToLower() + "_CompletedGames.json"));
        }

        Dictionary<string, string> LinkMessages = new Dictionary<string, string>()
        {
            {API_URL_Consoles,"API Consoles File"},
            {API_URL_UserInfo,"API User Summary File - Username not found!"},
            {Login_URL, "Failed to Login in RA"},
        };

        public const int MIN_POINTS = 250;

        static readonly Size GameIconSize = new Size(96, 96);
        static readonly Size GameBadgesSize = new Size(64, 64);

        public static readonly Bitmap DefaultIcon = new Picture(GameIconSize).Bitmap;
        public static readonly Bitmap ErrorIcon = Resources.notfound;

        public static readonly Bitmap DefaultTitleImage = new Picture(200, 150).Bitmap;
        public static readonly Bitmap DefaultIngameImage = new Picture(200, 150).Bitmap;
        public static readonly Bitmap DefaultBoxArtImage = new Picture(200, 150).Bitmap;

        public struct GameType
        {
            public static string[] NotOfficial = { Prototype, Unlicensed, Demo, Hack, Homebrew, Subset, TestKit, Demoted };
            public const string Prototype = "~Prototype~";
            public const string Unlicensed = "~Unlicensed~";
            public const string Demo = "~Demo~";
            public const string Hack = "~Hack~";
            public const string Homebrew = "~Homebrew~";
            public const string Subset = "[Subset";
            public const string TestKit = "~Test";
            public const string Demoted = "~Z~";
            public const string Multi = "~Multi~";
        }

        public RA()
        {
            WebClientExtend.CustomErrorMessages = LinkMessages;
        }
        #endregion

        #region _Consoles
        public async Task DownloadConsoles()
        {
            Browser.dlConsoles.SetFile(API_File_Consoles());

            if (await Browser.dlConsoles.Start())
            {
                await Console.DeleteAll();

                List<Console> list = await DeserializeConsoles();
                if (list.Any())
                    await Console.InsertList(await DeserializeConsoles());
            }
        }

        Task<List<Console>> DeserializeConsoles()
        {
            return Task.Run(() =>
            {
                List<Console> consoles = JsonConvert.DeserializeObject<List<Console>>(File.ReadAllText(API_File_Consoles().Path));
                return consoles.OrderBy(x => x.ID).ToList();
            });
        }
        #endregion

        #region _GameList
        public async Task DownloadGameList(Console console)
        {
            await Task.Run(async () =>
            {
                Browser.dlGames.SetFile(API_File_GameList(console));

                if (await Browser.dlGames.Start())
                {
                    await Game.Delete(console.ID);

                    List<Game> list = await DeserializeGameList(console);
                    if (list.Any())
                        await Game.InsertList(list);
                }
            });
        }

        Task<List<Game>> DeserializeGameList(Console console)
        {
            return Task.Run(() =>
            {
                List<Game> list = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(API_File_GameList(console).Path));
                return list;
            });
        }

        public async Task DownloadGamesIcon(Console console, Download dl)
        {
            await Task.Run(async () =>
            {
                List<Game> games = await Game.Search(console.ID, true);
                dl.Files = games.Select(g => g.ImageIconFile).ToList();
                await (dl.Start());
            });
        }
        #endregion

        #region _GameExtend
        public Task<GameExtend> DownloadGameExtend(Game game, Download dlExtend)
        {
            return Task.Run(async () =>
            {
                dlExtend.SetFile(API_File_GameExtend(game));

                if (await (dlExtend.Start()))
                {
                    GameExtend obj = (await DeserializeGameExtend(game));
                    obj.ID = game.ID;
                    obj.ConsoleID = game.ConsoleID;
                    await obj.Delete();
                    await obj.Insert();
                    return obj;
                }
                return null;
            });
        }

        Task<GameExtend> DeserializeGameExtend(Game game)
        {
            return Task.Run(() =>
            {
                string AllText = File.ReadAllText(API_File_GameExtend(game).Path);
                string gameData = AllText.GetBetween("{", ",\"Achievements\":");
                string cheevos = AllText.GetBetween("\"Achievements\":{", "}}");
                gameData = "{" + gameData + "}";
                cheevos = "{" + cheevos + "}";

                GameExtend obj = JsonConvert.DeserializeObject<GameExtend>(gameData);

                JToken jcheevos = JsonConvert.DeserializeObject<JToken>(cheevos);

                obj.SetAchievements(jcheevos);

                return obj;
            });
        }

        public async Task DownloadGameExtendImages(Game game)
        {
            await Task.Run(async () =>
            {
                GameExtend gamex = await GameExtend.Find(game.ID);
                Browser.dlGameExtendImages.Files = new List<DownloadFile>() {
                    gamex.ImageTitleFile,
                    gamex.ImageIngameFile,
                    gamex.ImageBoxArtFile,
                };
                await (Browser.dlGameExtendImages.Start());
            });
        }
        #endregion

        #region _UserInfo
        public async Task<UserProgress> GetUserProgress(string userName, int gameID)
        {
            return await Task.Run(async () =>
            {
                string userData = await Browser.DownloadString(API_File_UserProgress(userName, gameID).URL, true);
                userData = userData.GetBetween(":{", "}}");
                userData = "{" + userData + "}";

                UserProgress user = null;
                if (string.IsNullOrWhiteSpace(userData) == false)
                {
                    user = JsonConvert.DeserializeObject<UserProgress>(userData);
                    user.UserName = userName;
                    user.GameID = gameID;
                }
                return user;
            });
        }

        public async Task<User> GetUserInfo(string userName)
        {
            var user = new User();

            var file = API_File_UserInfo(userName);
            var dl = new Download(file) { Overwrite = true };
            if (await dl.Start() == false) { return user; }

            return await Task.Run(() =>
            {
                var userData = File.ReadAllText(file.Path);
                if (userData.Empty()) { return user; }

                user = JsonConvert.DeserializeObject<User>(userData);

                if (user.LastActivity != null)
                {
                    user.Name = user.LastActivity.User;
                    user.Lastupdate = user.LastActivity.lastupdate;
                }
                return user;
            });
        }

        public async Task<User> GetUserInfoPic(User user)
        {
            return await Task.Run(async () =>
            {
                var file = user.UserPicFile;
                var dl = new Download(file);
                if (await (dl.Start()))
                    user.SetUserPicBitmap();

                return user;
            });
        }

        public async Task<User> GetUserInfoLastGame(User user)
        {
            return await Task.Run(async () =>
            {
                if (user.LastGame.ID == 0) { return user; }

                var file = user.LastGame.ImageIconFile;
                var dl = new Download(file);
                if (await (dl.Start()))
                    user.LastGame.SetImageIconBitmap();

                return user;
            });
        }

        public async Task<User> GetUserInfoAwards(User user)
        {
            return await Task.Run(async () =>
            {
                if (user.TotalPoints == 0 && user.TotalSoftcorePoints == 0) { return user; }

                var file = API_File_UserCompletedGames(user.Name);
                var dl = new Download(file);
                if (await dl.Start() == false) { return user; }

                var awardsJson = File.ReadAllText(file.Path);

                var awardsList = JsonConvert.DeserializeObject<IEnumerable<GameProgress>>(awardsJson);
                awardsList = awardsList.Where(x => x.PctWon > 0 && x.ConsoleName != "Hubs" && x.ConsoleName != "Events");

                //Remove SoftCore Duplicates
                awardsList = awardsList.OrderByDescending(x => x.HardcoreMode).GroupBy(x => x.GameID,
                    (k, g) => g.Aggregate((x1, x2) => (x1.PctWon >= x2.PctWon) ? x1 : x2));

                if (awardsList.Count() > 0)
                {
                    float? totalPctWon = awardsList.Sum(x => x.PctWon);
                    float avgPctWon = ((float)totalPctWon / awardsList.Count()) * 100f;
                    user.AverageCompletion = avgPctWon;

                    user.PlayedGames = awardsList;
                }
                return user;
            });
        }
        #endregion

        #region MergeImages
        public async Task MergeGameBadges(Game game)
        {
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);

            //var query = gCheevos.GroupBy(x => new { x.BadgeURL, x.GameID }).Where(g => g.Count() > 1)
            //  .Select(y => new { Element = y.Key, Counter = y.Count() })
            //  .ToList().OrderBy(x => x.Element.BadgeURL).ToList();
            //var queryTotal = gCheevos.Count - (query.Select(x => x.Counter).Sum() - query.Count);

            //Game game = (await Game.Listar(new Game { ID = gameID })).FirstOrNew();
            GameExtend gameExtend = await DownloadGameExtend(game, Browser.dlGames);
            var badgeFiles = gameExtend.AchievementsList.Select(a => new DownloadFile(a.BadgeURL(), a.BadgeFile())).ToList();
            Browser.dlGamesBadges.Files = badgeFiles;

            await Browser.dlGamesBadges.Start();
            var badgeNames = badgeFiles.Select(a => a.Path).ToList();

            await Task.Run(() =>
            {
                badgeNames = Archive.RemoveDuplicates(badgeNames);
            });

            if (badgeNames.Any())
            {
                Picture pic = await Task.Run(() =>
                {
                    pic = new Picture(badgeNames, true, 11, GameBadgesSize, false);
                    pic.Save(game.MergedBadgesPath(), PictureFormat.Png, false);

                    Archive.SaveGameBadges(game, badgeNames, game.MergedBadgesPath() + ".txt");
                    return pic;
                });

                var f = Forms.Open<ImageViewer>();
                f.SetImage(pic, GameBadgesSize);
            }

            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;

            if (badgeNames.Empty())
            {
                MessageBox.Show("No Icons Found");
            }

            //MessageBox.Show("Badges merged in: " + fim0.TotalSeconds + "s");
        }

        public async Task MergeGamesIcon(Console console, bool getIncorrectSize = false)
        {
            if (console.IsNull()) { MessageBox.Show("No Console Selected"); return; }

            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);

            await DownloadGamesIcon(console, Browser.dlConsolesGamesIcon);
            var games = (await Game.Search(console.ID, true)).Where(g => g.NumAchievements > 0).ToList();
            var gamesIcon = games.Select(g => g.ImageIconFile.Path).ToList();

            await Task.Run(() =>
            {
                if (getIncorrectSize) { gamesIcon = Archive.RemoveImageSize(gamesIcon, GameIconSize); }
                //gamesIcon = Archive.RemoveDuplicates(gamesIcon);
            });

            if (gamesIcon.Any())
            {
                Picture pic = await Task.Run(() =>
                {
                    pic = new Picture(gamesIcon, true, 11, GameIconSize, false);
                    pic.Save(Game.MergedIconsPath(console.Name), PictureFormat.Png, false);

                    Archive.SaveGamesIcon(games, gamesIcon, Game.MergedIconsPath(console.Name) + ".txt");

                    return pic;
                });

                var f = Forms.Open<ImageViewer>();
                f.SetImage(pic, GameIconSize);
            }

            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
            //MessageBox.Show(fim0.TotalSeconds.ToString());
            if (gamesIcon.Empty())
            {
                MessageBox.Show("No Icons Found");
            }

            //MessageBox.Show("Badges merged in: " + fim0.TotalSeconds + "s");
            return;
        }
        #endregion
    }
}
