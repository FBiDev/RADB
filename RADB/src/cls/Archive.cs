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
    }
}
