using System.Collections.Generic;

namespace RADB
{
    public class SpeedRunPlatformSearch
    {
        public SpeedRunPlatformSearch()
        {
            Data = new List<SpeedRunPlatform>();
            Pagination = new SpeedRunPagination();
        }

        public List<SpeedRunPlatform> Data { get; set; }

        public SpeedRunPagination Pagination { get; set; }
    }
}