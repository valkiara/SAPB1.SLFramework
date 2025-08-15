using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    /// <summary>
    /// Specifies the type of account in the Chart of Accounts (OACT.ActType).
    /// Matches DI API / Service Layer values.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoAccountTypes
    {
        /// <summary>Sets account as revenues account.</summary>
        at_Revenues = 0,

        /// <summary>Sets account as expenses account.</summary>
        at_Expenses = 1,

        /// <summary>Neither expenses nor revenues.</summary>
        at_Other = 2
    }
}
