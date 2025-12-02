using System.Collections.Generic;

namespace RADB
{
    public class SpeedRunPagination
    {
        public SpeedRunPagination()
        {
            Links = new List<SpeedRunLink>();
        }

        public int Offset { get; set; }

        public int Max { get; set; }

        public int Size { get; set; }

        public List<SpeedRunLink> Links { get; set; }
    }
}