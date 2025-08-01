﻿using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.ServiceLayer
{
    public class CompanyInfoService(SLConnection connection) : ICompanyInfoService
    {
        private readonly SLConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        private readonly string _resource = "CompanyService_GetCompanyInfo";

        public async Task<CompanyInfo> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _connection.Request(_resource).GetAsync<CompanyInfo>();
        }
    }
}
