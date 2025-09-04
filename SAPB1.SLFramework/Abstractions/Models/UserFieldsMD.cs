using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class UserFieldsMD
    {
        /// <summary>
        /// Sets or returns the field name. 
        /// Field name: AliasID.
        /// Length: 50 characters.
        /// </summary>
        public required string Name { get; set; }


        /// <summary>
        /// Sets or returns the data type, which describes the nature of the data, of the specified field . 
        /// Field name: TypeID.
        /// </summary>
        public Enums.BoFieldTypes? Type { get; set; }


        /// <summary>
        /// The actual size of the field. 
        /// The value is automatically determined by the input of the         EditSize property. 
        /// Do not set any value in the Size property.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Sets or returns the description of the field. 
        /// Field name: Descr.
        /// Length: 80 characters.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Returns or set the field sub-type, which specifies a specific format of the data type. 
        /// </summary>
        public Enums.BoFldSubTypes? SubType { get; set; }

        /// <summary>
        /// Sets or returns a linked user table name, so that the user field will be used as a foreign key in the TableName. 
        /// Field name: RTable.
        /// Length: 20 characters.
        /// </summary>
        public string? LinkedTable { get; set; }

        /// <summary>
        /// Sets or returns the default value of the field. 
        /// Field name: Dflt.
        /// Length: 254 characters.
        /// </summary>
        public string? DefaultValue { get; set; }

        /// <summary>
        /// Sets or returns the name of the parent table that this field refers to. 
        /// Length: 21 characters.
        /// Field name: TableID.
        /// </summary>
        public required string TableName { get; set; }


        /// <summary>
        /// Returns the unique identification key of the field in the meta data table. 
        /// Field name: FieldID.
        /// </summary>
        public int FieldID { get; set; }

        /// <summary>
        /// Sets or returns the field maximum value entered by the user. 
        /// This applies only when the Type property is set to db_Alpha or db_Numeric. 
        /// Field name: EditSize.
        /// </summary>
        public int? EditSize { get; set; }


        /// <summary>
        /// Sets or returns a valid value that determines wether or not this User Field is mandatory in SAP Business One. 
        /// Field name: Sys.
        /// </summary>
        public Enums.BoYesNoEnum? Mandatory { get; set; }


        /// <summary>
        /// Links to a user-defined object (UDO) form of both Matrix style and Header Lines style. 
        /// Field name: RelUDO.
        /// Length: 20 characters.
        /// </summary>
        public string? LinkedUDO { get; set; }

        /// <summary>
        /// Links to an existing system object of SAP Business One.
        /// </summary>
        public Enums.UDFLinkedSystemObjectTypesEnum? LinkedSystemObject { get; set; }

        /// <summary>
        /// Returns the ValidValuesMD object. 
        /// </summary>
        public ICollection<ValidValueMD>? ValidValuesMD { get; set; }


        private static bool IsLinkedSystemObjectEquivalent(
            UDFLinkedSystemObjectTypesEnum? a,
            UDFLinkedSystemObjectTypesEnum? b)
        {
            // Treat null as None
            var na = a ?? UDFLinkedSystemObjectTypesEnum.ulNone;
            var nb = b ?? UDFLinkedSystemObjectTypesEnum.ulNone;
            return na == nb;
        }

        public bool IsDifferentFrom(UserFieldsMD other)
        {
            if (other == null) return true;

            bool shouldCompareEditSize = this.Type is Enums.BoFieldTypes.db_Alpha or Enums.BoFieldTypes.db_Numeric;

            return
                this.Type != other.Type ||
                !StringsEqual(this.Description, other.Description) ||
                !IsSubTypeEquivalent(this.SubType, other.SubType) ||
                !StringsEqual(this.LinkedTable, other.LinkedTable) ||
                !StringsEqual(this.DefaultValue, other.DefaultValue) ||
                (shouldCompareEditSize && this.EditSize != other.EditSize) ||
                this.Mandatory != other.Mandatory ||
                !StringsEqual(this.LinkedUDO, other.LinkedUDO) ||
                !IsLinkedSystemObjectEquivalent(this.LinkedSystemObject, other.LinkedSystemObject) ||  // <-- changed
                !AreValidValuesEqual(this.ValidValuesMD, other.ValidValuesMD);
        }


        private static bool StringsEqual(string? a, string? b)
        {
            return string.IsNullOrWhiteSpace(a) && string.IsNullOrWhiteSpace(b)
                || string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsSubTypeEquivalent(Enums.BoFldSubTypes? a, Enums.BoFldSubTypes? b)
        {
            bool isNoneOrNull(Enums.BoFldSubTypes? val) =>
                val == null || val == Enums.BoFldSubTypes.st_None;

            return isNoneOrNull(a) && isNoneOrNull(b) || a == b;
        }

        private static bool AreValidValuesEqual(ICollection<ValidValueMD>? a, ICollection<ValidValueMD>? b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Count != b.Count) return false;

            var orderedA = a.OrderBy(x => x.Value).ToList();
            var orderedB = b.OrderBy(x => x.Value).ToList();

            for (int i = 0; i < orderedA.Count; i++)
            {
                var va = orderedA[i];
                var vb = orderedB[i];

                if (!StringsEqual(va.Value, vb.Value) || !StringsEqual(va.Description, vb.Description))
                    return false;
            }

            return true;
        }

    }
}
