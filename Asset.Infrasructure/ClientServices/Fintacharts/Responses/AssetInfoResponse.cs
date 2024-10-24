using Asset.Domain;
using FinancialTask.ClientServices.Fintacharts.Types;
using System.Net;

namespace FinancialTask.Services.Fintacharts.Responses
{
    public class AssetInfoResponse: BaseHttpResponse
    {
        public AssetInfoResponse() { }
        public AssetInfoResponse(HttpStatusCode statusCode, string message = ""): base(message) 
        {
            StatusCode = statusCode;
        }

        public string? Type {  get; set; }
        public string? InstrumentId {  get; set; }
        public string? Provider {  get; set; }
        public AssetKindInfo? Ask {  get; set; }
        public AssetKindInfo? Bid {  get; set; }
        public AssetKindInfo? Last {  get; set; }

        public AssetEntity? ToAssetEntity()
        {
            var provider = Provider = Provider ?? string.Empty;
            var priceVal = Ask ?? Bid ?? Last ?? null;

            if(priceVal is null || InstrumentId is null) return null;

            Guid instrumentId = Guid.Empty;
            if (Guid.TryParse(InstrumentId, out Guid instId))
            {
                instrumentId = instId;
            }
            var entity = new AssetEntity(provider,
                                         instrumentId,
                                         priceVal.TimeStamp,
                                         priceVal.Price);

            return entity;
        }
    }
}