using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IO;
using System.Drawing;
using System.Windows.Forms;
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
        public static string HOST = "https://retroachievements.org/";

        //https://s3-eu-west-1.amazonaws.com/i.retroachievements.org
        public static string IMAGE_HOST = "http://media.retroachievements.org/Images/";
        public static string BADGE_HOST = "http://media.retroachievements.org/Badge/";

        //URLs
        public static string Game_URL(int gameID) { return HOST + "game/" + gameID.ToString(); }
        public static string User_URL(string userName) { return HOST + "user/" + userName; }

        //API
        private string API_HOST = HOST + "API/";
        private string API_UserName = "RADatabase";
        private string API_Key = "GRaWk9onm4B0LSWSFaDt5a2dQE3N8Yme";

        public DownloadFile API_Consoles()
        {
            return new DownloadFile(
                GetURL("API_GetConsoleIDs.php"),
                Folder.Console + "Consoles.json");
        }

        public DownloadFile API_GameList(Console console)
        {
            return new DownloadFile(
                GetURL("API_GetGameList.php", "i=" + console.ID),
                (Folder.GameData + console.Name + ".json").Replace("/", "-"));
        }

        public DownloadFile API_GameExtend(Game game)
        {
            return new DownloadFile(
                GetURL("API_GetGameExtended.php", "i=" + game.ID),
                (Folder.GameDataExtend(game.ConsoleID) + game.ID + ".json"));
        }

        public DownloadFile API_UserProgress(string userName, int gameID)
        {
            return new DownloadFile(
                GetURL("API_GetUserProgress.php", "u=" + userName + "&i=" + gameID),
                (Folder.User + "UserProgress.json"));
        }

        private static Size GameIconSize { get { return new Size(96, 96); } }
        private static Size GameBadgesSize { get { return new Size(64, 64); } }

        public static Bitmap DefaultIcon = new Picture(GameIconSize).Bitmap;
        public static Bitmap ErrorIcon = Resources.notfound;

        public static Bitmap DefaultTitleImage = new Picture(200, 150).Bitmap;
        public static Bitmap DefaultIngameImage = new Picture(200, 150).Bitmap;
        public static Bitmap DefaultBoxArtImage = new Picture(200, 150).Bitmap;

        public RA() { }

        private string AuthQS()
        {
            return "?z=" + API_UserName + "&y=" + API_Key;
        }

        private string GetURL(string target, string parames = "")
        {
            return API_HOST + target + AuthQS() + "&" + parames;
        }
        #endregion

        #region _Consoles
        public async Task DownloadConsoles()
        {
            Browser.dlConsoles.SetFile(API_Consoles());

            if (await Browser.dlConsoles.Start())
            {
                await new Console().Delete();
                await Console.InsertList(await DeserializeConsoles());
            }
        }

        private Task<List<Console>> DeserializeConsoles()
        {
            return Task<List<Console>>.Run(() =>
            {
                List<Console> consoles = JsonConvert.DeserializeObject<List<Console>>(File.ReadAllText(API_Consoles().Path));
                return consoles.OrderBy(x => x.ID).ToList();
            });
        }
        #endregion

        #region _GameList
        public async Task DownloadGameList(Console console)
        {
            await Task.Run(async () =>
            {
                Browser.dlGames.SetFile(API_GameList(console));

                if (await Browser.dlGames.Start())
                {
                    await Game.Delete(console.ID);
                    List<Game> list = await DeserializeGameList(console);
                    await Game.InsertList(list);
                }
            });
        }

        private Task<List<Game>> DeserializeGameList(Console console)
        {
            return Task<List<Game>>.Run(() =>
            {
                List<Game> list = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(API_GameList(console).Path));
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
            return Task<GameExtend>.Run(async () =>
            {
                dlExtend.SetFile(API_GameExtend(game));

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

        private Task<GameExtend> DeserializeGameExtend(Game game)
        {
            return Task<GameExtend>.Run(() =>
            {
                string AllText = File.ReadAllText(API_GameExtend(game).Path);
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
                string userData = await Browser.DownloadString(API_UserProgress(userName, gameID).URL, true);
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
        #endregion

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

                cForm.Open<frmImageViewer>();
                frmImageViewer f = cForm.Get<frmImageViewer>();
                f.SetImage(pic.Path, GameBadgesSize);
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
                    pic.Save(Game.MergedIconsPath(console.Name), PictureFormat.Jpg, false);

                    Archive.SaveGamesIcon(games, gamesIcon, Game.MergedIconsPath(console.Name) + ".txt");

                    return pic;
                });

                cForm.Open<frmImageViewer>();
                frmImageViewer f = cForm.Get<frmImageViewer>();
                f.SetImage(pic.Path, GameIconSize);
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
    }
}
