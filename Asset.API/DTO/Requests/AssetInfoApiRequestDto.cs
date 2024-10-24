using FinancialTask.ClientServices.Fintacharts.Types;
using FinancialTask.Services.Fintacharts.Requests;

namespace Asset.API.DTO.Requests
{
    public class AssetInfoApiRequestDto
    {
        public AssetInfo Data { get; set; } = new AssetInfo();
        public string Token { get; set; } = "";

        public AssetInfoRequest MapToAssetInfoRequest()
        {
            var result = new AssetInfoRequest(new AssetInfoRequest.AssetInfo()
            {
                Type = this.Data.Type,
                Id = this.Data.Id,
                InstrumentId = this.Data.InstrumentId,
                Provider = this.Data.Provider,
                Subscribe = this.Data.Subscribe
            },
            Token);
            result.Data.Kinds = Data.Kinds.Select(e => Enum.Parse<AssetKinds>(e, ignoreCase: true)).ToArray();

            return result;
        }
    }

    public class AssetInfo()
    {
        public string? Type { get; set; }
        public string? Id { get; set; }
        public Guid InstrumentId { get; set; }
        public string? Provider { get; set; }
        public bool Subscribe { get; set; } = true;
        public string[] Kinds { get; set; } = { };
    }
}