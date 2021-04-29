using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieAPI.Contracts
{
    public class MoviesStats
    {
        [JsonProperty(PropertyName = "averageWatchDurationS")]
        public long watchDurationMs { get; set; }

        //[JsonProperty(PropertyName = "watches")]
        //public long watches { get; set; }

        public long movieId { get; set; }
    }
}
