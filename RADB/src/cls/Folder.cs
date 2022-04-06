using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RADB
{
    public static class Folder
    {
        public static void CreateFolders()
        {
            Directory.CreateDirectory(Base);
            Directory.CreateDirectory(GameInfoExtend);
            Directory.CreateDirectory(Json);
            Directory.CreateDirectory(Consoles);
            Directory.CreateDirectory(Temp);
        }

        public static string Base
        {
            get
            {
                return @"Data\";
            }
        }

        public static string Temp
        {
            get
            {
                return Base + @"Temp\";
            }
        }

        public static string GameInfoExtend
        {
            get
            {
                return Base + @"GameInfoExtend\";
            }
        }

        public static string Json
        {
            get
            {
                return Base + @"Json\";
            }
        }

        public static string Consoles
        {
            get
            {
                return Base + @"GameList\";
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
            string folder = Base + @"ImageIcon\" + consoleID + @"\";
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
