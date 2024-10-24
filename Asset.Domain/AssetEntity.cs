namespace Asset.Domain
{
    public class AssetEntity
    {
        public AssetEntity(string provider, Guid instrumentId, DateTime timestamp, decimal price)
        {
            Provider = provider;
            InstrumentId = instrumentId;
            Timestamp = timestamp;
            Price = price;
        }

        public string? Type { get; set; }
        public string Provider { get; set; }
        public Guid InstrumentId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Price { get; set; }
        public long? Volume { get; set; }
    }
}