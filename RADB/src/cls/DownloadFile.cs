using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RADB
{
    public class DownloadFile
    {
        public string URL { get; set; }
        public string Path { get; set; }

        public DownloadFile(string url = "", string path = "")
        {
            URL = url;
            Path = path;
        }
    }
}
