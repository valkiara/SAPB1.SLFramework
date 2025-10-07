using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class UserTablesMD
    {
        /// <summary>
        /// Sets or returns the name for the user defined table. 
        /// Field name: TableName.
        /// Length: 19 characters.
        /// </summary>
        public required string TableName { get; set; }

        /// <summary>
        /// A string that describes the name and functionality of the table. 
        /// Field name: Descr.
        /// Length: 30 characters.
        /// </summary>
        public required string TableDescription { get; set; }

        /// <summary>
        /// Sets or returns a valid value of BoUTBTableType type that specifies the type of the user table.
        /// Field name: ObjectType.
        /// </summary>
        public BoUTBTableType TableType { get; set; }
        /// <summary>
        /// property Archivable 
        /// </summary>
        public BoYesNoEnum Archivable { get; set; }
        /// <summary>
        /// property ArchiveDateField 
        /// </summary>
        public string? ArchiveDateField { get; set; }

        public bool IsDifferentFrom(UserTablesMD other)
        {
            if (other == null) return true;

            return
                !StringsEqual(this.TableDescription, other.TableDescription) ||
                this.Archivable != other.Archivable ||
                (this.Archivable == BoYesNoEnum.tYES && !StringsEqual(this.ArchiveDateField, other.ArchiveDateField));
        }

        private static bool StringsEqual(string? a, string? b)
        {
            return string.IsNullOrWhiteSpace(a) && string.IsNullOrWhiteSpace(b)
                || string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }


        public bool IsValidForUpdate()
        {
            return Archivable != BoYesNoEnum.tYES || !string.IsNullOrWhiteSpace(ArchiveDateField);
        }

    }
}
