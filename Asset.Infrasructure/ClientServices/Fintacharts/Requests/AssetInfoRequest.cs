using FinancialTask.ClientServices.Fintacharts.Types;

namespace FinancialTask.Services.Fintacharts.Requests
{
    public class AssetInfoRequest
    {
        public AssetInfoRequest(AssetInfo data, string token)
        {
            Data = data;
            Token = token;
        }

        public AssetInfo Data { get; set; }
        public string Token { get; set; }

        public class AssetInfo
        {
            public string? Type { get; set; }
            public string? Id { get; set; }
            public Guid? InstrumentId { get; set; }
            public string? Provider { get; set; }
            public bool Subscribe { get; set; } = true;
            public AssetKinds[]? Kinds { get; set; }
        }
    }
}