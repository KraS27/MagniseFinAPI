namespace MagniseFinAPI.Models
{
    public class Mapping
    {
        public string Name { get; set; } = string.Empty;

        public string? Symbol { get; set; }

        public string? Exchange { get; set; }

        public int DefaultOrderSize { get; set; }

        public ICollection<MarketAsset>? MarketAssets { get; set; }
    }
}
