using Newtonsoft.Json;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class ODataResult<TResult>
    {
        public TResult? Value { get; set; }
        [JsonProperty("@odata.count")]
        public long? Count { get; set; }
        [JsonProperty("@odata.context")]
        public string? Context { get; set; }
        [JsonProperty("@odata.nextLink")]
        public string? NextLink { get; set; }
    }
}
