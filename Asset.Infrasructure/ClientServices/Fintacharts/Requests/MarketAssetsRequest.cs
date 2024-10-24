using FinancialTask.ClientServices.Base;
using FinancialTask.Services.Fintacharts.Responses;

namespace FinancialTask.Services.Fintacharts.Requests
{
    public class MarketAssetsRequest
        : BaseHttpRequest<MarketAssetsResponse>
    {
        const string ProviderAttr = "provider";
        const string KindAttr = "kind";
        const string SymbolAttr = "symbol";
        const string PageNumberAttr = "size";
        const string PageCountAttr = "page";

        public MarketAssetsRequest(
            string provider = "", string kind = "", string symbol = "", uint pageNumber = 1, uint pageCount = 10, string token = "")
        {
            Provider = provider;
            Kind = kind;
            Symbol = symbol;
            PageNumber = pageNumber;
            PageCount = pageCount;
            Token = token;

            SetParameters(
            [
                new KeyValuePair<string, string>(ProviderAttr, Provider),
                new KeyValuePair<string, string>(KindAttr, Kind),
                new KeyValuePair<string, string>(SymbolAttr, Symbol),
                new KeyValuePair<string, string>(PageNumberAttr, PageNumber.ToString()),
                new KeyValuePair<string, string>(PageCountAttr, PageCount.ToString()),
            ]);
        }

        public string Provider { get; }
        public string Kind { get; }
        public string Symbol { get; }
        public uint PageNumber { get; }
        public uint PageCount { get; }
        public string Token { get; }
    }
}