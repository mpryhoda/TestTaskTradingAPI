using FinancialTask.ClientServices.Fintacharts.Configuration;
using FinancialTask.Services.Fintacharts.Requests;

namespace Asset.API.DTO.Requests
{
    public class TokenApiRequestDto
    {
        public TokenApiRequestDto(string grantType = FintachartsServiceConfig.GRANT_TYPE,
            string clientId = FintachartsServiceConfig.CLIENT_ID,
            string userName = FintachartsServiceConfig.USERNAME,
            string password = FintachartsServiceConfig.PASSWORD)
        {
            GrantType = grantType;
            ClientId = clientId;
            UserName = userName;
            Password = password;
        }

        public required string GrantType { get; init; }
        public required string ClientId { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }

        public TokenRequest MapToTokenRequest()
        {
            return new TokenRequest()
            {
                ClientId = this.ClientId,
                UserName = this.UserName,
                Password = this.Password,
                GrantType = this.GrantType,
            };
        }
    }
}