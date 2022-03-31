using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.IO;

namespace RADB
{
    public static class Archive
    {
        public static DateTime LastUpdate(string fileName)
        {
            return File.GetLastWriteTime(fileName);
        }

        public static string RelativePath(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            string path = info.DirectoryName.Replace(AppDomain.CurrentDomain.BaseDirectory, "") + "\\";
            return path;
        }

        public static bool IsFileLocked(string fileName)
        {
            try
            {
                using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }
    }
}
