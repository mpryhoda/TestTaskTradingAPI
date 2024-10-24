using FinancialTask.Services.Fintacharts.Requests;

namespace Asset.API.DTO.Requests
{
    public class MarketAssetsApiRequestDto
    {
       public MarketAssets Data { get; set; }
       public string Token {  get; set; }

       public MarketAssetsApiRequestDto(MarketAssets data, string token)
        {
            Data = data;
            Token = token;
        }

        public class MarketAssets(
            string provider = "", string kind = "", string symbol = "", uint pageNumber = 1, uint pageCount = 10)
        {
            public string Provider { get; init; } = provider;
            public string Kind { get; init; } = kind;
            public string Symbol { get; init; } = symbol;
            public uint PageNumber { get; init; } = pageNumber;
            public uint PageCount { get; init; } = pageCount;
        }


        public MarketAssetsRequest MapToMarketAssetsRequest()
        {
            return new MarketAssetsRequest(Data.Provider,
                    Data.Kind,
                    Data.Symbol,
                    Data.PageNumber,
                    Data.PageCount,
                    Token = Token
                );
        }
    }
}