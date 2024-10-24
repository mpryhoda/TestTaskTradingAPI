namespace FinancialTask.ClientServices.Fintacharts.Configuration
{
    public class FintachartsServiceConfig
    {
        public const string URI = "https://platform.fintacharts.com";
        public const string URI_WSS = "wss://platform.fintacharts.com/api/streaming/ws/v1/realtime?token=";
        public const string URI_MARKET_ASSETS = $"{URI}/api/instruments/v1/instruments";
        public const string URI_TOKEN = $"{URI}/identity/realms/fintatech/protocol/openid-connect/token";

        public const string USERNAME = "r_test@fintatech.com";
        public const string PASSWORD = "kisfiz-vUnvy9-sopnyv";
        public const string GRANT_TYPE = "password";
        public const string CLIENT_ID = "app-cli";

    }
}
