using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieAPI.Contracts
{
    [Serializable]
    public class Movie
    {
        [JsonProperty(PropertyName = "movieId")]
        public long MovieId        { get; set; }

        [DataType(DataType.Text), MaxLength(100), Required]
        [JsonProperty(PropertyName = "title")]
        public string Title        { get; set; }

        [DataType(DataType.Text), MaxLength(50)]
        [JsonProperty(PropertyName = "language")]
        public string Language        { get; set; }

        [DataType(DataType.Text), MaxLength(750)]
        [JsonProperty(PropertyName = "duration")]
        public string Duration        { get; set; }

        [JsonProperty(PropertyName = "releaseYear")]
        public int ReleaseYear        { get; set; }
    }
}
