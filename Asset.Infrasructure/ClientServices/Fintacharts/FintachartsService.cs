using FinancialTask.ClientServices.Fintacharts.Configuration;
using FinancialTask.ClientServices.Fintacharts.Responses;
using FinancialTask.Services.Fintacharts.Requests;
using FinancialTask.Services.Fintacharts.Responses;
using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace FinancialTask.Services.Fintacharts
{
    public class FintachartsService : IFintachartsService
    {
        static HttpClient Client = (Client) ?? new HttpClient();

        public async Task<TokenResponse> GetToken(TokenRequest request)
        {
            request.RequestUri = new Uri(FintachartsServiceConfig.URI_TOKEN);
            request.Method = HttpMethod.Post;
            var response = (TokenResponse)await request.GetResponse();
            return response;
        }

        public async Task<MarketAssetsResponse> GetMarketAssets(MarketAssetsRequest request)
        {
            request.RequestUri = new Uri(FintachartsServiceConfig.URI_MARKET_ASSETS);
            request.Method = HttpMethod.Get;
            request.AddTokenInAuthenticationHeader(request.Token);
            var response = (MarketAssetsResponse)await request.GetResponse();
            return response;
        }

        public async IAsyncEnumerable<AssetInfoResponse> GetAssetInfo(AssetInfoRequest request)
        {
            AssetInfoResponse? result = null;

            using (var clientWebSocket = new ClientWebSocket())
            {
                var serviceUri = new Uri($"{FintachartsServiceConfig.URI_WSS}{request.Token}");
                var cts = new CancellationTokenSource();
                try
                {
                    await CreateSocketConnection(request, clientWebSocket, serviceUri, cts);
                }
                catch(Exception ex)
                {
                    result = new AssetInfoResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                if (result != null)
                {
                    yield return result;
                    yield break;
                }     
                while (request.Data.Subscribe && !clientWebSocket.CloseStatus.HasValue)
                {
                    try
                    {
                        result = await ReadSocketResponse(result, clientWebSocket, cts);
                    }
                    catch (Exception ex)
                    {
                        result = new AssetInfoResponse(HttpStatusCode.InternalServerError, ex.Message);
                    }
                    if (result == null)
                    {
                        yield return new AssetInfoResponse(HttpStatusCode.ServiceUnavailable);
                        yield break;
                    }
                    yield return result;
                }
            }
        }

        private async Task CreateSocketConnection(AssetInfoRequest request, ClientWebSocket clientWebSocket, Uri serviceUri, CancellationTokenSource cts)
        {
            await clientWebSocket.ConnectAsync(serviceUri, cts.Token);
            var jsonTextRequest = JsonConvert.SerializeObject(request.Data);
            var requestSocket = new ArraySegment<byte>(UTF8Encoding.UTF8.GetBytes(jsonTextRequest));
            await clientWebSocket.SendAsync(requestSocket, WebSocketMessageType.Binary, true, cts.Token);
        }

        private static async Task<AssetInfoResponse?> ReadSocketResponse(AssetInfoResponse? result, ClientWebSocket clientWebSocket, CancellationTokenSource cts)
        {
            var responseBuffer = new byte[1024];
            ArraySegment<byte> bytesReceive = new ArraySegment<byte>(responseBuffer);
            var response = await clientWebSocket.ReceiveAsync(bytesReceive, cts.Token);
            var respMessage = UTF32Encoding.UTF8.GetString(responseBuffer, 0, response.Count);
            result = JsonConvert.DeserializeObject<AssetInfoResponse>(respMessage);
            return result;
        }
    }
}
