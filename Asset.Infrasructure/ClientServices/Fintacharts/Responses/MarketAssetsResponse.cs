using Newtonsoft.Json;

namespace FinancialTask.Services.Fintacharts.Responses
{
    public class MarketAssetsResponse : BaseHttpResponse
    {
        public MarketAssetData[] Data { get; set; } = Array.Empty<MarketAssetData>();

        public class MarketAssetData()
        {
            public Guid Id { get; set; }
            public string? Symbol { get; set; }
            public string? Kind { get; set; }
            public string? Exchange { get; set; }
            public string? Description { get; set; }
            public double? TickSize { get; set; }
            public string? Currency { get; set; }
            public string? BaseCurrency { get; set; }
            public Profile? Profile { get; set; }
        }

        public class Profile()
        {
            public string? Name {  get; set; }
            public string? Location {  get; set; }
            public Gics? gics { get; set; }

            public class Gics 
            {
                public int? SectorId { get; set; }
                public int? IndustryGroupId { get; set; }
                public int? IndustryId { get; set; }
                public int? SubIndustryId { get; set; }
            }
        }
    }
}

    