using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using System.Text.Json;

namespace SAPB1.SLFramework.ServiceLayer
{
    public class ServiceLayerQueryService : IServiceLayerQueryService
    {
        private readonly SLConnection _connection;

        public ServiceLayerQueryService(SLConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> PostQueryAsync(string queryPath, string queryOption, CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                QueryPath = queryPath,
                QueryOption = queryOption
            };

            var result = await _connection.Request("QueryService_PostQuery")
                                          .PostAsync<JsonDocument>(payload, unwrapCollection: false);

            return result.RootElement.GetRawText(); // return full raw JSON as string
        }

    }
}
