using System.Collections.Generic;

namespace RADB
{
    public class SpeedRunGameSearch
    {
        public SpeedRunGameSearch()
        {
            Data = new List<SpeedRunGame>();
            Pagination = new SpeedRunPagination();
        }

        public List<SpeedRunGame> Data { get; set; }

        public SpeedRunPagination Pagination { get; set; }
    }
}