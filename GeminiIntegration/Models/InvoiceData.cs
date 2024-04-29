namespace GeminiIntegration.Models;

public class InvoiceData
{
    public string InvoiceNumber { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public string Buyer { get; set; } = null!;

    public string Seller { get; set; } = null!;

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
