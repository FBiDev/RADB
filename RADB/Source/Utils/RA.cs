using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RADB.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GNX;
using GNX.Desktop;

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

        public const string LOGOUT_URL = HOST_URL + "logout";
        public const string LOGIN_URL = HOST_URL + "login";

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
                                    (Folder.Console + "Consoles.json"));
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

        Dictionary<string, string> LinkMessages = new Dictionary<string, string>
        {
            {API_URL_Consoles,"API ConsoleIDs"},
            {API_URL_GameList, "API GameList"},
            {API_URL_GameExtend,"API GameExtend"},
            {API_URL_UserInfo,"API UserSummary"},
            {API_URL_UserProgress, "API UserProgress"},
            {API_URL_UserCompletedGames, "API UserCompletedGames"},
            {LOGIN_URL, "Failed to Login in RA"}
        };

        public const int MIN_POINTS = 250;

        static readonly Size GameIconSize = new Size(96, 96);
        static readonly Size GameBadgesSize = new Size(64, 64);
        static readonly Size GameIconGridSize = new Size(32, 32);

        public static readonly Bitmap DefaultIcon = new Picture(GameIconSize).Bitmap;
        public static readonly Bitmap DefaultIconGrid = new Picture(GameIconGridSize).Bitmap;
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

        public RA() { WebClientExtend.CustomErrorMessages = LinkMessages; }
        #endregion

        #region _Consoles
        public async Task DownloadConsoles()
        {
            await Task.Run(async () =>
            {
                Browser.dlConsoles.SetFile(API_File_Consoles());

                if (await Browser.dlConsoles.Start())
                {
                    var list = await DeserializeConsoles();
                    if (list.Any())
                    {
                        await Console.DeleteAll();
                        await Console.SaveList(list);
                    }
                }
            });
        }

        Task<List<Console>> DeserializeConsoles()
        {
            return Task.Run(() =>
            {
                var consoles = Json.DeserializeObject<List<Console>>(File.ReadAllText(API_File_Consoles().Path));
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
                    var list = await DeserializeGameList(console);
                    if (list.Any())
                    {
                        await Game.Delete(console.ID);
                        await Game.SaveList(list);
                    }
                }
            });
        }

        Task<List<Game>> DeserializeGameList(Console console)
        {
            return Task.Run(() =>
            {
                var games = Json.DeserializeObject<List<Game>>(File.ReadAllText(API_File_GameList(console).Path));
                return games;
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
        //To get Year from all games
        //public Task<List<GameExtend>> DownloadGameExtendList(List<Game> gameList, Download dlExtend)
        //{
        //    return Task.Run(async () =>
        //    {
        //        var gameListToDownload = await Game.ListNotInReleasedDate(BIND.Console.ID);
        //        //gameList.ForEach(x =>
        //        //{
        //        //    if (File.Exists(x.ExtendFile.Path) == false)
        //        //        gameListToDownload.Add(x);
        //        //});

        //        int max = gameListToDownload.Count;
        //        int itemsToGet = 4;
        //        var gameExList = new List<GameExtend>();

        //        for (int i = 0; i < max; i += itemsToGet)
        //        {
        //            MainCommon.WriteOutput(i + " of " + max);
        //            var gamesTaken = gameListToDownload.Skip(i).Take(itemsToGet);
        //            dlExtend.Files = gamesTaken.Select(x => x.ExtendFile).ToList();

        //            if (await (dlExtend.Start()))
        //            {
        //                foreach (var game in gamesTaken)
        //                {
        //                    GameExtend obj = (await DeserializeGameExtend(game));
        //                    obj.ID = game.ID;
        //                    obj.ConsoleID = game.ConsoleID;

        //                    await obj.Delete();

        //                    if (await obj.Save())
        //                    {
        //                        game.SetYear(obj.ReleasedDate);
        //                        await game.SaveReleasedDate();
        //                        gameExList.Add(obj);
        //                    }

        //                    if (game.NumAchievements == 0)
        //                        File.Delete(game.ExtendFile.Path);
        //                }
        //            }
        //        }
        //        return gameExList;
        //    });
        //}

        public Task<GameExtend> DownloadGameExtend(Game game, Download dlExtend)
        {
            return Task.Run(async () =>
            {
                dlExtend.SetFile(game.ExtendFile);

                if (await (dlExtend.Start()))
                {
                    GameExtend obj = (await DeserializeGameExtend(game));
                    obj.ID = game.ID;
                    obj.ConsoleID = game.ConsoleID;
                    await obj.Delete();
                    await obj.Save();
                    return obj;
                }
                return null;
            });
        }

        Task<GameExtend> DeserializeGameExtend(Game game)
        {
            if (File.Exists(game.ExtendFile.Path) == false) return Task.FromResult(new GameExtend());

            var AllText = File.ReadAllText(game.ExtendFile.Path);
            var gameData = AllText.GetBetween("{", ",\"Achievements\":");
            var cheevos = AllText.GetBetween("\"Achievements\":{", "}}");
            gameData = "{" + gameData + "}";
            cheevos = "{" + cheevos + "}";

            var obj = JsonConvert.DeserializeObject<GameExtend>(gameData);
            var jcheevos = JsonConvert.DeserializeObject<JToken>(cheevos);

            obj.SetAchievements(jcheevos);

            return Task.FromResult(obj);
        }

        public async Task DownloadGameExtendImages(Game game)
        {
            await Task.Run(async () =>
            {
                GameExtend gamex = await GameExtend.Find(game.ID);
                Browser.dlGameExtendImages.Files = new List<DownloadFile> {
                    gamex.ImageTitleFile,
                    gamex.ImageIngameFile,
                    gamex.ImageBoxArtFile
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

                if (userData.IsEmpty()) return null;

                var user = JsonConvert.DeserializeObject<UserProgress>(userData);
                user.UserName = userName;
                user.GameID = gameID;

                return user;
            });
        }

        public async Task<User> GetUserInfo(string userName)
        {
            var user = new User();

            var file = API_File_UserInfo(userName);
            var dl = new Download(file) { };
            if (await dl.Start() == false) { return user; }

            return await Task.Run(() =>
            {
                var userData = File.ReadAllText(file.Path);
                if (userData.IsEmpty()) { return user; }

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
                var dl = new Download(file) { Overwrite = false };
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
                    user.LastGame.SetImageIconGridBitmap();

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
                    var totalPctWon = awardsList.Sum(x => x.PctWon);
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
            var gameExtend = await DeserializeGameExtend(game);
            if (gameExtend.ID == 0)
                gameExtend = await DownloadGameExtend(game, Browser.dlGames);
            if (gameExtend.IsNull())
            {
                MessageBox.Show("GameFile Not Found");
                return;
            }

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
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Picture pic = await Task.Run(() =>
                {
                    pic = new Picture(badgeNames, true, 11, GameBadgesSize, false);
                    pic.Save(game.MergedBadgesPath(), PictureFormat.Png, false);

                    Archive.SaveGameBadges(badgeNames, game.MergedBadgesPath() + ".txt");
                    return pic;
                });

                ImageViewerCommon.SetImage(pic, GameBadgesSize);

                stopwatch.Stop();
                //MessageBox.Show("Badges merged in: " + stopwatch.ElapsedSeconds() + "s");
            }

            if (badgeNames.IsEmpty())
            {
                MessageBox.Show("No Icons Found");
            }
        }

        public async Task MergeGamesIcon(Console console, bool getIncorrectSize = false)
        {
            if (console.IsNull()) { MessageBox.Show("No Console Selected"); return; }

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
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Picture pic = await Task.Run(() =>
                {
                    pic = new Picture(gamesIcon, true, 11, GameIconSize, false);
                    pic.Save(Game.MergedIconsPath(console.Name), PictureFormat.Jpg, false);

                    Archive.SaveGamesIcon(games, gamesIcon, Game.MergedIconsPath(console.Name) + ".txt");

                    return pic;
                });

                ImageViewerCommon.SetImage(pic, GameIconSize);

                stopwatch.Stop();
                //MessageBox.Show("Badges merged in: " + stopwatch.ElapsedSeconds() + "s");
            }

            if (gamesIcon.IsEmpty())
            {
                MessageBox.Show("No Icons Found");
            }
        }
        #endregion
    }
}