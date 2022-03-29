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
                return Base + @"game\";
            }
        }

        public static string Badges(int gameID)
        {
            string folder = Game + gameID + @"\badges\";
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string GameID(int gameID)
        {
            string folder = Game + gameID + @"\";
            Directory.CreateDirectory(folder);
            return folder;
        }
    }
}
