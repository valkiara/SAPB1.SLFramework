using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoMaterialTypes
    {
        mt_GoodsForReseller = 0,       // Goods for reseller material type
        mt_FinishedGoods = 1,          // Finished goods material type
        mt_GoodsInProcess = 2,         // Goods in process material type
        mt_RawMaterial = 3,            // Raw material type
        mt_Package = 4,                // Package material type
        mt_SubProduct = 5,             // Subproduct material type
        mt_IntermediateMaterial = 6,   // Intermediate material type
        mt_ConsumerMaterial = 7,       // Consumer material type
        mt_FixedAsset = 8,             // Fixed asset material type
        mt_Service = 9,                // Service material type
        mt_OtherInput = 10,            // Other input material type
        mt_Other = 99                  // Other material type
    }
}
