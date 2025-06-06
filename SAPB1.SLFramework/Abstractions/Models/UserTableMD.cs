using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class UserTablesMD
    {
        public required string TableName { get; set; }
        public required string TableDescription { get; set; }
        public Enums.BoUTBTableType TableType { get; set; }
        public Enums.BoYesNoEnum Archivable { get; set; }
        public string? ArchiveDateField { get; set; }

        /// <summary>
        /// Returns <see cref="TableName"/> without a leading '@', if present.
        /// </summary>
        [JsonIgnore]      // or [JsonIgnore] if you’re using System.Text.Json
        public string CleanTableName
            => TableName.StartsWith("@")
                 ? TableName[1..]   // drop the first character
                 : TableName;
    }
}
