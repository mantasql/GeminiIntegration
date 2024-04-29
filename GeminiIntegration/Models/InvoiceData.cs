namespace GeminiIntegration.Models;

public class InvoiceData
{
    public string InvoiceNumber { get; set; }

    public DateTime InvoiceDate { get; set; }

    public string Buyer { get; set; }

    public string Seller { get; set; }

    public decimal Subtotal { get; set; }

    public decimal VAT { get; set; }

    public decimal Total { get; set; }

    public override string ToString()
    {
        return $"Invoice number: {InvoiceNumber}\n" +
            $"Seller: {Seller}\n" +
            $"Buyer: {Buyer}\n" +
            $"Invoice date: {InvoiceDate}\n" +
            $"Subtotal: {Subtotal}\n" +
            $"VAT: {VAT}\n" +
            $"Total: {Total}";
    }
}
