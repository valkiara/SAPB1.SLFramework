using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    /// <summary>
    /// StockTransfers (marketing document: Inventory Transfer)
    /// NOTE: Many SAP B1 SDK fields are enum/object types in COM.
    /// This POCO uses conservative types (string/int/DateTime) so you can
    /// easily adapt them to your project's enums and child models.
    /// </summary>
    public partial class StockTransfers
    {
        /// <summary>Customer address (consignment). Field: Address (len 254)</summary>
        public string? Address { get; set; }

        /// <summary>AT Document Type (localization-specific)</summary>
        public string? ATDocumentType { get; set; }

        /// <summary>Attachment entry (OATC->AbsEntry)</summary>
        public int? AttachmentEntry { get; set; }

        /// <summary>Authorization code</summary>
        public string? AuthorizationCode { get; set; }

        /// <summary>Authorization status. Field: wddStatus (map to your enum if needed)</summary>
        public ApprovalStatusEnum? AuthorizationStatus { get; set; }

        /// <summary>Branch (BPL) Id</summary>
        public int? BPLID { get; set; }

        /// <summary>Branch (BPL) Name</summary>
        public string? BPLName { get; set; }

        /// <summary>Business Partner code (CardCode). Field: CardCode (len 15)</summary>
        public string? CardCode { get; set; }

        /// <summary>Business Partner name (CardName). Field: CardName (len 100)</summary>
        public string? CardName { get; set; }

        /// <summary>Remarks. Field: Comments (len 254)</summary>
        public string? Comments { get; set; }

        /// <summary>Contact person code. Field: CntctCode (FK to ContactEmployees)</summary>
        public int? ContactPerson { get; set; }

        /// <summary>QR payload source (implementation-specific)</summary>
        public string? CreateQRCodeFrom { get; set; }

        /// <summary>Creation date. Field: CreateDate</summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>Posting date. Field: DocDate</summary>
        public DateTime? DocDate { get; set; }

        /// <summary>Document entry (primary key). Field: DocEntry</summary>
        public int? DocEntry { get; set; }

        /// <summary>Document number. Field: DocNum</summary>
        public int? DocNum { get; set; }

        /// <summary>Object type (e.g., oStockTransfer). Field: ObjType</summary>
        public BoObjectTypes? DocObjectCode { get; set; }


        /// <summary>Document status (placeholder; consider enum)</summary>
        public BoStatus? DocumentStatus { get; set; }

        /// <summary>Due date. Field: DocDueDate</summary>
        public DateTime? DueDate { get; set; }

        /// <summary>Duty status (with/without duty). Field: DutyStatus</summary>
        public BoYesNoEnum? DutyStatus { get; set; }

        /// <summary>End delivery date</summary>
        public DateTime? EndDeliveryDate { get; set; }

        /// <summary>End delivery time (SAP often stores HHmm; using TimeSpan)</summary>
        public TimeSpan? EndDeliveryTime { get; set; }

        /// <summary>Financial period (FK to FinancePeriod). Field: FinncPriod</summary>
        public int? FinancialPeriod { get; set; }

        /// <summary>Folio number (Mexico/Chile). Field: FolioNum</summary>
        public int? FolioNumber { get; set; }

        /// <summary>Folio Number From</summary>
        public int? FolioNumberFrom { get; set; }

        /// <summary>Folio Number To</summary>
        public int? FolioNumberTo { get; set; }

        /// <summary>Folio prefix (MX/CL). Field: FolioPref (len 2)</summary>
        public string? FolioPrefixString { get; set; }

        /// <summary>From warehouse code. Field: Filler (len 8, FK Warehouses)</summary>
        public string? FromWarehouse { get; set; }

        /// <summary>Journal memo. Field: JrnlMemo (len 50)</summary>
        public string? JournalMemo { get; set; }

        /// <summary>Last page folio number (Chile). Field: LPgFolioN</summary>
        public int? LastPageFolioNumber { get; set; }

        /// <summary>Letter (placeholder; localization)</summary>
        public string? Letter { get; set; }

        /// <summary>Lines collection</summary>
        public List<StockTransferLine> StockTransferLines { get; set; } = new();

        /// <summary>Point of Issue Code</summary>
        public string? PointOfIssueCode { get; set; }

        /// <summary>Price list (FK PriceLists). Field: GroupNum</summary>
        public int? PriceList { get; set; }

        /// <summary>Printed flag (BoYesNoEnum). Using bool for convenience.</summary>
        public BoYesNoEnum? Printed { get; set; }

        /// <summary>Reference 1. Field: Ref1 (len 11)</summary>
        public string? Reference1 { get; set; }

        /// <summary>Reference 2. Field: Ref2 (len 11)</summary>
        public string? Reference2 { get; set; }

        /// <summary>Sales person code (FK SalesPersons). Field: SlpCode</summary>
        public int? SalesPersonCode { get; set; }

        /// <summary>SAP Passport (trace id / header)</summary>
        public string? SAPPassport { get; set; }

        /// <summary>Series (FK to numeration series). Field: Series</summary>
        public int? Series { get; set; }

        /// <summary>Ship-to code</summary>
        public string? ShipToCode { get; set; }

        /// <summary>Start delivery date</summary>
        public DateTime? StartDeliveryDate { get; set; }

        /// <summary>Start delivery time (HH:mm)</summary>
        public TimeSpan? StartDeliveryTime { get; set; }

        /// <summary>Approval requests (placeholder)</summary>
        public object? StockTransfer_ApprovalRequests { get; set; }

        /// <summary>Tax date. Field: TaxDate</summary>
        public DateTime? TaxDate { get; set; }

        /// <summary>Tax extension (child object)</summary>
        public StockTransferTaxExtension? TaxExtension { get; set; }

        /// <summary>To warehouse code. Field: ToWhsCode (len 8)</summary>
        public string? ToWarehouse { get; set; }

        /// <summary>Transaction Id (OJDT->TransId). Field: TransId</summary>
        public int? TransNum { get; set; }

        /// <summary>Last update date</summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>User-defined fields bag</summary>
        public Dictionary<string, object?>? UserFields { get; set; }

        /// <summary>VAT registration number</summary>
        public string? VATRegNum { get; set; }

        /// <summary>Vehicle plate</summary>
        public string? VehiclePlate { get; set; }
    }

    /// <summary>
    /// One row of a Stock Transfer (DI API: StockTransfer_Lines)
    /// </summary>
    public class StockTransferLine
    {
        // --- Base/Linkage ---
        /// <summary>Used for TARIC No. in GR (E-Books). </summary>
        public string? AdditionalIdentifier { get; set; }

        /// <summary>Source document ID. Field: BaseEntry</summary>
        public int? BaseEntry { get; set; }

        /// <summary>Source document line. Field: BaseLine</summary>
        public int? BaseLine { get; set; }

        /// <summary>Base doc type (InvBaseDocTypeEnum). Field: BaseType</summary>
        public int? BaseType { get; set; }

        /// <summary>Parent DocEntry. Field: DocEntry</summary>
        public int? DocEntry { get; set; }

        // --- Item / Warehouse ---
        /// <summary>Item code (len 20, mandatory). Field: ItemCode</summary>
        public string? ItemCode { get; set; }

        /// <summary>Item description (len 100). Field: Dscription</summary>
        public string? ItemDescription { get; set; }

        /// <summary>From warehouse (len 8). Field: FromWhsCod</summary>
        public string? FromWarehouseCode { get; set; }

        /// <summary>To/target warehouse for this row (len 8). Field: WarehouseCode</summary>
        public string? WarehouseCode { get; set; }

        /// <summary>UoM code. Field: UoMCode</summary>
        public string? UoMCode { get; set; }

        /// <summary>UoM entry (FK). Field: UoMEntry</summary>
        public int? UoMEntry { get; set; }

        /// <summary>Measure unit text (len 20). Field: unitMsr</summary>
        public string? MeasureUnit { get; set; }

        /// <summary>Units per measure. Field: NumPerMsr</summary>
        public double? UnitsOfMeasurment { get; set; }

        /// <summary>Use base units? Field: UseBaseUn</summary>
        public bool? UseBaseUnits { get; set; }

        /// <summary>Line number. Field: LineNum</summary>
        public int? LineNum { get; set; }

        /// <summary>Line status. Field: LineStatus</summary>
        public BoStatus? LineStatus { get; set; }

        // --- Quantities / Batches / Serials / Bins ---
        /// <summary>Quantity. Field: Quantity</summary>
        public double? Quantity { get; set; }

        /// <summary>Inventory quantity (system UoM). </summary>
        public double? InventoryQuantity { get; set; }

        /// <summary>Remaining open quantity. </summary>
        public double? RemainingOpenQuantity { get; set; }

        /// <summary>Remaining open inventory quantity. </summary>
        public double? RemainingOpenInventoryQuantity { get; set; }

        /// <summary>Single serial (legacy). Field: SerialNum (len 17)</summary>
        public string? SerialNumber { get; set; }

        /// <summary>Serial assignments object (child).</summary>
        public List<SerialNumberRow>? SerialNumbers { get; set; }

        /// <summary>Batch assignments object (child).</summary>
        public List<BatchNumberRow>? BatchNumbers { get; set; }

        /// <summary>Bin allocations object (child).</summary>
        public List<BinAllocationRow>? BinAllocations { get; set; }

        // --- Prices / Currency / Taxes ---
        /// <summary>Currency (len 3). Field: Currency</summary>
        public string? Currency { get; set; }

        /// <summary>Exchange rate. Field: Rate</summary>
        public double? Rate { get; set; }

        /// <summary>Discount percent. Field: DiscPrcnt</summary>
        public double? DiscountPercent { get; set; }

        /// <summary>Price. Field: Price</summary>
        public double? Price { get; set; }

        /// <summary>Unit price (raw). Field: UnitPrice</summary>
        public double? UnitPrice { get; set; }

        /// <summary>VAT group. Field: VatGroup</summary>
        public string? VatGroup { get; set; }

        /// <summary>Plastic package tax exemption reason (len 2). Field: PPTaxExRe</summary>
        public string? PlasticPackageExemptionReason { get; set; }

        /// <summary>Total weight of recycled plastic for this row.</summary>
        public double? WeightOfRecycledPlastic { get; set; }

        // --- Costing / Dimensions / Projects ---
        /// <summary>Distribution rule (cost center). Field: OcrCode</summary>
        public string? DistributionRule { get; set; }

        /// <summary>Distribution rule 2. Field: OcrCode2</summary>
        public string? DistributionRule2 { get; set; }

        /// <summary>Distribution rule 3. Field: OcrCode3</summary>
        public string? DistributionRule3 { get; set; }

        /// <summary>Distribution rule 4. Field: OcrCode4</summary>
        public string? DistributionRule4 { get; set; }

        /// <summary>Distribution rule 5. Field: OcrCode5</summary>
        public string? DistributionRule5 { get; set; }

        /// <summary>Project code. Field: Project</summary>
        public string? ProjectCode { get; set; }

        /// <summary>Vendor number (len 17). Field: VendorNum</summary>
        public string? VendorNum { get; set; }

        // --- Factors ---
        /// <summary>Factor1. Field: Factor1</summary>
        public double? Factor { get; set; }

        /// <summary>Factor2. Field: Factor2</summary>
        public double? Factor2 { get; set; }

        /// <summary>Factor3. Field: Factor3</summary>
        public double? Factor3 { get; set; }

        /// <summary>Factor4. Field: Factor4</summary>
        public double? Factor4 { get; set; }

        // --- Misc / Children ---
        /// <summary>CCD numbers (child placeholder).</summary>
        public List<string>? CCDNumbers { get; set; }

        /// <summary>Pick list info for the line (child).</summary>
        public List<PickListLink>? PickLists { get; set; }

        /// <summary>User-defined fields bag.</summary>
        public Dictionary<string, object?>? UserFields { get; set; }
    }

    // -------- Minimal child models (expand as needed) --------

    public class SerialNumberRow
    {
        public string? SerialNumber { get; set; }
        public double? Quantity { get; set; } // usually 1 for serial-managed items
        public DateTime? ExpiryDate { get; set; }
        public Dictionary<string, object?>? UserFields { get; set; }
    }

    public class BatchNumberRow
    {
        public string? BatchNumber { get; set; }
        public double? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Dictionary<string, object?>? UserFields { get; set; }
    }

    public class BinAllocationRow
    {
        public string? BinCode { get; set; }
        public int? BinAbsEntry { get; set; }
        public double? Quantity { get; set; }
        public string? WarehouseCode { get; set; }
        public Dictionary<string, object?>? UserFields { get; set; }
    }

    public class PickListLink
    {
        public int? PickListId { get; set; }
        public int? PickListLineId { get; set; }
        public double? PickedQuantity { get; set; }
        public Dictionary<string, object?>? Extras { get; set; }
    }

    /// <summary>
    /// Minimal tax extension placeholder for StockTransfers.TaxExtension.
    /// Map fields like Incoterms, TaxId0/1, Nationality, etc. as needed.
    /// </summary>
    public class StockTransferTaxExtension
    {
        public string? Incoterms { get; set; }
        public string? Carrier { get; set; }
        public string? PlaceOfSupply { get; set; }
        public string? Comments { get; set; }
        public Dictionary<string, object?>? Extras { get; set; }
    }
}
