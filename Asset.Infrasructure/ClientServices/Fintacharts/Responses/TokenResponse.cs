using FinancialTask.Services.Fintacharts.Responses;
using Newtonsoft.Json;

namespace FinancialTask.ClientServices.Fintacharts.Responses
{
    public class TokenResponse : BaseHttpResponse
    {

        [JsonProperty("access_token")]
        public string Token { get; set; } = "";
        [JsonProperty("expires_in")]
        public uint ExpiresIn { get; set; } = 0;
    }
}