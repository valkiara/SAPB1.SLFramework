namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    /// <summary>
    /// Ensures that SAP B1 User-Defined Tables (UDTs) and User-Defined Fields (UDFs)
    /// are created or updated based on metadata definitions.
    /// </summary>
    public interface IMetadataProvisioner
    {
        /// <summary>
        /// Creates or updates all UDTs and UDFs according to the defined metadata.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task EnsureAsync(CancellationToken cancellationToken = default);
    }
}
