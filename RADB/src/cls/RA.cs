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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RADB.Properties;
using GNX;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Management;

namespace RADB
{
    public class RA
    {
        #region _Main
        private string API_URL = "https://retroachievements.org/API/";
        private string API_UserName;
        private string API_Key;

        //URLs
        public static string URL_Images = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Images/";
        public static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";

        public string API_Consoles { get { return GetURL("API_GetConsoleIDs.php"); } }
        public string API_GameList(int consoleID) { return GetURL("API_GetGameList.php", "i=" + consoleID); }
        public string API_GameExtend(int gameID) { return GetURL("API_GetGameExtended.php", "i=" + gameID); }
        public string API_UserProgress(string userName, int gameID) { return GetURL("API_GetUserProgress.php", "u=" + userName + "&i=" + gameID); }



        private static Size GameIconSize { get { return new Size(96, 96); } }
        private static Size GameBadgesSize { get { return new Size(64, 64); } }

        public static Bitmap DefaultIcon = new Picture(GameIconSize).Bitmap;
        public static Bitmap ErrorIcon = Resources.notfound;

        public static Bitmap DefaultTitleImage = new Picture(200, 150).Bitmap;
        public static Bitmap DefaultIngameImage = new Picture(200, 150).Bitmap;
        public static Bitmap DefaultBoxArtImage = new Picture(200, 150).Bitmap;

        public RA()
        {
            API_UserName = "RADatabase";
            API_Key = "GRaWk9onm4B0LSWSFaDt5a2dQE3N8Yme";
        }

        private string AuthQS()
        {
            return "?z=" + API_UserName + "&y=" + API_Key;
        }

        private string GetURL(string target, string parames = "")
        {
            return API_URL + target + AuthQS() + "&" + parames;
        }
        #endregion

        #region _Consoles
        public string ConsolesPath()
        {
            return Folder.Console + "Consoles.json";
        }

        public async Task DownloadConsoles()
        {
            Browser.dlConsoles.Files = new List<DownloadFile> { new DownloadFile(API_Consoles, ConsolesPath()) };

            if (await Browser.dlConsoles.Start())
            {
                await Console.Excluir();
                await Console.IncluirLista(await DeserializeConsoles());
            }
        }

        private Task<List<Console>> DeserializeConsoles()
        {
            return Task<List<Console>>.Run(() =>
            {
                List<Console> consoles = JsonConvert.DeserializeObject<List<Console>>(File.ReadAllText(ConsolesPath()));
                return consoles.OrderBy(x => x.ID).ToList();
            });
        }
        #endregion

        #region _GameList
        public string GameListPath(string consoleName)
        {
            return (Folder.GameData + consoleName + ".json").Replace("/", "-");
        }

        public async Task DownloadGameList(Console console)
        {
            await Task.Run(async () =>
            {
                Browser.dlGames.Files = new List<DownloadFile> { new DownloadFile(API_GameList(console.ID), GameListPath(console.Name)) };

                if (await Browser.dlGames.Start())
                {
                    await Game.Excluir(console.ID);
                    List<Game> list = await DeserializeGameList(console.Name);
                    await Game.IncluirLista(list);
                }
            });
        }

        private Task<List<Game>> DeserializeGameList(string consoleName)
        {
            return Task<List<Game>>.Run(() =>
            {
                List<Game> list = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(GameListPath(consoleName)));
                return list;
            });
        }

        public async Task DownloadGamesIcon(Console console, Download dl)
        {
            await Task.Run(async () =>
            {
                List<Game> games = await Game.Listar(console.ID, true);
                dl.Files = games.Select(g => g.ImageIconFile).ToList();
                await (dl.Start());
            });
        }
        #endregion

        #region _GameExtend
        public string GameExtendPath(Game game)
        {
            return (Folder.GameDataExtend(game.ConsoleID) + game.ID + ".json");
        }

        public Task<GameExtend> DownloadGameExtend(Game game, Download dlExtend)
        {
            return Task<GameExtend>.Run(async () =>
            {
                dlExtend.Files = new List<DownloadFile>() { new DownloadFile(API_GameExtend(game.ID), GameExtendPath(game)) };

                if (await (dlExtend.Start()))
                {
                    GameExtend obj = (await DeserializeGameExtend(game));
                    obj.ID = game.ID;
                    obj.ConsoleID = game.ConsoleID;
                    await obj.Excluir();
                    obj.Incluir();
                    return obj;
                }
                return null;
            });
        }

        private Task<GameExtend> DeserializeGameExtend(Game game)
        {
            return Task<GameExtend>.Run(() =>
            {
                string AllText = File.ReadAllText(GameExtendPath(game));
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
                GameExtend gamex = await GameExtend.Listar(game.ID);
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
                string userData = await Browser.DownloadString(API_UserProgress(userName, gameID), true);
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

            //Game game = (await Game.Listar(new Game { ID = gameID })).FirstOrDefault();
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
                    pic.Save(game.BadgesMerged(), PictureFormat.Png, false);

                    Archive.SaveGameBadges(game, badgeNames, game.BadgesMerged() + ".txt");
                    return pic;
                });

                cForm.Open<frmImageViewer>();
                frmImageViewer f = cForm.Get<frmImageViewer>();
                f.SetImage(pic.Path);
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
            var games = (await Game.Listar(console.ID, true)).Where(g => g.NumAchievements > 0).ToList();
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
                    pic.Save(Game.IconsMerged(console.Name), PictureFormat.Png, false);

                    Archive.SaveGamesIcon(games, gamesIcon, Game.IconsMerged(console.Name) + ".txt");

                    return pic;
                });

                cForm.Open<frmImageViewer>();
                frmImageViewer f = cForm.Get<frmImageViewer>();
                f.SetImage(pic.Path);
            }

            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;

            if (gamesIcon.Empty())
            {
                MessageBox.Show("No Icons Found");
            }

            //MessageBox.Show("Badges merged in: " + fim0.TotalSeconds + "s");
            return;
        }
    }
}
