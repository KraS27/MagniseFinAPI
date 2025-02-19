﻿using System.Text.Json.Serialization;

namespace MagniseFinAPI.Models
{
    public class Mapping
    {
        public string Name { get; set; } = string.Empty;

        public string? Symbol { get; set; }

        public string? Exchange { get; set; }

        public int DefaultOrderSize { get; set; }

        [JsonIgnore]
        public MarketAsset? MarketAsset { get; set; }
    }
}
