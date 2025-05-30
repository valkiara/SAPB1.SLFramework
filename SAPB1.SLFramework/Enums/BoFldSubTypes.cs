using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SAPB1.SLFramework.Enums
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BoFldSubTypes
    {
        st_None = 0,
        st_Address = 63,
        st_Phone = 35,
        st_Time = 84,
        st_Rate = 82,
        st_Sum = 83,
        st_Price = 80,
        st_Quantity = 81,
        st_Percentage = 37,
        st_Measurement = 77,
        st_Link = 66,
        st_Image = 73
    }
}
