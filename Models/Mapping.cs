namespace MagniseFinAPI.Models
{
    public class Mapping
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Symbol { get; set; }

        public string? Exchange { get; set; }

        public int DefaultOrderSize { get; set; }

        public string? MarketAssetId { get; set; }
        public MarketAsset? MarketAsset { get; set; }
    }
}
