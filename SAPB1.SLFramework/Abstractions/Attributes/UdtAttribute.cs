using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Attributes
{
    /// <summary>
    /// Marks a class as a SAP B1 User-Defined Table (UDT) and provides metadata for its creation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class UdtAttribute : Attribute
    {
        /// <summary>
        /// The alias of the UDT (must start with '@', e.g. "@MY_TABLE").
        /// </summary>
        public string TableName { get; }

        /// <summary>
        /// User-visible description of the table.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Type of UDT (Master Data, Document, etc.).
        /// </summary>
        public BoUTBTableType TableType { get; }

        /// <summary>
        /// Whether rows in the table can be archived.
        /// </summary>
        public BoYesNoEnum Archivable { get; set; } = BoYesNoEnum.tNO;

        /// <summary>
        /// If archivable = YES, the field name to store the archive date.
        /// </summary>
        public string ArchiveDateField { get; set; } = string.Empty;

        /// <summary>
        /// Creates a new UdtAttribute for a SAP B1 User-Defined Table.
        /// </summary>
        /// <param name="tableName">Alias, e.g. "@MY_TABLE"</param>
        /// <param name="description">Description shown in SAP UI</param>
        /// <param name="tableType">Master Data, Document, or NoObject</param>
        public UdtAttribute(
            string tableName,
            string description,
            BoUTBTableType tableType)
        {
            TableName = tableName;
            Description = description;
            TableType = tableType;
        }
    }
}
