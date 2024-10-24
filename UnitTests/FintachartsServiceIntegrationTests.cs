using FinancialTask.ClientServices.Fintacharts.Types;
using FinancialTask.Services.Fintacharts;
using FinancialTask.Services.Fintacharts.Requests;
using FinancialTask.Services.Fintacharts.Responses;
using System.Net;

namespace Tests
{
    public class FintachartsServiceIntegrationTests
    {
        FintachartsService _service;
        private string token = "";

        [SetUp]
        public void Setup()
        {
            _service = new FintachartsService();
        }

        [OneTimeTearDown]
        public void Destructor()
        {
        }

        [Test]
        public async Task GetToken_TokenNotEmpty()
        {
            var response = await _service.GetToken(new TokenRequest());
            CheckOnSuccess(response);
            Assert.IsFalse(response.Token == null || response.Token.Length == 0, "Token is empty");
            if(response.Token is not null)
            {
                token = response.Token;
            }
        }

        [Test]
        public async Task GetMarketAssets_SuccessResult()
        {
            await GetToken_TokenNotEmpty();
            var request = new MarketAssetsRequest(token: token);

            var response = await _service.GetMarketAssets(request);
            CheckOnSuccess(response);
            Assert.IsNotNull(response.Data);
        }

        [Test]
        public async Task GetMarketAssets_EmptyToken_Unauthorized()
        {
            var response = await _service.GetMarketAssets(new MarketAssetsRequest());
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task GetAssetInfo_ReturnResultNotNull()
        {
            await GetToken_TokenNotEmpty();

            var request = new AssetInfoRequest(new AssetInfoRequest.AssetInfo()
            {
                Type = "l1-subscription",
                Id = "1",
                Kinds = [AssetKinds.Last, AssetKinds.Bid, AssetKinds.Ask],
                InstrumentId = new Guid("ad9e5345-4c3b-41fc-9437-1d253f62db52"),
                Provider = "simulation"
            }, token);

            var response = _service.GetAssetInfo(request);

            var maxCountResponses = 2;
            await foreach(var item in response)
            {
                Assert.IsNotNull(response);
                maxCountResponses--;
                if(maxCountResponses == 0) break;
            }
        }

        private void CheckOnSuccess(BaseHttpResponse response)
        {
            Assert.IsTrue(response.IsSuccessStatusCode, "That is not success response");
        }
    }
}