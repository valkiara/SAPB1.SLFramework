using SAPB1.SLFramework.Enums;
using System.Runtime.InteropServices;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public abstract class Document
    {
        /// <summary>
        /// Returns the document unique key.
        /// </summary>
        public int? DocEntry { get; set; }

        /// <summary>
        /// Returns the document number.
        /// </summary>
        public int? DocNum { get; set; }
        /// <summary>
        /// Sets or returns a valid value of BoObjectTypes type that specifies the object type related to a draft document.
        /// </summary>
        public BoObjectTypes? DocObjectCode { get; set; }

        /// <summary>
        /// Determines whether or not this document is a Reserve Invoice, invoice that can be drawn to a delivery.
        /// Field name: isIns.
        /// Reserve Invoices allow issuing invoices for warehouse items without deducting the items from the inventory (SAP Business One creates a journal entry without creating an inventory entry).
        //Reserve Invoices refer only to item invoices.
        /// </summary>
        public BoYesNoEnum? ReserveInvoice { get; set; }


        public BoDocumentTypes? DocType { get; set; }

        /// <summary>
        /// Indicates the document cancel status.
        /// </summary>
        public CancelStatusEnum? CancelStatus { get; set; }

        /// <summary>
        /// Sets or returns the cancel date of the sales order or purchase order. After this date, shipping the goods to the customer or from the vendor is not allowed. 
        /// </summary>
        public DateTimeOffset? CancelDate { get; set; }

        /// <summary>
        /// Indicates whether the document was cancelled.
        /// </summary>
        public BoYesNoEnum? Cancelled { get; set; }

        /// <summary>
        /// Returns a valid value of BoStatus type that specifies document status (closed or open).
        /// </summary>
        public BoStatus? DocumentStatus { get; set; }

        public DownPaymentTypeEnum? DownPaymentType { get; set; }

        /// <summary>
        /// This enables to create a sub document with a separate series number. For example, creating an exempt invoice.
        /// </summary>
        public BoDocumentSubType? DocumentSubType { get; set; }

        /// <summary>
        /// Sets or returns the business place ID assigned to a marketing document (such as, invoice).
        /// </summary>
        public int? BPL_IDAssignedToInvoice { get; set; }

        /// <summary>
        /// Sets or returns the customer or vendor code.
        /// </summary>
        public string? CardCode { get; set; }


        public string? DocCurrency { get; set; }

        /// <summary>
        /// Sets or returns the customer or vendor name.
        /// </summary>
        public string? CardName { get; set; }

        /// <summary>
        /// Sets or returns the document posting date.
        /// </summary>
        public DateTimeOffset? DocDate { get; set; } 


        /// <summary>
        /// Sets or returns the document due date (for example, Delivery Date in sales orders, Value Date in invoices, Valid To in quotations, and so on).
        /// </summary>
        public DateTimeOffset? DocDueDate { get; set; }


        /// <summary>
        /// Sets or returns the date for the tax calculation or payment.
        /// </summary>
        public DateTimeOffset? TaxDate { get; set; } 


        /// <summary>
        /// Sets or returns comments for the document.
        /// </summary>
        public string? Comments { get; set; }


        /// <summary>
        ///Sets or returns the total amount in the document..
        /// </summary>
        public double? DocTotal { get; set; }

        public double? DocTotalFc { get; set; }

        public string? ControlAccount { get; set; }

        /// <summary>
        /// Returns the Document Line objects.
        /// </summary>
        public virtual IList<DocumentLine>? DocumentLines { get; set; } = [];
        public virtual IList<DownPaymentToDraw>? DownPaymentsToDraw { get; set; } = [];
    }

    /// <summary>
    /// Returns the Document Line objects.
    /// </summary>
    public class DocumentLine
    {
        /// <summary>Row number in the document.</summary>
        public int? LineNum { get; set; }

        /// <summary>Document type of the base document (e.g., 17 = Sales Order).</summary>
        public int? BaseType { get; set; }

        /// <summary>Document number of the base document.</summary>
        public int? BaseEntry { get; set; }

        /// <summary>Line number in the base document.</summary>
        public int? BaseLine { get; set; }

        /// <summary>Item code. Must be unique. (Required)</summary>
        public string? ItemCode { get; set; }

        /// <summary>Item name/description.</summary>
        public string? ItemDescription { get; set; }

        /// <summary>Ordered quantity.</summary>
        public double? Quantity { get; set; }

        /// <summary>Remaining quantity not yet delivered or invoiced.</summary>
        public double? RemainingOpenQuantity { get; set; }

        /// <summary>Remaining open inventory quantity.</summary>
        public double? RemainingOpenInventoryQuantity { get; set; }

        /// <summary>Open amount (local currency).</summary>
        public double? OpenAmount { get; set; }

        /// <summary>Open amount (foreign currency).</summary>
        public double? OpenAmountFC { get; set; }

        /// <summary>Open amount (system currency).</summary>
        public double? OpenAmountSC { get; set; }

        /// <summary>Package quantity.</summary>
        public double? PackageQuantity { get; set; }

        /// <summary>Status of the line (e.g., Open, Closed).</summary>
        public BoStatus? LineStatus { get; set; }

        /// <summary>Warehouse where the item is stored.</summary>
        public string? WarehouseCode { get; set; }

        /// <summary>Unit of measure entry (internal key).</summary>
        public int? UoMEntry { get; set; }

        /// <summary>Unit of measure code.</summary>
        public string? UoMCode { get; set; }

        /// <summary>Measurement unit (e.g., KG, EA).</summary>
        public string? MeasureUnit { get; set; }

        /// <summary>Number of items per measurement unit.</summary>
        public double? UnitsOfMeasurement { get; set; }

        /// <summary>Item price after VAT.</summary>
        public double? PriceAfterVAT { get; set; }

        /// <summary>Unit price (before VAT).</summary>
        public double? UnitPrice { get; set; }

        /// <summary>Item price (synonym for UnitPrice).</summary>
        public double? Price { get; set; }

        /// <summary>Total amount per line (excluding VAT).</summary>
        public double? LineTotal { get; set; }

        /// <summary>Total amount including VAT.</summary>
        public double? GrossTotal { get; set; }

        /// <summary>Gross price (price * quantity).</summary>
        public double? GrossPrice { get; set; }

        /// <summary>VAT group for the item.</summary>
        public string? VatGroup { get; set; }

        /// <summary>Tax code applied to the line.</summary>
        public string? TaxCode { get; set; }

        /// <summary>Tax percentage per line.</summary>
        public double? TaxPercentagePerRow { get; set; }

        /// <summary>Tax amount (excluding equalization tax) in local currency.</summary>
        public double? NetTaxAmount { get; set; }

        /// <summary>Tax amount (FC).</summary>
        public double? NetTaxAmountFC { get; set; }

        /// <summary>Tax amount (SC).</summary>
        public double? NetTaxAmountSC { get; set; }

        /// <summary>Total equalization tax.</summary>
        public double? TotalEqualizationTax { get; set; }

        /// <summary>G/L Account Code (Required for Service Documents).</summary>
        public string? AccountCode { get; set; }

        /// <summary>COGS (Cost of Goods Sold) Account Code.</summary>
        public string? COGSAccountCode { get; set; }

        /// <summary>Cost Center for Dimension 1.</summary>
        public string? CostingCode { get; set; }

        /// <summary>Cost Center for Dimension 2.</summary>
        public string? CostingCode2 { get; set; }

        /// <summary>Cost Center for Dimension 3.</summary>
        public string? CostingCode3 { get; set; }

        /// <summary>Cost Center for Dimension 4.</summary>
        public string? CostingCode4 { get; set; }

        /// <summary>Cost Center for Dimension 5.</summary>
        public string? CostingCode5 { get; set; }

        /// <summary>Sales employee code.</summary>
        public int? SalesPersonCode { get; set; }

        /// <summary>Project code linked to this line.</summary>
        public string? ProjectCode { get; set; }

        /// <summary>Requested ship/delivery date.</summary>
        public DateTime? ShipDate { get; set; }

        /// <summary>Required delivery date.</summary>
        public DateTime? RequiredDate { get; set; }

        /// <summary>Actual delivery date.</summary>
        public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>Discount percentage applied to the line.</summary>
        public double? DiscountPercent { get; set; }

        /// <summary>Whether the line is tax liable.</summary>
        public BoYesNoEnum? TaxLiable { get; set; }

        /// <summary>Barcode (EAN code) for the item.</summary>
        public string? BarCode { get; set; }

        /// <summary>Vendor catalog number.</summary>
        public string? SupplierCatNum { get; set; }

        /// <summary>Free text/comment field for the line.</summary>
        public string? FreeText { get; set; }

        /// <summary>Item volume.</summary>
        public double? Volume { get; set; }

        /// <summary>Volume unit (e.g., L, m3).</summary>
        public int? VolumeUnit { get; set; }

        /// <summary>Primary weight of the item.</summary>
        public double? Weight1 { get; set; }

        /// <summary>Secondary weight of the item.</summary>
        public double? Weight2 { get; set; }

        /// <summary>Unit of measure for primary weight.</summary>
        public int? Weight1Unit { get; set; }

        /// <summary>Unit of measure for secondary weight.</summary>
        public int? Weight2Unit { get; set; }

        /// <summary>Primary width of the item.</summary>
        public double? Width1 { get; set; }

        /// <summary>Secondary width of the item.</summary>
        public double? Width2 { get; set; }

        /// <summary>Unit of measure for Width1.</summary>
        public int? Width1Unit { get; set; }

        /// <summary>Unit of measure for Width2.</summary>
        public int? Width2Unit { get; set; }

        /// <summary>Primary height of the item.</summary>
        public double? Height1 { get; set; }

        /// <summary>Secondary height of the item.</summary>
        public double? Height2 { get; set; }

        /// <summary>Unit of measure for Height1.</summary>
        public int? Height1Unit { get; set; }

        /// <summary>Unit of measure for Height2.</summary>
        public int? Height2Unit { get; set; }

        /// <summary>Primary length of the item.</summary>
        public double? Lengh1 { get; set; }

        /// <summary>Secondary length of the item.</summary>
        public double? Lengh2 { get; set; }

        /// <summary>Unit of measure for Lengh1.</summary>
        public int? Lengh1Unit { get; set; }

        /// <summary>Unit of measure for Lengh2.</summary>
        public int? Lengh2Unit { get; set; }

        /// <summary>Address (used in purchasing documents).</summary>
        public string? Address { get; set; }

        /// <summary>CNPJ of the manufacturer (Brazil localization).</summary>
        public string? CNJPOfManufacturer { get; set; }

        /// <summary>Country of origin code (ISO Alpha-3).</summary>
        public string? CountryOrg { get; set; }

        /// <summary>Original item code (if substituted by an alternative).</summary>
        public string? OriginalItem { get; set; }

        /// <summary>Currency of the line.</summary>
        public string? Currency { get; set; }

        /// <summary>Agreement number (Blanket Agreement).</summary>
        public int? AgreementNo { get; set; }

        /// <summary>Agreement row number.</summary>
        public int? AgreementRowNumber { get; set; }

        /// <summary>Actual base document entry.</summary>
        public int? ActualBaseEntry { get; set; }

        /// <summary>Actual base document line number.</summary>
        public int? ActualBaseLine { get; set; }

        /// <summary>Applied tax amount (local currency).</summary>
        public double? AppliedTax { get; set; }

        /// <summary>Applied tax amount (foreign currency).</summary>
        public double? AppliedTaxFC { get; set; }

        /// <summary>Applied tax amount (system currency).</summary>
        public double? AppliedTaxSC { get; set; }

        /// <summary>Ship-To address code.</summary>
        public string? ShipToCode { get; set; }

        /// <summary>Vendor assigned to this line (if applicable).</summary>
        public string? LineVendor { get; set; }

        /// <summary>Additional identifier for the item (SWW field).</summary>
        public string? SWW { get; set; }

        /// <summary>Total tax amount (including equalization tax).</summary>
        public double? TaxTotal { get; set; }

        /// <summary>Tax before down payments (local currency).</summary>
        public double? TaxBeforeDPM { get; set; }

        /// <summary>Tax before down payments (FC).</summary>
        public double? TaxBeforeDPMFC { get; set; }

        /// <summary>Tax before down payments (SC).</summary>
        public double? TaxBeforeDPMSC { get; set; }

        /// <summary>Total amount (FC).</summary>
        public double? RowTotalFC { get; set; }

        /// <summary>Total amount (SC).</summary>
        public double? RowTotalSC { get; set; }

        /// <summary>Visual order of the line.</summary>
        public int? VisualOrder { get; set; }

        /// <summary>True if this is a non-inventory-affecting document.</summary>
        public BoYesNoEnum? WithoutInventoryMovement { get; set; }

        /// <summary>Exchange rate for this row.</summary>
        public double? Rate { get; set; }

        /// <summary>Down payments linked to this line.</summary>
        public IList<DownPaymentToDrawDetails> DownPaymentsToDrawDetails { get; set; } = [];
    }


    public class DownPaymentToDraw
    {
        public int? DocEntry { get; set; }
        public double? AmountToDraw { get; set; }
        public double? AmountToDrawFC { get; set; }
        public ICollection<DownPaymentToDrawDetails> DownPaymentsToDrawDetails { get; set; } = [];
        
    }

    public class DownPaymentToDrawDetails
    {
        public string? VatGroupCode { get; set; }
        public double? AmountToDraw { get; set; }
        public double? AmountToDrawFC { get; set; }
    }
}
