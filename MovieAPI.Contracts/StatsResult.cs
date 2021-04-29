using System;
using System.Collections.Generic;
using System.Text;

namespace MovieAPI.Contracts
{
    public class StatsResult
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public long averageWatchDurationS { get; set; }

        public int watches { get; set; }
        public int ReleaseYear { get; set; }
    }
}
