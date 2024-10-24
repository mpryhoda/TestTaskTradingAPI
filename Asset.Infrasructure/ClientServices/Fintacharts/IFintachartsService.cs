using FinancialTask.ClientServices.Fintacharts.Responses;
using FinancialTask.Services.Fintacharts.Requests;
using FinancialTask.Services.Fintacharts.Responses;

namespace FinancialTask.Services.Fintacharts
{
    public interface IFintachartsService
    {
        Task<TokenResponse> GetToken(TokenRequest request);
        Task<MarketAssetsResponse> GetMarketAssets(MarketAssetsRequest request);
        IAsyncEnumerable<AssetInfoResponse> GetAssetInfo(AssetInfoRequest request);
    }
}
