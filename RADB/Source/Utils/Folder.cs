using System.IO;
using System.Windows.Forms;

namespace RADB
{
    public static class Folder
    {
        public const string Base = @".\Data\";
        const string Json = Base + @"Json\";

        public const string User = Json + @"User\";
        public const string Console = Json + @"Console\";
        public const string GameData = Json + @"GameData\";
        public const string GameDataExtendBase = Json + @"GameDataExtend\";

        public const string Images = Base + @"Images\";
        public const string IconsBase = Images + @"Icons\";
        public const string BadgesBase = Images + @"Achievements\";

        public const string MergedIcons = Base + @"MergedIcons\";
        public const string MergedBadges = Base + @"MergedBadges\";

        public static void CreateFolders()
        {
            Directory.CreateDirectory(Base);
            Directory.CreateDirectory(Json);

            Directory.CreateDirectory(User);
            Directory.CreateDirectory(Console);
            Directory.CreateDirectory(GameData);
            Directory.CreateDirectory(GameDataExtendBase);

            Directory.CreateDirectory(Images);
            Directory.CreateDirectory(IconsBase);
            Directory.CreateDirectory(BadgesBase);

            Directory.CreateDirectory(MergedIcons);
            Directory.CreateDirectory(MergedBadges);
        }

        public static string GameDataExtend(int consoleID)
        {
            string folder = GameDataExtendBase + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string Icons(int consoleID)
        {
            if (consoleID == 0) MessageBox.Show("X");
            string folder = IconsBase + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string Titles(int consoleID)
        {
            string folder = Images + @"Titles\" + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string Ingame(int consoleID)
        {
            string folder = Images + @"Ingame\" + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string BoxArt(int consoleID)
        {
            string folder = Images + @"BoxArt\" + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string Achievements(int consoleID, int gameID)
        {
            string folder = Images + @"Achievements\" + consoleID + @"\" + gameID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }
    }
}
