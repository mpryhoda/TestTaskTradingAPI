using Asset.API.DTO.Requests;
using Asset.API.DTO.Responses;
using Asset.Domain;
using FinancialTask.Services.Fintacharts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinancialTask.Controllers
{
    [ApiController]
    public class AssetController : ControllerBase
    {
        public readonly IFintachartsService _fintachartsService;
        private readonly IAssetRepository assetRepository;

        public AssetController(IFintachartsService fintachartsService, IAssetRepository assetRepository)
        {
            _fintachartsService = fintachartsService;
            this.assetRepository = assetRepository;
        }

        [HttpPost]
        [Route("/GetToken")]
        public async Task<TokenApiResponseDto> GetToken([FromBody] TokenApiRequestDto request)
        {
            var token = await _fintachartsService.GetToken(request.MapToTokenRequest());

            if (!token.IsSuccessStatusCode) 
                return new TokenApiResponseDto() { StatusCode = HttpStatusCode.BadRequest };

            return new TokenApiResponseDto()
            {
                ExpiresIn = token.ExpiresIn,
                Token = token.Token
            };
        }

        [HttpPost]
        [Route("/GetMarketAssets")]
        public async Task<MarketAssetsApiResponseDto> GetMarketAssets([FromBody] MarketAssetsApiRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return new MarketAssetsApiResponseDto() { StatusCode = HttpStatusCode.Unauthorized };
            }

            var marketAssets = await _fintachartsService.GetMarketAssets(request.MapToMarketAssetsRequest());

            if (!marketAssets.IsSuccessStatusCode)
                return new MarketAssetsApiResponseDto() { StatusCode = HttpStatusCode.BadRequest, Message = marketAssets.Message };

            var result = MarketAssetsApiResponseDto.GetFromMarketAssetsResponse(marketAssets);
            return result;
        }

        [HttpPost]
        [Route("/GetAssetInfo")]
        public async IAsyncEnumerable<AssetInfoApiResponseDto> GetAssetInfo([FromBody] AssetInfoApiRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                yield return new AssetInfoApiResponseDto() { StatusCode = HttpStatusCode.Unauthorized };
                yield break;
            }

            var assetsInfo = _fintachartsService.GetAssetInfo(request.MapToAssetInfoRequest());

            await foreach (var item in assetsInfo)
            {
                if (item == null) break;
                
                var assetEntity = item.ToAssetEntity();
                if (assetEntity is null) continue;

                await assetRepository.AddAsync(assetEntity);

                yield return AssetInfoApiResponseDto.FromAssetInfoResponse(item);
            }
        }
    }
}
