using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    // ---------- Model ----------
    [SapTable("OITM")]
    public class Items
    {
        // Mandatory core
        public required string ItemCode { get; set; }           // len 20
        public required string ItemName { get; set; }           // len 200

        // AR/AP tax codes
        public string? ArTaxCode { get; set; }                  // TaxCodeAR len 8
        public string? ApTaxCode { get; set; }                  // TaxCodeAP len 8

        // Asset-related
        public BoYesNoEnum? AssetItem { get; set; }
        public string? AssetClass { get; set; }                 // len 20
        public string? AssetGroup { get; set; }                 // len 15
        public string? AssetSerialNumber { get; set; }          // AssetSerNo len 30
        public AssetStatusEnum? AssetStatus { get; set; }
        public string? InventoryNumber { get; set; }            // InventoryNo len 12
        public string? Location { get; set; }
        public BoYesNoEnum? ManageByQuantity { get; set; }            // MgrByQty
        public DateTime? CapitalizationDate { get; set; }       // CapDate
        public BoYesNoEnum? Cession { get; set; }
        public BoYesNoEnum? StatisticalAsset { get; set; }            // StatAsset
        public string? DepreciationGroup { get; set; }          // DeprGroup len 15

        // Attachments & attributes
        public int? AttachmentEntry { get; set; }               // AtcEntry

        // Prices / costs / quantities
        public decimal? AvgStdPrice { get; set; }               // AvgPrice
        public decimal? MovingAveragePrice { get; set; }
        public decimal? QuantityOnStock { get; set; }
        public decimal? QuantityOrderedByCustomers { get; set; }
        public decimal? QuantityOrderedFromVendors { get; set; }
        public decimal? DesiredInventory { get; set; }          // ReorderQty
        public decimal? MaxInventory { get; set; }
        public decimal? MinInventory { get; set; }

        // Item flags & classification
        public BoYesNoEnum? InventoryItem { get; set; }               // warehouse item?
        public BoYesNoEnum? SalesItem { get; set; }
        public BoYesNoEnum? PurchaseItem { get; set; }
        public ItemClassEnum? ItemClass { get; set; }               // ItemClass
        public int? ItemsGroupCode { get; set; }             // ItmsGrpCod (keep string/int as per your repo)
        public string? ItemType { get; set; }                   // ItemType (DI string flag often 'I','L','T')
        public string? ItemCountryOrg { get; set; }             // len 3
        public string? ForeignName { get; set; }                // FrgnName len 200
        public string? CommodityClassification { get; set; }    // CommClass
        public int? MaterialGroup { get; set; }              // MatGrp (FK to OMGP)
        public BoMaterialTypes? MaterialType { get; set; }         // MatType
        public int? Manufacturer { get; set; }               // FK code
        public int? ServiceGroup { get; set; }               // ServiceGrp (FK to OSGP)
        public int? NCMCode { get; set; }                    // Brazil ONCM

        // Barcodes & identifiers
        public string? BarCode { get; set; }                    // CodeBars len 16
        public string? SWW { get; set; }                        // len 16
        public string? SerialNum { get; set; }                  // len 17
        public string? SupplierCatalogNo { get; set; }          // len 17
        public string? Picture { get; set; }                    // PicturName len 200

        // UoM / Units
        public string? BaseUnitName { get; set; }               // BaseUnit len 20
        public string? InventoryUOM { get; set; }               // len 5
        public int? InventoryUoMEntry { get; set; }             // IUoMEntry
        public int? DefaultSalesUoMEntry { get; set; }          // SUoMEntry
        public int? DefaultPurchasingUoMEntry { get; set; }     // PUoMEntry
        public int? DefaultCountingUoMEntry { get; set; }       // INUoMEntry
        public string? DefaultCountingUnit { get; set; }        // CntUnitMsr len 100
        public int? UoMGroupEntry { get; set; }                 // UgpEntry
        public int? PricingUnit { get; set; }

        // Warehouse / logistics
        public string? DefaultWarehouse { get; set; }           // DfltWH len 8
        public BoYesNoEnum? ManageStockByWarehouse { get; set; }
        public IssuePrimarilyByEnum? IssuePrimarilyBy { get; set; }  // IssuePriBy

        // Inventory control
        public BoIssueMethod? IssueMethod { get; set; }
        public BoYesNoEnum? ManageSerialNumbers { get; set; }
        public BoYesNoEnum? ManageBatchNumbers { get; set; }
        public BoYesNoEnum? ManageSerialNumbersOnReleaseOnly { get; set; } // ManOutOnly
        public BoYesNoEnum? ForceSelectionOfSerialNumber { get; set; }     // BlockOut
        public BoYesNoEnum? IsPhantom { get; set; }

        // Planning / procurement
        public BoPlanningSystem? PlanningSystem { get; set; }
        public BoProcurementMethod? ProcurementMethod { get; set; }
        public int? LeadTime { get; set; }                      // days

        // GL / Tax / Accounting
        public BoGLMethods? GLMethod { get; set; }
        public TypeOfAdvancedRulesEnum? TypeOfAdvancedRules { get; set; }   // GLPickMeth
        public BoTaxTypes? TaxType { get; set; }
        public BoYesNoEnum? VatLiable { get; set; }
        public BoYesNoEnum? WTLiable { get; set; }
        public BoYesNoEnum? IndirectTax { get; set; }

        public string? ECExpensesAccount { get; set; }          // len 15
        public string? ECRevenuesAccount { get; set; }          // len 15
        public string? ForeignExpensesAccount { get; set; }     // len 15
        public string? ForeignRevenuesAccount { get; set; }     // len 15

        public string? PurchaseVATGroup { get; set; }           // len 8
        public string? SalesVATGroup { get; set; }              // len 8

        // Validity / status / freeze
        public BoYesNoEnum? Valid { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ValidRemarks { get; set; }               // len 30

        public BoYesNoEnum? Frozen { get; set; }
        public DateTime? FrozenFrom { get; set; }
        public DateTime? FrozenTo { get; set; }
        public string? FrozenRemarks { get; set; }              // len 30

        // Dates & audit
        public DateTimeOffset? CreateDate { get; set; }
        public TimeOnly? CreateTime { get; set; }                    // hhmmss as int (CreateTS)
        public DateTimeOffset? UpdateDate { get; set; }
        public TimeOnly? UpdateTime { get; set; }                    // hhmmss as int (UpdateTS)

        // Texts / misc
        public string? LegalText { get; set; }                  // len 250
        public string? User_Text { get; set; }                  // len 10
        public string? Mainsupplier { get; set; }               // len 15
        public string? ForeignName2 { get; set; }               // alias if you keep FrgnName elsewhere

        // Service codes (FKs exist in OSCD; DI often not exposing master)
        public int? IncomingServiceCode { get; set; }        // ISvcCode
        public int? OutgoingServiceCode { get; set; }        // OSvcCode

        // Customs / export
        public int? CustomsGroupCode { get; set; }              // CstGrpCode
        public int? DataExportCode { get; set; }             // ExportCode len 20
        public int? DNFEntry { get; set; }                      // foreign key

        // Properties / groups / commissions
        public int? CommissionGroup { get; set; }               // CommisGrp
        public decimal? CommissionPercent { get; set; }         // CommisPcnt
        public decimal? CommissionSum { get; set; }             // CommisSum

        // Counting / packaging / per-unit
        public decimal? CountingItemsPerUnit { get; set; }      // NumInCnt
        public string? SalesUnit { get; set; }                  // len 5
        public string? PurchaseUnit { get; set; }               // len 5
        public decimal? SalesItemsPerUnit { get; set; }
        public decimal? PurchaseItemsPerUnit { get; set; }
        public string? SalesPackagingUnit { get; set; }         // len 8
        public string? PurchasePackagingUnit { get; set; }      // len 8
        public decimal? SalesQtyPerPackUnit { get; set; }
        public decimal? PurchaseQtyPerPackUnit { get; set; }

        // Factors & ordering
        public decimal? SalesFactor1 { get; set; }
        public decimal? SalesFactor2 { get; set; }
        public decimal? SalesFactor3 { get; set; }
        public decimal? SalesFactor4 { get; set; }
        public decimal? PurchaseFactor1 { get; set; }
        public decimal? PurchaseFactor2 { get; set; }
        public decimal? PurchaseFactor3 { get; set; }
        public decimal? PurchaseFactor4 { get; set; }
        public int? OrderIntervals { get; set; }                // OCYC ref (opaque)
        public decimal? OrderMultiple { get; set; }
        public decimal? MinOrderQuantity { get; set; }          // MinOrdrQty

        // Weights / volumes (inventory & units)
        public decimal? InventoryWeight { get; set; }
        public decimal? InventoryWeight1 { get; set; }
        public string? InventoryWeightUnit { get; set; }
        public string? InventoryWeightUnit1 { get; set; }

        public decimal? SalesUnitWeight { get; set; }
        public decimal? SalesUnitWeight1 { get; set; }
        public string? SalesWeightUnit { get; set; }            // len 6
        public string? SalesWeightUnit1 { get; set; }

        public decimal? SalesUnitVolume { get; set; }
        public int? SalesVolumeUnit { get; set; }

        public decimal? SalesUnitHeight { get; set; }
        public decimal? SalesUnitHeight1 { get; set; }
        public string? SalesHeightUnit { get; set; }
        public string? SalesHeightUnit1 { get; set; }

        public decimal? SalesUnitLength { get; set; }
        public decimal? SalesUnitLength1 { get; set; }
        public string? SalesLengthUnit { get; set; }
        public string? SalesLengthUnit1 { get; set; }

        public decimal? SalesUnitWidth { get; set; }
        public decimal? SalesUnitWidth1 { get; set; }
        public string? SalesWidthUnit { get; set; }             // len 6
        public string? SalesWidthUnit1 { get; set; }

        public decimal? PurchaseUnitWeight { get; set; }
        public decimal? PurchaseUnitWeight1 { get; set; }
        public string? PurchaseWeightUnit { get; set; }
        public string? PurchaseWeightUnit1 { get; set; }

        public decimal? PurchaseUnitVolume { get; set; }
        public int? PurchaseVolumeUnit { get; set; }

        public decimal? PurchaseUnitHeight { get; set; }
        public decimal? PurchaseUnitHeight1 { get; set; }
        public string? PurchaseHeightUnit { get; set; }         // len 6
        public string? PurchaseHeightUnit1 { get; set; }

        public decimal? PurchaseUnitLength { get; set; }
        public decimal? PurchaseUnitLength1 { get; set; }
        public string? PurchaseLengthUnit { get; set; }
        public string? PurchaseLengthUnit1 { get; set; }

        public decimal? PurchaseUnitWidth { get; set; }
        public decimal? PurchaseUnitWidth1 { get; set; }
        public string? PurchaseWidthUnit { get; set; }
        public string? PurchaseWidthUnit1 { get; set; }

        // Tree/BOM & traceability
        public BoItemTreeTypes? TreeType { get; set; }
        public BoYesNoEnum? TraceableItem { get; set; }


        // Misc flags often seen on localizations (kept as bool? to avoid overspec)
        public BoYesNoEnum? GSTRelevnt { get; set; }
        public string? GSTTaxCategory { get; set; }
        public string? GTSItemSpec { get; set; }
        public string? GTSItemTaxCategory { get; set; }
        public BoYesNoEnum? ImportedItem { get; set; }

        // Validity / user / QR
        public BoYesNoEnum? CreateQRCodeFromProvidedSource { get; set; } // QRCodeSrc as presence flag
        public string? CreateQRCodeFrom { get; set; }              // QRCodeSrc raw value

        // Misc keys / codes (opaque to DI)
        public int? SACEntry { get; set; }
        public string? SAFTProductType { get; set; }            // ProdctType
        public string? SAFTProductTypeEx { get; set; }          // ProdTypeEx len 10
        public int? NVECode { get; set; }                    // len 6
        public int? ChapterID { get; set; }
        public int? CESTCode { get; set; }
        public int? Series { get; set; }

        // Technician / employee
        public int? Technician { get; set; }                    // Employee id

        // Picking/serial-batch combined method (left generic)
        public string? SRIAndBatchManageMethod { get; set; }

        // Withholding / VAT flags already covered (WTLiable/VatLiable)

        // Warehouses child-like info placeholder (use your own child object as needed)
        public List<ItemWarehouseInfo>? ItemWarehouseInfoCollection { get; set; }
    }

    // Minimal placeholder for warehouse info; replace with your actual child model
    public class ItemWarehouseInfo
    {
        // Mandatory core
        public required string WarehouseCode { get; set; }   // WhsCode len 8
        public string? ItemCode { get; set; }

        // Quantities
        public decimal? InStock { get; set; }
        public decimal? Committed { get; set; }
        public decimal? Ordered { get; set; }
        public decimal? CountedQuantity { get; set; }

        // Min/Max
        public decimal? MinimalStock { get; set; }
        public decimal? MaximalStock { get; set; }
        public decimal? MinimalOrder { get; set; }

        // Average cost
        public decimal? StandardAveragePrice { get; set; }   // AvgPrice

        // Bin locations
        public int? DefaultBin { get; set; }                 // DftBinAbs
        public BoYesNoEnum? DefaultBinEnforced { get; set; } // DftBinEnfd
        public BoYesNoEnum? WasCounted { get; set; }

        // Manufacturer
        public string? CNJPOfManufacturer { get; set; }      // CNJPMan len 14

        // Accounts (all length 15 in SAP)
        public string? CostAccount { get; set; }
        public string? CostInflationAccount { get; set; }            // DecresGlAc
        public string? CostInflationOffsetAccount { get; set; }      // CostRvlAct
        public string? DecreasingAccount { get; set; }
        public string? EUExpensesAccount { get; set; }
        public string? EUPurchaseCreditAcc { get; set; }             // APCMEUAct
        public string? EURevenuesAccount { get; set; }
        public string? ExchangeRateDifferencesAcct { get; set; }     // ExchangeAc
        public string? ExemptedCredits { get; set; }                 // ARCMExpAct
        public string? ExemptIncomeAcc { get; set; }
        public string? ExpenseClearingAct { get; set; }              // ExpClrAct
        public string? ExpenseOffsettingAccount { get; set; }        // ExpOfstAct
        public string? ExpensesAccount { get; set; }
        public string? ForeignExpensAcc { get; set; }
        public string? ForeignPurchaseCreditAcc { get; set; }        // APCMFrnAct
        public string? ForeignRevenueAcc { get; set; }
        public string? GLDecreaseAcct { get; set; }                  // DecresGlAc
        public string? GLIncreaseAcct { get; set; }                  // PAReturnAc
        public string? GoodsClearingAcct { get; set; }               // BalanceAcc
        public string? IncreasingAccount { get; set; }
        public string? InventoryAccount { get; set; }
        public string? InventoryOffsetProfitAndLossAccount { get; set; } // StockOffst
        public string? NegativeInventoryAdjustmentAccount { get; set; }  // NegStckAct
        public string? PAReturnAcct { get; set; }
        public string? PriceDifferenceAcc { get; set; }
        public string? PurchaseAcct { get; set; }                    // PurchaseAc
        public string? PurchaseBalanceAccount { get; set; }
        public string? PurchaseCreditAcc { get; set; }               // APCMAct
        public string? PurchaseOffsetAcct { get; set; }              // PurchOfsAc
        public string? ReturningAccount { get; set; }
        public string? RevenuesAccount { get; set; }
        public string? SalesCreditAcc { get; set; }                  // ARCMAct
        public string? SalesCreditEUAcc { get; set; }                // ARCMEUAct
        public string? SalesCreditForeignAcc { get; set; }           // ARCMFrnAct
        public string? ShippedGoodsAccount { get; set; }             // ShpdGdsAct
        public string? StockInflationAdjustAccount { get; set; }     // StokRvlAct
        public string? StockInflationOffsetAccount { get; set; }     // StkOffsAct
        public string? StockInTransitAccount { get; set; }           // StkInTnAct
        public string? TransferAccount { get; set; }
        public string? VarienceAccount { get; set; }
        public string? VATInRevenueAccount { get; set; }             // VatRevAct
        public string? WHIncomingCenvatAccount { get; set; }         // WhICenAct
        public string? WHOutgoingCenvatAccount { get; set; }         // WhOCenAct
        public string? WipAccount { get; set; }                      // WipAcct
        public string? WipOffsetProfitAndLossAccount { get; set; }   // WipOffset
        public string? WipVarianceAccount { get; set; }              // WipVarAcct

        // Flags
        public BoYesNoEnum? Locked { get; set; }     // Locked for entry/exit
        public string? IndicatorForRelevantScale { get; set; } // IndEscala

        // --- Optional system properties ---
        public Double? Count { get; set; }       // Total rows in object
        public Double? Counted { get; set; }     // Current count
    }
}
