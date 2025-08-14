using B1SLayer;
using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.ServiceLayer
{
    public class CashFlowLineItemsService(SLConnection connection) : ICashFlowLineItemsService
    {
        private readonly SLConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        private const string Resource = "CashFlowLineItemsService_GetCashFlowLineItemList";

        public async Task<IReadOnlyList<CashFlowLineItems>> ListAsync(CancellationToken cancellationToken = default)
        {
            // SL actions of this shape expect POST with empty JSON body (`{}`) and return:
            // { "@odata.context": "...", "value": [ { LineItemID, LineItemName }, ... ] }
            var payload = await _connection
                .Request(Resource)
                .PostAsync<List<CashFlowLineItems>>(new { });

            return payload ?? new List<CashFlowLineItems>();
        }
    }
}
