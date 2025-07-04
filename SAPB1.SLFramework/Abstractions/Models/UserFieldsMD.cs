﻿namespace SAPB1.SLFramework.Abstractions.Models
{
    public class UserFieldsMD
    {
        public required string Name { get; set; }
        public Enums.BoFieldTypes? Type { get; set; }
        public int? Size { get; set; }
        public required string Description { get; set; }
        public Enums.BoFldSubTypes? SubType { get; set; }
        public string? LinkedTable { get; set; }
        public string? DefaultValue { get; set; }
        public required string TableName { get; set; }
        public int FieldID { get; set; }
        public int? EditSize { get; set; }
        public Enums.BoYesNoEnum? Mandatory { get; set; }
        public string? LinkedUDO { get; set; }
        public Enums.UDFLinkedSystemObjectTypesEnum? LinkedSystemObject { get; set; }
        public ICollection<ValidValueMD>? ValidValuesMD { get; set; }
    }
}
