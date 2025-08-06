using B1SLayer;
using Flurl.Http;
using SAPB1.SLFramework.Abstractions.Interfaces;
using System.Globalization;
using System.Text.Json;

namespace SAPB1.SLFramework.ServiceLayer
{
    public class SBOBobService(SLConnection connection) : ISBOBobService
    {
        private readonly SLConnection _connection = connection
                ?? throw new ArgumentNullException(nameof(connection));
        private const string ServicePrefix = "SBOBobService";

        public Task<int?> GetSystemPermissionAsync(string userCode, string permissionID)
            => _connection
                .Request($"{ServicePrefix}_GetSystemPermission")
                .PostAsync<int?>(new { UserCode = userCode, PermissionID = permissionID });

        public Task<string> GetSystemCurrencyAsync()
            => _connection
                .Request($"{ServicePrefix}_GetSystemCurrency")
                .PostAsync<string>();

        public Task<string> GetDueDateAsync(string cardCode, string refDate)
            => _connection
                .Request($"{ServicePrefix}  ")
                .PostAsync<string>(new { CardCode = cardCode, RefDate = refDate });

        public Task<string> GetLocalCurrencyAsync()
            => _connection
                .Request($"{ServicePrefix}_GetLocalCurrency")
                .PostAsync<string>();

        public async Task<double?> GetCurrencyRateAsync(string currency, DateTime date)
        {
            await _connection.LoginAsync();
            // Build the payload with the correct format
            var payload = new
            {
                Currency = currency,
                Date = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
            };

            // Send and parse exactly like before
            var cookieHeader = $"B1SESSION={_connection.LoginResponse.SessionId}; CompanyDB={_connection.CompanyDB}";
            string raw = await _connection.Client
                .Request($"{ServicePrefix}_GetCurrencyRate")
                .WithHeader("Cookie", cookieHeader)
                .PostJsonAsync(payload)
                .ReceiveString();

            return double.TryParse(raw,
                                   NumberStyles.Float,
                                   CultureInfo.InvariantCulture,
                                   out var rate) ? rate : (double?)null;
        }


        public Task<double?> GetIndexRateAsync(string index, string date)
            => _connection
                .Request($"{ServicePrefix}_GetIndexRate")
                .PostAsync<double?>(new { Index = index, Date = date });

        public Task<string> FormatMoneyToStringAsync(string inMoney, string inPrecision)
            => _connection
                .Request($"{ServicePrefix}_Format_MoneyToString")
                .PostAsync<string>(new { InMoney = inMoney, InPrecision = inPrecision });

        public Task SetCurrencyRateAsync(string rateDate, string currency, string rate)
            => _connection
                .Request($"{ServicePrefix}_SetCurrencyRate")
                .PostAsync(new { RateDate = rateDate, Currency = currency, Rate = rate });

        public Task SetSystemPermissionAsync(
            string userCode,
            string permissionID,
            int permission)
            => _connection
                .Request($"{ServicePrefix}_SetSystemPermission")
                .PostAsync(new
                {
                    UserCode = userCode,
                    PermissionID = permissionID,
                    Permission = permission
                });
    }
}
