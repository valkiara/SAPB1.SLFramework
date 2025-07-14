using System.Text.Json;

namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    public interface IServiceLayerQueryService
    {
        Task<string> PostQueryAsync(string queryPath, string queryOption, CancellationToken cancellationToken = default);
    }
}
