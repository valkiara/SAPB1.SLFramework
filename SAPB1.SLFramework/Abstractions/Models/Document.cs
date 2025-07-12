using SAPB1.SLFramework.Enums;

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


        public BoDocumentTypes? DocType { get; set; }


        /// <summary>
        /// Returns a valid value of BoStatus type that specifies document status (closed or open).
        /// </summary>
        public BoStatus DocumentStatus { get; set; }

        public DownPaymentTypeEnum? DownPaymentType { get; set; }

        /// <summary>
        /// Sets or returns the business place ID assigned to a marketing document (such as, invoice).
        /// </summary>
        public int? BPL_IDAssignedToInvoice { get; set; }

        /// <summary>
        /// Sets or returns the customer or vendor code.
        /// </summary>
        public required string CardCode { get; set; }


        public required string DocCurrency { get; set; }

        /// <summary>
        /// Sets or returns the customer or vendor name.
        /// </summary>
        public string? CardName { get; set; }

        /// <summary>
        /// Sets or returns the document posting date.
        /// </summary>
        public DateTimeOffset DocDate { get; set; } = DateTimeOffset.Now;


        /// <summary>
        /// Sets or returns the document due date (for example, Delivery Date in sales orders, Value Date in invoices, Valid To in quotations, and so on).
        /// </summary>
        public DateTimeOffset DocDueDate { get; set; } = DateTimeOffset.Now;


        /// <summary>
        /// Sets or returns the date for the tax calculation or payment.
        /// </summary>
        public DateTimeOffset TaxDate { get; set; } = DateTimeOffset.Now;


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
        public IList<DocumentLine> DocumentLines { get; set; } = [];
        public IList<DownPaymentToDraw> DownPaymentsToDraw { get; set; } = [];
    }

    /// <summary>
    /// Returns the Document Line objects.
    /// </summary>
    public class DocumentLine
    {
        public int LineNum { get; set; }

        public int? BaseType { get; set; }
        public int? BaseEntry { get; set; }
        public int? BaseLine { get; set; }

        /// <summary>
        /// Sets or returns the item code in the Document line. The item code must be unique.
        /// </summary>
        public required string ItemCode { get; set; }


        /// <summary>
        /// Sets or returns the item name/description.
        /// </summary>
        public string? ItemDescription { get; set; }


        /// <summary>
        /// Sets or returns the quantity of items.
        /// </summary>
        public double Quantity { get; set; }


        /// <summary>
        ///  Sets or returns the warehouse code where the item is stored.
        /// </summary>
        public string? WarehouseCode { get; set; }

        /// <summary>
        /// Sets or returns The key of a UoM. (UoM = Unit of measure)
        /// </summary>
        public int? UoMEntry { get; set; }


        /// <summary>
        ///  Sets or returns the unique code for the UoM. (UoM = Unit of measure)
        /// </summary>
        public string? UoMCode { get; set; }


        /// <summary>
        ///  Sets or returns the item price after taxation. 
        /// </summary>
        public double? PriceAfterVAT { get; set; }


        /// <summary>
        ///  Sets or returns the VAT group for the item specified in the row. 
        /// </summary>
        public string? VatGroup { get; set; }


        /// <summary>
        ///  Price of the item in the sales or purchasing document. 
        /// </summary>
        public double? UnitPrice { get; set; }

        public double? GrossPrice { get; set; }

        public string? AccountCode { get; set; }

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
