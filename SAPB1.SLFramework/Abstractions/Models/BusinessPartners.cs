using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class BusinessPartners
    {
        public string? CardCode { get; set; }
        public required string CardName { get; set; }
        public BoCardTypes CardType { get; set; }
        public required string FederalTaxID { get; set; }
        public string? Phone1 { get; set; }
        public string? IBAN { get; set; }
        public string? DefaultBankCode { get; set; }

        public IList<BPBankAccount> BPBankAccounts { get; set; }

        public BusinessPartners()
        {
            BPBankAccounts = [];
        }
    }

    public class BPBankAccount
    {
        public int? LogInstance { get; set; }
        public string? UserNo4 { get; set; }
        public string? BPCode { get; set; }
        public string? County { get; set; }
        public string? State { get; set; }
        public string? UserNo2 { get; set; }
        public string? IBAN { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Block { get; set; }
        public string? Branch { get; set; }
        public string? Country { get; set; }
        public string? Street { get; set; }
        public string? ControlKey { get; set; }
        public string? UserNo3 { get; set; }
        public required string BankCode { get; set; }
        public required string AccountNo { get; set; }
        public string? UserNo1 { get; set; }
        public int? InternalKey { get; set; }
        public string? BuildingFloorRoom { get; set; }
        public string? BIK { get; set; }
        public string? AccountName { get; set; }
        public string? CorrespondentAccount { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? CustomerIdNumber { get; set; }
        public string? ISRBillerID { get; set; }
        public int? ISRType { get; set; }
        public string? BICSwiftCode { get; set; }
        public string? ABARoutingNumber { get; set; }
        public string? MandateID { get; set; }
        public DateTimeOffset? SignatureDate { get; set; }
        public DateTimeOffset? MandateExpDate { get; set; }
        public int? SEPASeqType { get; set; }
    }
}