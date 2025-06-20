﻿using SAPB1.SLFramework.Converters;
using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class ODataResult<TResult>
    {
        [JsonPropertyName("value")]
        public TResult? Value { get; set; }

        [JsonPropertyName("@odata.count")]
        [JsonConverter(typeof(FlexibleLongConverter))]
        public long? Count { get; set; }

        [JsonPropertyName("@odata.context")]
        public string? Context { get; set; }

        [JsonPropertyName("@odata.nextLink")]
        public string? NextLink { get; set; }
    }
}
