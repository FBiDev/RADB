using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RADB
{
    public static class RAMedia
    {
        // https://s3-eu-west-1.amazonaws.com/i.retroachievements.org
        public const string ConsoleIconBaseUrl = "https://static.retroachievements.org/assets/images/system/";
        public const string GameImageBaseUrl = "http://media.retroachievements.org/Images/";
        public const string BadgeBaseUrl = "http://media.retroachievements.org/Badge/";
        public const string UserPicBaseUrl = "http://media.retroachievements.org/UserPic/";

        public static void SaveGamesIcon(List<Game> games, List<string> list, string fileName)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                foreach (string item in list)
                {
                    var imageName = item.Split('\\').Last();
                    Game game = games.FirstOrDefault(g => g.ImageIcon == imageName) ?? new Game();
                    string gameID = game.ID.ToString().PadLeft(5) + " => " + game.Title;
                    sw.WriteLine(imageName + " => " + gameID);
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