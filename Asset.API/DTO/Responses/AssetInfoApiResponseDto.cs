using FinancialTask.ClientServices.Fintacharts.Types;
using FinancialTask.Services.Fintacharts.Responses;

namespace Asset.API.DTO.Responses
{
    public class AssetInfoApiResponseDto : BaseApiResponseDto
    {
        public string? Type { get; set; }
        public string? InstrumentId { get; set; }
        public string? Provider { get; set; }
        public AssetKindInfo? Ask { get; set; }
        public AssetKindInfo? Bid { get; set; }
        public AssetKindInfo? Last { get; set; }

        public static AssetInfoApiResponseDto FromAssetInfoResponse(AssetInfoResponse assetInfoResponse)
        {
            return new AssetInfoApiResponseDto()
            {
                Ask = assetInfoResponse.Ask,
                Bid = assetInfoResponse.Bid,
                Last = assetInfoResponse.Last,
                InstrumentId = assetInfoResponse.InstrumentId,
                Provider = assetInfoResponse.Provider,
                Type = assetInfoResponse.Type,
            };
        }
    }
}