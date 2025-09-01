using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoIssueMethod
    {
        im_Backflush = 0,   // SAP Business One issues items automatically from the inventory.
        im_Manual = 1       // The user issues specified items manually from the inventory.
    }

    /*
     * Remarks:
     * When one of the properties ManageBatchNumbers or ManageSerialNumbers 
     * is set to 'Yes', you must set the IssueMethod property to 'im_Manual'.
     */
}
