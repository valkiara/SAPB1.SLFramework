namespace SAPB1.SLFramework.Abstractions.Interfaces
{
    public interface ISBOBobService
    {
        Task<string> FormatMoneyToStringAsync(string inMoney, string inPrecision);
        Task<double?> GetCurrencyRateAsync(string currency, DateTime date);
        Task<string> GetDueDateAsync(string cardCode, string refDate);
        Task<double?> GetIndexRateAsync(string index, string date);
        Task<string> GetLocalCurrencyAsync();
        Task<string> GetSystemCurrencyAsync();
        Task<int?> GetSystemPermissionAsync(string userCode, string permissionID);
        Task SetCurrencyRateAsync(string rateDate, string currency, string rate);
        Task SetSystemPermissionAsync(string userCode, string permissionID, int permission);
    }
}
