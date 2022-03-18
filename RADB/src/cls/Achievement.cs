using System;
using System.Collections.Generic;

namespace RADB
{
    public class Achievement
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public string BadgeName { get; set; }
        public int DisplayOrder { get; set; }

        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        
        public string MemAddr { get; set; }
        public int NumAwarded { get; set; }
        public int NumAwardedHardcore { get; set; }
        
        public int TrueRatio { get; set; }
    }
}
