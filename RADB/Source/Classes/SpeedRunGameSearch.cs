using System.Collections.Generic;

namespace RADB
{
    public class SpeedRunLink
    {
        public SpeedRunLink()
        {
            rel = uri = string.Empty;
        }

        public string rel { get; set; }

        public string uri { get; set; }
    }

    public class SpeedRunPagination
    {
        public SpeedRunPagination()
        {
            links = new List<SpeedRunLink>();
        }

        public int offset { get; set; }

        public int max { get; set; }

        public int size { get; set; }

        public List<SpeedRunLink> links { get; set; }
    }

    public class SpeedRunGameSearch
    {
        public SpeedRunGameSearch()
        {
            data = new List<SpeedRunGame>();
            pagination = new SpeedRunPagination();
        }

        public List<SpeedRunGame> data { get; set; }

        public SpeedRunPagination pagination { get; set; }
    }

    public class SpeedRunPlatformSearch
    {
        public SpeedRunPlatformSearch()
        {
            data = new List<SpeedRunPlatform>();
            pagination = new SpeedRunPagination();
        }

        public List<SpeedRunPlatform> data { get; set; }

        public SpeedRunPagination pagination { get; set; }
    }
}