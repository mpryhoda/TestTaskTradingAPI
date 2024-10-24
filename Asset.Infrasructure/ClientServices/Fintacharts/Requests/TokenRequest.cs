using FinancialTask.ClientServices.Base;
using FinancialTask.ClientServices.Fintacharts.Configuration;
using FinancialTask.ClientServices.Fintacharts.Responses;

namespace FinancialTask.Services.Fintacharts.Requests
{
    public class TokenRequest : BaseHttpRequest<TokenResponse>
    {
        const string GrantTypeAttr = "grant_type";
        const string ClientIdAttr = "client_id";
        const string UsernameAttr = "username";
        const string PasswordAttr = "password";

        public TokenRequest(string grantType = FintachartsServiceConfig.GRANT_TYPE,
            string clientId = FintachartsServiceConfig.CLIENT_ID,
            string userName = FintachartsServiceConfig.USERNAME,
            string password = FintachartsServiceConfig.PASSWORD)
        {
            GrantType = grantType;
            ClientId = clientId;
            UserName = userName;
            Password = password;

            SetParameters(new[]
            {
                new KeyValuePair<string, string>(GrantTypeAttr, GrantType),
                new KeyValuePair<string, string>(ClientIdAttr, ClientId),
                new KeyValuePair<string, string>(UsernameAttr, UserName),
                new KeyValuePair<string, string>(PasswordAttr, Password),
            });
        }

        public string GrantType { get; init; }
        public string ClientId { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}