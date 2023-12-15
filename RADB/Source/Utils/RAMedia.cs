using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RADB
{
    public static class RAMedia
    {
        public static void SaveGamesIcon(List<Game> games, List<string> list, string fileName)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                foreach (string item in list)
                {
                    var imageName = item.Split('\\').Last();
                    Game game = games.FirstOrDefault(g => g.ImageIcon == imageName) ?? new Game();
                    string GameID = game.ID.ToString().PadLeft(5) + " => " + game.Title;
                    sw.WriteLine(imageName + " => " + GameID);
                }
            }
        }

        public static void SaveGameBadges(List<string> badges, string fileName)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                foreach (string item in badges)
                {
                    var imageName = item.Split('\\').Last().PadLeft(12);

                    sw.WriteLine(imageName);
                }
            }
        }
    }
}