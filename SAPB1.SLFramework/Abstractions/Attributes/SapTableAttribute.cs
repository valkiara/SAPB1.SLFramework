namespace SAPB1.SLFramework.Abstractions.Attributes
{
    /// <summary>
    /// Marks a CLR class as representing a system table in SAP B1.
    /// The scanner will use this TableName when creating UDFs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SapTableAttribute : Attribute
    {
        /// <summary>
        /// The exact SAP table name—e.g. "OITM" for Items, "OCRD" for Business Partners.
        /// </summary>
        public string TableName { get; }

        public SapTableAttribute(string tableName) => TableName = tableName;
    }
}