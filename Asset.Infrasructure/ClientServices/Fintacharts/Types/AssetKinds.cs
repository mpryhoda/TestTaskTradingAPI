using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace FinancialTask.ClientServices.Fintacharts.Types
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AssetKinds
    {
        Ask,
        Bid,
        Last
    }
}
