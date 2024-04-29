using GeminiIntegration.Models;
using GeminiIntegration.Utils;

namespace GeminiIntegration;

public class InvoiceDigitalizer : IDigitalizable<InvoiceData>
{
    public string Prompt { get; } = "You receive an invoice and should respond with it's data. Data you will respond with is only buyer, seller, invoice date, invoice number, subtotal, VAT, total. Your response must be a JSON object. An invoice object has the following schema: { \"InvoiceNumber\" : \"string\", \"Buyer\" : \"string\", \"Seller\" : \"string\", \"InvoiceDate\" : \"string\", \"Subtotal\" : decimal, \"VAT\" : decimal, \"Total\" : decimal }";

    public async Task<InvoiceData> GetDigitalizedDataAsync(string filePath)
    {
        byte[] image = File.ReadAllBytes(filePath);

        Gemini gemini = new Gemini();
        InvoiceData data = new GeminiJsonParser().ParseJsonResponse<InvoiceData>(await gemini.GenerateContentAsync(Prompt, image));

        return data;
    }
}