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
            Directory.CreateDirectory(Icons);
            
            Directory.CreateDirectory(Json);
            Directory.CreateDirectory(Temp);
        }

        public static string Base
        {
            get
            {
                return @"Data\";
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

        public static string Icons
        {
            get
            {
                return Base + @"ImageIcon\";
            }
        }

        public static string Json
        {
            get
            {
                return Base + @"Json\";
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

        public static string ImageIcon(int consoleID)
        {
            string folder = Icons + consoleID + @"\";
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
