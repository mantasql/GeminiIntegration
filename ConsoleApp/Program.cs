using GeminiIntegration;
using GeminiIntegration.Models;
using GeminiIntegration.Utils;

namespace ConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        string prompt = "Parašyk šios sąskaitos numerį, kas išrašė sąskaitą,\r\nkas sąskaitos gavėjas, sąskaitos datą, sąskaitos sumą be PVM, PVM sumą ir sąskaitos\r\nsumą su PVM. Atsakymą pateik json formatu. Duomenis pateik taip: { \"InvoiceNumber\" : \"string\", \"Buyer\" : \"string\", \"Seller\" : \"string\", \"InvoiceDate\" : \"string\", \"Subtotal\" : decimal, \"VAT\" : decimal, \"Total\" : decimal }. Pateik tik ką prašau.";
        string filePath = @"..\..\..\..\SF1.png";
        //byte[] image = File.ReadAllBytes(filePath);
        //Gemini gemini = new Gemini();

        //InvoiceData data = new GeminiJsonParser<InvoiceData>().ParseResponse(await gemini.GenerateContentAsync(prompt, image));

        InvoiceDigitalizer digitalizer = new InvoiceDigitalizer();
        InvoiceData data = await digitalizer.GetDigitalizedDataAsync(filePath);

        Console.WriteLine(data.ToString());
    }
}
