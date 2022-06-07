using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;

namespace RADB
{
    public static class Folder
    {
        public static void CreateFolders()
        {
            Directory.CreateDirectory(Base);

            Directory.CreateDirectory(GameInfoExtend);
            Directory.CreateDirectory(Consoles);
            Directory.CreateDirectory(IconsBase);

            Directory.CreateDirectory(Temp);
        }

        public static string Base
        {
            get
            {
                return @"Data\";
            }
        }

        public static string GameInfo
        {
            get
            {
                return Base + @"Game\";
            }
        }

        public static string GameInfoExtend
        {
            get
            {
                return Base + @"GameInfoExtend\";
            }
        }

        public static string Consoles
        {
            get
            {
                return Base + @"GameList\";
            }
        }

        public static string IconsBase
        {
            get
            {
                return Base + @"Icons\";
            }
        }

        public static string Titles
        {
            get
            {
                return Base + @"ImageTitle\";
            }
        }

        public static string InGame
        {
            get
            {
                return Base + @"ImageInGame\";
            }
        }

        public static string Temp
        {
            get
            {
                return Base + @"Temp\";
            }
        }

        public static string Achievements(int consoleID, int gameID)
        {
            string folder = Base + @"Achievements\" + consoleID + @"\" + gameID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string Icons(int consoleID)
        {
            string folder = IconsBase + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string ImageTitle(int consoleID)
        {
            string folder = Titles + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string ImageIngame(int consoleID)
        {
            string folder = InGame + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string GameInfoConsole(int consoleID)
        {
            string folder = GameInfo + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string GameInfoExtendConsole(int consoleID)
        {
            string folder = GameInfoExtend + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }
    }
}
