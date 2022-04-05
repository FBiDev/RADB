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
            Directory.CreateDirectory(Game);
            Directory.CreateDirectory(Json);
            Directory.CreateDirectory(Consoles);
            Directory.CreateDirectory(Temp);
        }

        public static string Base
        {
            get
            {
                return @"data\";
            }
        }

        public static string Temp
        {
            get
            {
                return Base + @"temp\";
            }
        }

        public static string Game
        {
            get
            {
                return Base + @"gamelist\";
            }
        }

        public static string Json
        {
            get
            {
                return Base + @"json\";
            }
        }

        public static string Consoles
        {
            get
            {
                return Base + @"consoles\";
            }
        }

        public static string Badges(int consoleID, int gameID)
        {
            string folder = Game + consoleID + @"\" + gameID + @"\badges\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string GameInfo(int consoleID)
        {
            string folder = Game + consoleID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }
    }
}
