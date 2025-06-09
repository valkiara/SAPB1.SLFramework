using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class BusinessPartner
    {
        public required string CardCode { get; set; }
        public required string CardName { get; set; }
        public BoCardTypes CardType { get; set; }
        public required string FederalTaxID { get; set; }
        public string? Phone1 { get; set; }
        public string? IBAN { get; set; }
        public string? DefaultBankCode { get; set; }
    }
}
