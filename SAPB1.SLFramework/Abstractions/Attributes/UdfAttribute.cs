using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UdfAttribute : Attribute
    {
        /// <summary>
        /// AliasID — e.g. "U_MyField"
        /// </summary>
        public string FieldID { get; }

        /// <summary>
        /// User-friendly name — e.g. "My Field"
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// db_Alpha, db_Numeric, etc.
        /// </summary>
        public BoFieldTypes Type { get; }

        /// <summary>
        /// Max length for alpha fields (or precision for numeric)
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Description visible in SAP UI
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// st_Alpha, st_Sum, etc.
        /// </summary>
        public BoFldSubTypes SubType { get; set; } = BoFldSubTypes.st_None;

        /// <summary>
        /// Whether this field is mandatory
        /// </summary>
        public BoYesNoEnum Mandatory { get; set; } = BoYesNoEnum.tNO;

        /// <summary>
        /// Linked user table alias (if any)
        /// </summary>
        public string LinkedTable { get; set; } = string.Empty;

        /// <summary>
        /// Default value, where applicable
        /// </summary>
        public string DefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// Edit size in the UI
        /// </summary>
        public int EditSize { get; set; } = 0;

        /// <summary>
        /// If this UDF links to a UDO, its alias
        /// </summary>
        public string LinkedUDO { get; set; } = string.Empty;

        /// <summary>
        /// Linked system object type (e.g. SalesOrder, BusinessPartner, etc.)
        /// </summary>
        public UDFLinkedSystemObjectTypesEnum LinkedSystemObject { get; set; }
            = UDFLinkedSystemObjectTypesEnum.ulNone;

        public UdfAttribute(
            string fieldID,
            string name,
            BoFieldTypes type,
            int size,
            string description)
        {
            FieldID = fieldID;
            Name = name;
            Type = type;
            Size = size;
            Description = description;
        }
    }
}
