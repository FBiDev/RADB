using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;
using App.File.Json;

namespace RADB
{
    public partial class SpeedRunController
    {
        private const string RASiteURL = "https://retroachievements.org/";
        private const string RAAPI = RASiteURL + "API/";
        private const string RAAPIGameList = "API_GetGameList.php";

        private const string SpeedRunAPI = "https://www.speedrun.com/api/v1/";
        private const string SpeedRunFolder = "Data/SpeedRun/";
        private const string SpeedRunPlatformsFile = SpeedRunFolder + "platforms.json";

        #region Entrada
        public SpeedRunController(SpeedRunForm pageForm)
        {
            Page = pageForm;
            Page.Shown += ShownForm;
        }

        private void ShownForm(object sender, System.EventArgs ev)
        {
            SearchGameTextBox.KeyDown += SearchGame_ButtonClick;
            SearchGameButton.Click += (s, e) => SearchGame().TryAwait();
        }

        private void SearchGame_ButtonClick(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SearchGameButton.PerformClick();
            }
        }

        private async Task SearchGame()
        {
            var plataforms = await GetPlataforms();

            var searchGameURL = SpeedRunAPI + "games?name=";
            var searchGameOptions = "&orderby=released&direction=asc&max=20&offset=0"; // &romhack=false

            var searchName = SearchGameTextBox.Text.Trim();
            searchName = searchName.Replace(" ", "+");

            var searchJson = await Browser.DownloadString(searchGameURL + searchName + searchGameOptions);
            var searchObj = Json.DeserializeObject<SpeedRunGameSearch>(searchJson);

            // var jsonNew = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JToken>(searchJson);
            // var jsonNew2 = Json.DeserializeObject<Newtonsoft.Json.Linq.JToken>(searchJson);
            var jsonNew3 = Json.DeserializeObject<JToken>(searchJson);

            foreach (JProperty cheevo in jsonNew3)
            {
                var a = cheevo.Value.ToString();
            }

            // searchObj.data = null;
            searchObj.Data.ForEach(x =>
            {
                x.PlatformsIDs.ForEach(id =>
                {
                    var plat = plataforms.Where(p => p.Id == id).FirstOrDefault();

                    if (plat != null)
                    {
                        x.Platforms.Add(plat);
                    }
                });

                if (x.Platforms.Count > 0)
                {
                    x.Platforms = x.Platforms.OrderBy(o => o.Released).ToList();
                }
            });

            // RemoveHackGames(searchObj.data);
            SearchGameGrid.DataSource = new ListBind<SpeedRunGame>(searchObj.Data);

            var se = Json.Save(searchObj.Data, "test.json");

            // await RA.DownloadGameList(Session.Console);
        }

        private void RemoveHackGames(List<SpeedRunGame> gamesData)
        {
            for (int i = 0; i < gamesData.Count; i++)
            {
                if (gamesData[i].Platforms.Count == 0 || gamesData[i].RegionsIDs.Count == 0)
                {
                    gamesData.RemoveAt(i);
                    i--;
                }
            }
        }

        private async Task<List<SpeedRunPlatform>> GetPlataforms()
        {
            var searchJson = string.Empty;

            Directory.CreateDirectory(SpeedRunFolder);
            if (File.Exists(SpeedRunPlatformsFile) == false)
            {
                var searchPlatformURL = SpeedRunAPI + "platforms?";
                var searchPlatformOptions = "&max=200&offset=0";

                searchJson = await Browser.DownloadString(searchPlatformURL + searchPlatformOptions);
                File.WriteAllText(SpeedRunPlatformsFile, searchJson);
            }

            searchJson = File.ReadAllText(SpeedRunPlatformsFile);
            var searchObj = Json.DeserializeObject<SpeedRunPlatformSearch>(searchJson);

            return searchObj.Data;
        }
        #endregion

        private string GetURL(string target, string parames = "")
        {
            return RAAPI + target + "&" + parames;
        }

        public DownloadFile APIGameListFile(Console console)
        {
            return new DownloadFile(
                GetURL(RAAPIGameList, "i=" + console.ID),
                (Folder.GameData + console.Name + ".json").Replace("/", "-"));
        }

        public async Task DownloadGameList(Console console)
        {
            await Task.Run(async () =>
            {
                RASite.DLGames.SetFile(APIGameListFile(console));

                if (await RASite.DLGames.Start())
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

        private Task<List<Game>> DeserializeGameList(Console console)
        {
            return Task.Run(() =>
            {
                var games = Json.DeserializeObject<List<Game>>(File.ReadAllText(APIGameListFile(console).Path));
                return games;
            });
        }
    }
}