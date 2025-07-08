using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    public interface ICompanyInfoService
    {
        Task<CompanyInfo> GetAsync(CancellationToken cancellationToken = default);
    }
}
