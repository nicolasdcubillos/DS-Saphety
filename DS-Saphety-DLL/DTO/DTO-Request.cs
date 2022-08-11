using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_Saphety_DLL
{
    internal class PaymentMean 
    {
        public String Code { get; set; }
        public String Mean { get; set; }
        public String DueDate { get; set; }
    }
    internal class Identification
    {
        public String DocumentNumber { get; set; }
        public String DocumentType { get; set; }
        public String CountryCode { get; set; }
        public String CheckDigit { get; set; }
    }
    internal class CustomerParty
    {
        public Identification Identification { get; set; }
    }
    internal class Address
    {
        public String DepartmentCode { get; set; }
        public String CityCode { get; set; }
        public String AddressLine { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
    }
    internal class SupplierParty
    {
        public String LegalType { get; set; }
        public String Email { get; set; }
        public String TaxScheme { get; set; }
        public List<String> ResponsabilityTypes { get; set; } = new List<String>();
        public String name { get; set; }
        public Identification Identification { get; set; }
        public Address Address { get; set; }
    }
    internal class TaxSubtotal
    {
        public String TaxCategory { get; set; }
        public String TaxPercentage { get; set; }
        public String TaxableAmount { get; set; }
        public String TaxAmount { get; set; }
    }
    internal class TaxTotal
    {
        public String TaxCategory { get; set; }
        public String TaxAmount { get; set; }
        public String RoundingAmount { get; set; }
    }
    internal class Item
    {
        public String Gtin { get; set; }
        public String Description { get; set; }
    }
    internal class InvoicePeriod
    {
        public String From { get; set; }
        public String DescriptionCode { get; set; }
    }
    internal class Line
    {
        public String Number { get; set; }
        public String Quantity { get; set; }
        public String QuantityUnitOfMeasure { get; set; }
        public List<TaxSubtotal> TaxSubtotals { get; set; } = new List<TaxSubtotal>();
        public List<TaxTotal> TaxTotals { get; set; } = new List<TaxTotal>();
        public String UnitPrice { get; set; }
        public String GrossAmount { get; set; }
        public String NetAmount { get; set; }
        public Item Item { get; set; }
        public InvoicePeriod InvoicePeriod { get; set; }
    }
    internal class Total
    {
        public String GrossAmount { get; set; }
        public String TotalBillableAmount { get; set; }
        public String PayableAmount { get; set; }
        public String TaxableAmount { get; set; }
    }
    internal class DocumentoSoporteDTO
    {
        public String Currency { get; set; }
        public String SeriePrefix { get; set; }
        public String SerieNumber { get; set; }
        public String IssueDate { get; set; }
        public String DueDate { get; set; }
        public String DeliveryDate { get; set; }
        public String OperationType { get; set; }
        public String CorrelationDocumentId { get; set; }
        public String SerieExternalKey { get; set; }
        public List<PaymentMean> PaymentMeans { get; set; } = new List<PaymentMean>();
        public CustomerParty CustomerParty { get; set; }
        public SupplierParty SupplierParty { get; set; }
        public List<Line> Lines { get; set; } = new List<Line>();
        public List<TaxSubtotal> TaxSubtotals { get; set; } = new List<TaxSubtotal>();
        public List<TaxTotal> TaxTotals { get; set; } = new List<TaxTotal>();
        public Total Total { get; set; }
        public List<String> Notes { get; set; } = new List<String>();
    }
}
