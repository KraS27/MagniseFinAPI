namespace MagniseFinAPI.Models
{
    public class MarketAsset
    {
        public string Id { get; set; } = string.Empty;

        public string Symbol { get; set; } = string.Empty;

        public string Kind { get; set; } = string.Empty;

        public string Exchange { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal TickSize { get; set; }

        public string Currency { get; set; } = string.Empty;

        public ICollection<Mapping>? Mappings { get; set; }
    }
}
