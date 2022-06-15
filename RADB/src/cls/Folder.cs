using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;

namespace RADB
{
    public static class Folder
    {
        private static string Base { get { return @"Data\"; } }
        private static string Images { get { return Base + @"Images\"; } }
        private static string Json { get { return Base + @"Json\"; } }
        private static string GameDataExtendBase { get { return Json + @"GameDataExtend\"; } }

        public static string Temp { get { return Base + @"Temp\"; } }
        public static string Console { get { return Json + @"Console\"; } }
        public static string GameData { get { return Json + @"GameData\"; } }

        public static void CreateFolders()
        {
            Directory.CreateDirectory(Base);
            Directory.CreateDirectory(Images);
            Directory.CreateDirectory(Json);
            Directory.CreateDirectory(GameDataExtendBase);

            Directory.CreateDirectory(Temp);
            Directory.CreateDirectory(Console);
            Directory.CreateDirectory(GameData);
        }

        public static string GameDataExtend(int consoleID)
        {
            string folder = GameDataExtendBase + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string Icons(int consoleID)
        {
            string folder = Images + @"Icons\" + consoleID + @"\";
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
            string folder = Base + @"Achievements\" + consoleID + @"\" + gameID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }
    }
}
