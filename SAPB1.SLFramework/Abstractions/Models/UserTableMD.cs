namespace SAPB1.SLFramework.Abstractions.Models
{
    public class UserTablesMD
    {
        public required string TableName { get; set; }
        public required string TableDescription { get; set; }
        public Enums.BoUTBTableType TableType { get; set; }
        public Enums.BoYesNoEnum Archivable { get; set; }
        public string? ArchiveDateField { get; set; }
    }
}
