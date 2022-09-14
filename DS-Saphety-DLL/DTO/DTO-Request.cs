using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DS_Saphety_DLL
{
    /*
     * Configuracion DTO
     */

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("ConfiguracionDTO.Class")]
    [ComVisible(true)]
    public class ConfiguracionDTO
    {
        public String PATH { get; set; }
        public String WS_URL_PRUEBAS { get; set; }
        public String WS_URL_PRODUCCION { get; set; }
        public String AMBIENTE { get; set; }
        public String VIRTUAL_OPERATOR { get; set; }
        public String USERNAME { get; set; }
        public String PASSWORD { get; set; }
        public String TIPO_DCTO { get; set; }
        public String NOTA_AJUSTE { get; set; }
    }

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
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Identification.Class")]
    [ComVisible(true)]
    public class Identification
    {
        public String DocumentNumber { get; set; }
        public String DocumentType { get; set; }
        public String CountryCode { get; set; }
        public String CheckDigit { get; set; }
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("CustomerParty.Class")]
    [ComVisible(true)]
    public class CustomerParty
    {
        public Identification Identification { get; set; } = new Identification();
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Address.Class")]
    [ComVisible(true)]
    public class Address
    {
        public String DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public String CityCode { get; set; }
        public String CityName { get; set; }
        public String AddressLine { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("SupplierParty.Class")]
    [ComVisible(true)]
    public class SupplierParty
    {
        public String LegalType { get; set; }
        public String Email { get; set; }
        public String TaxScheme { get; set; }
        public List<String> ResponsabilityTypes { get; set; } = new List<String>();
        public String name { get; set; }
        public Identification Identification { get; set; } = new Identification();
        public Address Address { get; set; } = new Address();
        [DispId(0)]
        public void addResponsabilityType(String responsabilityType)
        {
            this.ResponsabilityTypes.Add(responsabilityType);
        }
    }
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TaxSubtotal.Class")]
    [ComVisible(true)]
    public class TaxSubtotal
    {
        public String TaxCategory { get; set; }
        public String TaxPercentage { get; set; }
        public String TaxableAmount { get; set; }
        public String TaxAmount { get; set; }
    }
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TaxTotal.Class")]
    [ComVisible(true)]
    public class TaxTotal
    {
        public String TaxCategory { get; set; }
        public String TaxAmount { get; set; }
        public String RoundingAmount { get; set; }
    }
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Item.Class")]
    [ComVisible(true)]
    public class Item
    {
        public String Gtin { get; set; }
        public String Description { get; set; }
    }
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("InvoicePeriod.Class")]
    [ComVisible(true)]
    public class InvoicePeriod
    {
        public String From { get; set; }
        public String DescriptionCode { get; set; }
    }
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Line.Class")]
    [ComVisible(true)]
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
        [DispId(0)]
        public void addTaxSubtotal(TaxSubtotal taxSubtotal)
        {
            this.TaxSubtotals.Add(taxSubtotal);
        }
        [DispId(1)]
        public void addTaxTotal(TaxTotal taxTotal)
        {
            this.TaxTotals.Add(taxTotal);
        }
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Total.Class")]
    [ComVisible(true)]
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
