using System.Text.Json.Serialization;
using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public abstract class Payments
    {
        // ——— Keys / identity ———
        public int? DocEntry { get; set; }
        public int? DocNum { get; set; }

        public BoPaymentsObjectType? DocObjectCode { get; set; }
        public BoRcptTypes? DocType { get; set; }
        /// <summary>Legacy alias sometimes used in payloads.</summary>
        public BoRcptTypes? DocTypte { get; set; }

        // ——— Business partner & addressing ———
        /// <summary>BP code or account code. (CardCode)</summary>
        public string? CardCode { get; set; }
        /// <summary>Full BP name. (CardFName)</summary>
        public string? CardName { get; set; }
        /// <summary>Bill-to address (max 254). (Address)</summary>
        public string? Address { get; set; }
        public int? ContactPersonCode { get; set; }
        /// <summary>Destination code for outgoing payments. (PayToCode, 50)</summary>
        public string? PayToCode { get; set; }

        // ——— Dates & currency ———
        public DateTimeOffset? DocDate { get; set; } // posting date

        public DateTimeOffset? TaxDate { get; set; } // VAT/tax date

        public DateTimeOffset? DueDate { get; set; } // check due date

        public string? DocCurrency { get; set; } // 3 chars

        public double? DocRate { get; set; }

        public BoYesNoEnum? LocalCurrency { get; set; } // DiffCurr
        public BoYesNoEnum? ApplyVAT { get; set; }      // ApplyVAT

        // ——— General info / references ———
        public string? JournalRemarks { get; set; } // 50
        public string? Remarks { get; set; }        // 254
        public string? Reference1 { get; set; }     // 11
        public string? Reference2 { get; set; }     // 8
        public string? CounterReference { get; set; } // 8
        public string? TransactionCode { get; set; }  // TransCode (OTRC)
        public int? Series { get; set; }

        // ——— Cash & accounts ———
        public string? CashAccount { get; set; } // CashAcct (15)

        public double? CashSum { get; set; } // mandatory

        public double? CashSumFC { get; set; }

        public double? CashSumSys { get; set; } // CheckSumSy (per docs)

        public string? CheckAccount { get; set; } // 15
        public string? ControlAccount { get; set; } // BpAct (OACT)

        // ——— Bank transfer ———
        public BoYesNoEnum? IsPayToBank { get; set; } // IsPaytoBnk
        public string? BankCode { get; set; }        // 30
        public string? BankAccount { get; set; }     // 50
        public string? TransferAccount { get; set; } // TrsfrAcct (15)

        public DateTimeOffset? TransferDate { get; set; }

        public string? TransferReference { get; set; } // 27

        public double? TransferSum { get; set; } // mandatory

        public double? TransferRealAmount { get; set; } // RU loc (TfrRealAmt)

        // ——— Bank charges ———
        public double? BankChargeAmount { get; set; }   // BcgSum

        public double? BankChargeAmountInFC { get; set; } // BcgSumFC

        public double? BankChargeAmountInSC { get; set; } // BcgSumSy

        // ——— Pay-to bank (outgoing) ———
        public string? PayToBankCode { get; set; }     // PBnkCode (30)
        public string? PayToBankBranch { get; set; }   // PBnkBranch (50)
        public string? PayToBankAccountNo { get; set; } // PBnkAccnt (50)
        public string? PayToBankCountry { get; set; }  // PBnkCnt (3)

        // ——— Bill of Exchange ———
        public string? BoeAccount { get; set; }       // BoeAcc (15)
        public string? BillOfExchangeAgent { get; set; } // BoeAgent (OAGP)

        public double? BillOfExchangeAmount { get; set; } // BoeSum (LC)

        public double? BillOfExchangeAmountFC { get; set; } // BoeSumFc

        
        public double? BillOfExchangeAmountSC { get; set; } // BoeSumSc

        /// <summary>Status of BoE (map to your enum if available).</summary>
        public string? BillofExchangeStatus { get; set; } // BoBoeStatus

        // ——— Withholding tax ———
        public string? WTAccount { get; set; } // WtAccount (15)

        
        public double? WTAmount { get; set; }   // WtSum (LC)

        
        public double? WTAmountFC { get; set; } // WtSumFrgn

        
        public double? WTAmountSC { get; set; } // WtSumSys

        
        public double? WtBaseSum { get; set; }   // WtBaseSum

        
        public double? WtBaseSumFC { get; set; } // WtBaseSumF

        
        public double? WtBaseSumSC { get; set; } // WtSumSys (per list)

        public string? WTCode { get; set; } // 4
        
        public double? WTTaxableAmount { get; set; } // WtBaseAmnt

        // ——— VAT ———
        public string? TaxGroup { get; set; } // VatGroup (8)

        // ——— Misc flags / meta ———
        public BoYesNoEnum? HandWritten { get; set; }         // Handwrtten
        public BoYesNoEnum? Cancelled { get; set; }           // Cancelled
        public string? CancelStatus { get; set; }             // CancelStatus
        public BoYesNoEnum? Printed { get; set; }             // Printed
        public BoYesNoEnum? Proforma { get; set; }            // Proforma
        public BoYesNoEnum? DigitalPayments { get; set; }     // DigPayment
        public BoYesNoEnum? SplitVendorCreditRow { get; set; } // credit card installments

        // ——— Payment prioritization ———
        /// <summary>Payment priority (map to enum if you have BoPaymentPriorities).</summary>
        public BoPaymentPriorities? PaymentPriority { get; set; } // PaPriority

        public string? PaymentReferenceNo { get; set; } // PaymentRef (27)
        public string? PaymentType { get; set; }        // ObjType

        // ——— Localization / attachments / auth ———
        public string? AllocationNumberIL { get; set; } // AllocNum (IL)
        public int? AttachmentEntry { get; set; }
        public string? AuthorizationStatus { get; set; } // wddStatus

        // ——— Project / location / BP VAT ———
        public string? ProjectCode { get; set; }   // PrjCode (8)
        public string? LocationCode { get; set; }  // LocCode (cluster B)
        public string? VATRegNum { get; set; }

        // ——— Optional electronic fields ———
        public string? EDocExportFormat { get; set; }
        public string? ElecCommMessage { get; set; }
        public string? ElecCommStatus { get; set; }
        public string? SignatureDigest { get; set; }
        public string? SignatureInputMessage { get; set; }
        public string? PrimaryFormItems { get; set; }
        public string? PrivateKeyVersion { get; set; }
        public string? PaymentByWTCertif { get; set; }
        public string? CertificationNumber { get; set; }
        public string? Cup { get; set; }
        public string? Cig { get; set; }

        // ——— Under/Over payment ———
        
        public double? UnderOverpaymentdifference { get; set; } // UndOvDiff

        
        public double? UnderOverpaymentdiffFC { get; set; }

        
        public double? UnderOverpaymentdiffSC { get; set; } // UndOvDiffS

        // ——— Lines / children ———
        public IList<PaymentAccount>? PaymentAccounts { get; set; }
        public IList<PaymentInvoice>? PaymentInvoices { get; set; } 
        public IList<PaymentCheck>? Checks { get; set; } 
        public IList<PaymentCreditCard>? CreditCards { get; set; }
        public IList<CashFlowAssignment>? CashFlowAssignments { get; set; }
        
    }

    // ————————————————————————
    // Child line models
    // ————————————————————————

    public class PaymentAccount
    {
        public string? AccountCode { get; set; }
        
        public double? SumPaid { get; set; }
    }

    public class PaymentInvoice
    {
        public int DocEntry { get; set; }
        public BoRcptInvTypes? InvoiceType { get; set; }
        
        public double? PaidSum { get; set; }
        
        public double? SumApplied { get; set; }
        
        public double? AppliedFC { get; set; }
        public int? LineNum { get; set; }
    }

    public class PaymentCheck
    {
        public int? CheckNumber { get; set; }
        public string? BankCode { get; set; }
        public string? Accountt { get; set; }
        
        public double? CheckSum { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public string? CountryCode { get; set; }
        public string? Branch { get; set; }
        public string? Trnsfrable { get; set; }
    }

    public class PaymentCreditCard
    {
        public int? CreditCard { get; set; }     // Credit card code (OCRC)
        public string? CreditAcct { get; set; }  // G/L account for card
        public double? CreditSum { get; set; }
        public string? VoucherNum { get; set; }
        public DateTimeOffset? CardValidUntil { get; set; }
        public string? OwnerIdNum { get; set; }
        public string? OwnerPhone { get; set; }
    }

    public class CashFlowAssignment
    {
        public PaymentMeansTypeEnum? PaymentMeans { get; set; }
        public double? AmountLC { get; set; }
        public double? AmountFC { get; set; }
    }
}
