using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;

namespace RADB
{
    public static class Folder
    {
        public static string Base { get { return @".\Data\"; } }
        public static string Images { get { return Base + @"Images\"; } }
        private static string Json { get { return Base + @"Json\"; } }

        public static string Temp { get { return Base + @"Temp\"; } }
        public static string Console { get { return Json + @"Console\"; } }
        public static string GameData { get { return Json + @"GameData\"; } }
        public static string GameDataExtendBase { get { return Json + @"GameDataExtend\"; } }

        public static string IconsBase { get { return Images + @"Icons\"; } }
        public static string BadgesBase { get { return Images + @"Achievements\"; } }

        public static void CreateFolders()
        {
            Directory.CreateDirectory(Base);
            Directory.CreateDirectory(Images);
            Directory.CreateDirectory(Json);
            Directory.CreateDirectory(GameDataExtendBase);

            Directory.CreateDirectory(Temp);
            Directory.CreateDirectory(Console);
            Directory.CreateDirectory(GameData);

            Directory.CreateDirectory(IconsBase);
            Directory.CreateDirectory(BadgesBase);
        }

        public static string GameDataExtend(int consoleID)
        {
            string folder = GameDataExtendBase + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string Icons(int consoleID)
        {
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
