using FinancialTask.Services.Fintacharts.Responses;
using System.Text.Json.Serialization;

namespace Asset.API.DTO.Responses
{
    public class MarketAssetsApiResponseDto : BaseApiResponseDto
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
            public string? Name { get; set; }
            public string? Location { get; set; }
            public Gics? Gics { get; set; }
        }
        public class Gics
        {
            public int? SectorId { get; set; }
            public int? IndustryGroupId { get; set; }
            public int? IndustryId { get; set; }
            public int? SubIndustryId { get; set; }
        }

        public static MarketAssetsApiResponseDto GetFromMarketAssetsResponse(MarketAssetsResponse marketAssets)
        {
            var result = new MarketAssetsApiResponseDto();
            if(marketAssets.Data.Length == 0) return result;
            result.Data = new MarketAssetData[marketAssets.Data.Length];

            var i = 0;
            foreach (var marketAsset in marketAssets.Data)
            {
                result.Data[i] = new MarketAssetData();
                var data = result.Data[i];
                data.Description = marketAsset.Description;
                data.Currency = marketAsset.Currency;
                data.BaseCurrency = marketAsset.BaseCurrency;
                data.Id = marketAsset.Id;
                data.Symbol = marketAsset.Symbol;
                data.TickSize = marketAsset.TickSize;
                data.Kind = marketAsset.Kind;
                data.Exchange = marketAsset.Exchange;

                if (marketAsset.Profile != null)
                {
                    data.Profile = new Profile();
                    data.Profile.Name = marketAsset.Profile.Name;
                    data.Profile.Location = marketAsset.Profile.Location;
                    if (marketAsset.Profile.gics != null)
                    {
                        var apiGics = data.Profile.Gics = new Gics();
                        var serviceGics = marketAsset.Profile.gics;
                        apiGics.IndustryGroupId = serviceGics.IndustryGroupId;
                        apiGics.SectorId = serviceGics.SectorId;
                        apiGics.SubIndustryId = serviceGics.SubIndustryId;
                        apiGics.IndustryId = serviceGics.IndustryId;
                    }
                }
                i++;
            }
            return result;
        }
    }
}

