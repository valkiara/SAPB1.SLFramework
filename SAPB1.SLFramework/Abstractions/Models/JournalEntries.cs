using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public partial class JournalEntries
    {
        public int? JdtNum { get; set; } // JDT_NUM
        public int? Number { get; set; } // Number
        public DateTimeOffset? ReferenceDate { get; set; } // RefDate
        public string? Memo { get; set; } // Memo
        public string? Reference { get; set; } // Ref1
        public string? Reference2 { get; set; } // Ref2

        // From full list
        public BoYesNoEnum? AdjustTransaction { get; set; }
        public string? AllocationNumberIL { get; set; } // AllocNum
        public int? AttachmentEntry { get; set; }
        public BoYesNoEnum? AutomaticWT { get; set; } // AutoWT
        public BoYesNoEnum? AutoVAT { get; set; } // AutoVAT
        public string? BaseReference { get; set; } // BaseRef
        public int? BlanketAgreementNumber { get; set; }
        public BoYesNoEnum? BlockDunningLetter { get; set; } // BlockDunn
        public string? CertificationNumber { get; set; }
        public string? Cig { get; set; }
        public BoYesNoEnum? Corisptivi { get; set; } // Corisptivi
        public string? Cup { get; set; }
        public BoYesNoEnum? DeferredTax { get; set; } // DeferredTax
        public string? DocumentType { get; set; }
        public DateTimeOffset? DueDate { get; set; } // DueDate
        public string? ECDPostingType { get; set; }
        public string? ExcludeFromTaxReportControlStatementVAT { get; set; }
        public string? ExposedTransNumber { get; set; }
        public string? FolioNumber { get; set; } // FolioNum
        public string? FolioNumberFrom { get; set; }
        public string? FolioNumberTo { get; set; }
        public string? FolioPrefixString { get; set; } // FolioPref
        public string? Indicator { get; set; } // Indicator
        public BoYesNoEnum? IsCostCenterTransfer { get; set; }
        public string? Letter { get; set; }
        public int? LocationCode { get; set; } // LocCode
        public string? OperationCode { get; set; }
        public int? Original { get; set; } // CreatedBy
        public TransactionTypeEnum? OriginalJournal { get; set; } // TransType
        public string? PointOfIssueCode { get; set; }
        public string? Printed { get; set; }
        public string? PrivateKeyVersion { get; set; }
        public string? ProjectCode { get; set; } // Project
        public string? Reference3 { get; set; } // Ref3
        public BoYesNoEnum? Report347 { get; set; }
        public BoYesNoEnum? ReportEU { get; set; }
        public string? ReportingSectionControlStatementVAT { get; set; }
        public string? ResidenceNumberType { get; set; }
        public string? SAFTTransactionType { get; set; } // SAFTType
        public string? SAFTTransactionTypeEx { get; set; } // SAFTTypeEx
        public string? SAPPassport { get; set; }
        public int? Series { get; set; }
        public string? SignatureDigest { get; set; }
        public string? SignatureInputMessage { get; set; }
        public BoYesNoEnum? StampTax { get; set; }
        public DateTimeOffset? StornoDate { get; set; }
        public DateTimeOffset? TaxDate { get; set; }
        public string? TransactionCode { get; set; } // TransCode
        public BoYesNoEnum? UseAutoStorno { get; set; } // AutoStorno
        public DateTimeOffset? VatDate { get; set; } // vatdate
        public double? WTSum { get; set; } // WTSum
        public double? WTSumFC { get; set; } // WTSumFC
        public double? WTSumSC { get; set; } // WTSumSC

        // Child collections
        public ICollection<JournalEntryLine>? JournalEntryLines { get; set; }

        public class JournalEntryLine
        {
            // Core / identifiers
            public int? Line_ID { get; set; }                         // Line_ID
            public string? AccountCode { get; set; }                  // Account (G/L)
            public string? ControlAccount { get; set; }               // Account (control)
            public string? ShortName { get; set; }                    // ShortName (15)
            public string? FederalTaxID { get; set; }                 // FederalTaxID

            // References
            public string? Reference1 { get; set; }                   // Ref1 (100)
            public string? Reference2 { get; set; }                   // Ref2 (100)
            public string? AdditionalReference { get; set; }          // Ref3Line (27)
            public DateTimeOffset? ReferenceDate1 { get; set; }       // RefDate (not used)
            public DateTimeOffset? ReferenceDate2 { get; set; }       // Ref2Date (not used)

            // Descriptions
            public string? LineMemo { get; set; }                     // LineMemo (50)
            public string? BlockReason { get; set; }                  // PayBlckRef (50)

            // Values (LC/FC/SYS)
            public double? Debit { get; set; }                        // Debit (mandatory in DI)
            public double? Credit { get; set; }                       // Credit (mandatory in DI)
            public double? FCDebit { get; set; }                      // FCDebit
            public double? FCCredit { get; set; }                     // FCCredit
            public double? DebitSys { get; set; }                     // SYSDeb
            public double? CreditSys { get; set; }                    // SYSCred

            // VAT / Tax
            public string? TaxCode { get; set; }                      // TaxCode (8)
            public string? TaxGroup { get; set; }                     // VatGroup (8)
            public string? TaxPostAccount { get; set; }               // TaxPostAcc
            public double? BaseSum { get; set; }                      // BaseSum (incl. VAT)
            public double? GrossValue { get; set; }                   // GrossValue
            public double? VatAmount { get; set; }                    // VatAmount
            public double? TotalTax { get; set; }                     // TotalVat
            public double? SystemBaseAmount { get; set; }             // SYSBaseSum
            public double? SystemVatAmount { get; set; }              // SYSVatSum
            public double? SystemEqualizationTaxAmount { get; set; }  // EquVatSum (system)
            public double? SystemTotalTax { get; set; }               // SYSEquSum
            public double? EqualizationTaxAmount { get; set; }        // EquVatSum
            public BoYesNoEnum? VatLine { get; set; }                 // VatLine (is tax line)
            public string? VATExemptionCause { get; set; }            // EBVatExCau
            public string? VatClassificationCategory { get; set; }    // VATClassificationCategory
            public string? VatClassificationType { get; set; }        // VATClassificationType
            public DateTimeOffset? VatDate { get; set; }              // vatdate
            public DateTimeOffset? TaxDate { get; set; }              // TaxDate

            // Currencies
            public string? FCCurrency { get; set; }                   // FCCurrency (3)

            // Offsetting / contra
            public string? ContraAccount { get; set; }                // ContraAct (15)

            // Dimensions / cost centers
            public string? ProjectCode { get; set; }                  // Project (8)
            public string? CostingCode { get; set; }                  // ProfitCode (dim1)
            public string? CostingCode2 { get; set; }                 // OcrCode2 (dim2)
            public string? CostingCode3 { get; set; }                 // OcrCode3 (dim3)
            public string? CostingCode4 { get; set; }                 // OcrCode4 (dim4)
            public string? CostingCode5 { get; set; }                 // OcrCode5 (dim5)
            public string? CostElementCode { get; set; }              // CostElementCode

            // Localization / branches / locations
            public int? BPLID { get; set; }                           // BPLID
            public string? BPLName { get; set; }                      // BPLName
            public int? LocationCode { get; set; }                    // LocCode
            public string? VATRegNum { get; set; }                    // VATRegNum

            // Payments / blocking
            public BoYesNoEnum? PaymentBlock { get; set; }            // PayBlckRef (indicator)
            public BoYesNoEnum? PaymentOrdered { get; set; }               // PaymentOrdered (opaque)
            public int? CheckAbs { get; set; }                        // CheckAbs (internal check no.)

            // Dates
            public DateTimeOffset? DueDate { get; set; }              // DueDate

            // Compliance / classifications
            public string? ExpensesClassificationCategory { get; set; } // ExpensesClassificationCategory
            public string? ExpensesClassificationType { get; set; }     // ExpensesClassificationType
            public string? IncomeClassificationCategory { get; set; }    // IncomeClassificationCategory
            public string? IncomeClassificationType { get; set; }        // IncomeClassificationType

            // Misc keys / codes
            public string? ReferenceDate1Note /*not used*/ => null;
            public string? ReferenceDate2Note /*not used*/ => null;
            public string? ExposedTransNumber { get; set; }           // ExposedTransNumber
            public string? Cup { get; set; }                          // Cup
            public string? Cig { get; set; }                          // Cig
            public string? LineAllocationNumber { get; set; }         // LineAllocationNumber

            // Source doc linkage (marketing document linkage, when present)
            public int? DocumentArray { get; set; }                   // DocumentArray (Src array)
            public int? DocumentLine { get; set; }                    // DocumentLine (Src line)

            // Withholding tax flags
            public BoYesNoEnum? WTLiable { get; set; }                // WTLiable
            public BoYesNoEnum? WTRow { get; set; }                   // WTLine

            public ICollection<CashFlowAssignment>? CashFlowAssignments { get; set; }

            public class CashFlowAssignment
            {
                /// <summary>Amount in foreign currency. (AmountFC)</summary>
                public double? AmountFC { get; set; }

                /// <summary>Amount in local currency. (AmountLC)</summary>
                public double? AmountLC { get; set; }

                /// <summary>Primary key of cash flow assignment. (CashFlowAssignmentsID)</summary>
                public int? CashFlowAssignmentsID { get; set; }

                /// <summary>Cash flow line item id (FK to cash flow line items master). (CashFlowLineItemID)</summary>
                public int? CashFlowLineItemID { get; set; }

                /// <summary>Check number associated with this assignment (if relevant). (CheckNumber)</summary>
                public string? CheckNumber { get; set; }

                /// <summary>Journal entry id (header). (JDTId)</summary>
                public int? JDTId { get; set; }

                /// <summary>Journal entry line id. (JDTLineId)</summary>
                public int? JDTLineId { get; set; }

                /// <summary>Payment means / method code (SL returns a code; keep as string unless you map to an enum). (PaymentMeans)</summary>
                public PaymentMeansTypeEnum? PaymentMeans { get; set; }
            }
        }
    }
}
