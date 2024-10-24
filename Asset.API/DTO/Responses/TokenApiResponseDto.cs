using Newtonsoft.Json;

namespace Asset.API.DTO.Responses
{
    public class TokenApiResponseDto(string token = "", uint expireIn = 0): BaseApiResponseDto
    {
        [JsonProperty("access_token")]
        public string Token { get; set; } = token;
        [JsonProperty("expires_in")]
        public uint ExpiresIn { get; set; } = expireIn;
    }
}