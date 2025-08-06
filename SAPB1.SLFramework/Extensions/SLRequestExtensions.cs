using B1SLayer;
using System.Text.Json;

namespace SAPB1.SLFramework.Extensions
{
    public static class SLRequestExtensions
    {
        /// <summary>
        /// Adds all of the given query parameters to the SLRequest.
        /// </summary>
        public static SLRequest SetQueryParams(this SLRequest request, IDictionary<string, string> query)
        {
            foreach (var kvp in query)
            {
                request.SetQueryParam(kvp.Key, kvp.Value);
            }
            return request;
        }
    }

}
