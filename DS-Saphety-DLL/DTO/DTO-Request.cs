using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DS_Saphety_DLL
{
    /*
     * Token DTO
     */

    public class TokenRequestDTO
    {
        public String username { get; set; }
        public String password { get; set; }
        public String virtual_operator { get; set; }
    }

    /*
     * Documento Soporte DTO
     */

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("PaymentMean.Class")]
    [ComVisible(true)]
    public class PaymentMean 
    {
        public String Code { get; set; }
        public String Mean { get; set; }
        public String DueDate { get; set; }
    }
    public class Identification
    {
        public String DocumentNumber { get; set; }
        public String DocumentType { get; set; }
        public String CountryCode { get; set; }
        public String CheckDigit { get; set; }
    }
    public class CustomerParty
    {
        public CustomerParty() { }
        public Identification Identification { get; set; } = new Identification();
    }
    public class Address
    {
        public String DepartmentCode { get; set; }
        public String CityCode { get; set; }
        public String AddressLine { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
    }
    public class SupplierParty
    {
        public String LegalType { get; set; }
        public String Email { get; set; }
        public String TaxScheme { get; set; }
        public List<String> ResponsabilityTypes { get; set; } = new List<String>();
        public String name { get; set; }
        public Identification Identification { get; set; } = new Identification();
        public Address Address { get; set; } = new Address();
    }
    public class TaxSubtotal
    {
        public String TaxCategory { get; set; }
        public String TaxPercentage { get; set; }
        public String TaxableAmount { get; set; }
        public String TaxAmount { get; set; }
    }
    public class TaxTotal
    {
        public String TaxCategory { get; set; }
        public String TaxAmount { get; set; }
        public String RoundingAmount { get; set; }
    }
    public class Item
    {
        public String Gtin { get; set; }
        public String Description { get; set; }
    }
    public class InvoicePeriod
    {
        public String From { get; set; }
        public String DescriptionCode { get; set; }
    }
    public class Line
    {
        public String Number { get; set; }
        public String Quantity { get; set; }
        public String QuantityUnitOfMeasure { get; set; }
        public List<TaxSubtotal> TaxSubtotals { get; set; } = new List<TaxSubtotal>();
        public List<TaxTotal> TaxTotals { get; set; } = new List<TaxTotal>();
        public String UnitPrice { get; set; }
        public String GrossAmount { get; set; }
        public String NetAmount { get; set; }
        public Item Item { get; set; } = new Item();
        public InvoicePeriod InvoicePeriod { get; set; } = new InvoicePeriod();
    }
    public class Total
    {
        public String GrossAmount { get; set; }
        public String TotalBillableAmount { get; set; }
        public String PayableAmount { get; set; }
        public String TaxableAmount { get; set; }
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("DocumentoSoporteDTO.Class")]
    [ComVisible(true)]
    public class DocumentoSoporteDTO
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
        public CustomerParty CustomerParty { get; set; } = new CustomerParty();
        public SupplierParty SupplierParty { get; set; } = new SupplierParty();
        public List<Line> Lines { get; set; } = new List<Line>();
        public List<TaxSubtotal> TaxSubtotals { get; set; } = new List<TaxSubtotal>();
        public List<TaxTotal> TaxTotals { get; set; } = new List<TaxTotal>();
        public Total Total { get; set; }
        public List<String> Notes { get; set; } = new List<String>();
        
        [DispId(0)]
        public void addPaymentMean(PaymentMean paymentMean)
        {
            this.PaymentMeans.Add(paymentMean);
        }

        [DispId(1)]
        public void addLine (Line line)
        {
            this.Lines.Add(line);
        }
        [DispId(2)]
        public void addTaxSubtotal(TaxSubtotal taxSubtotal)
        {
            this.TaxSubtotals.Add(taxSubtotal);
        }
        [DispId(3)]
        public void addTaxTotal(TaxTotal taxTotal)
        {
            this.TaxTotals.Add(taxTotal);
        }
    }
}
