using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Abstractions.Models;
using SAPB1.SLFramework.Enums;
using System.Text.Json.Serialization;

namespace SAPb1.SLFramework.Tests
{
    [ServiceLayerResourcePath("U_RSM_BDPM")]
    [Udt("RSM_BDPM", "Bitrix24 DPM Accounts", BoUTBTableType.bott_NoObjectAutoIncrement, Archivable = BoYesNoEnum.tNO)]
    public class RSM_BDPM : NoObject
    {
        [Udf("Currency", "Agreement Currency", BoFieldTypes.db_Alpha, EditSize = 3, Mandatory = BoYesNoEnum.tYES)]
        public required string U_Currency { get; set; }

        [Udf("ControlAcc", "Control Acocunt", BoFieldTypes.db_Alpha, EditSize = 15, Mandatory = BoYesNoEnum.tYES)]
        public required string U_ControlAcc { get; set; }

        [Udf("Status", "Status", BoFieldTypes.db_Numeric, EditSize = 1)]
        [ValidValue("1", "საცხოვრებელი")]
        [ValidValue("2", "იჯარით")]
        public AreaTypeEnum? U_Status { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AreaTypeEnum
    {
        Residential = 1,
        Leasing = 2,
    }
}
