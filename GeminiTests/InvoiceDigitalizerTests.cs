using GeminiIntegration;
using GeminiIntegration.Models;

namespace GeminiTests;

public class InvoiceDigitalizerTests
{
    [Theory]
    [InlineData(@"..\..\..\..\SF1.png", 0)]
    [InlineData(@"..\..\..\..\SF2.png", 1)]
    [InlineData(@"..\..\..\..\SF3.png", 2)]
    public async Task GetDigitalizedDataAsync_GetData_Valid(string filePath, int index)
    {
        // arrange
        InvoiceData expected = GetInvoiceData()[index];
        InvoiceDigitalizer digitalizer = new();

        // act
        InvoiceData actual = await digitalizer.GetDigitalizedDataAsync(filePath);

        // assert
        Assert.Multiple(() =>
        {
            Assert.Contains(expected.InvoiceNumber, actual.InvoiceNumber);
            Assert.Equal(expected.InvoiceDate, actual.InvoiceDate);
            Assert.Contains(expected.Buyer, actual.Buyer);
            Assert.Contains(expected.Seller, actual.Seller);
            Assert.Equal(expected.Subtotal, actual.Subtotal);
            Assert.Equal(expected.VAT, actual.VAT);
            Assert.Equal(expected.Total, actual.Total);
        });
    }

    [Fact]
    public async Task GetDigitalizedDataAsync_InvalidFilePath_ThrowsFileNotFoundException()
    {
        string filePath = "InvalidFileName";
        InvoiceDigitalizer digitalizer = new();

        await Assert.ThrowsAsync<FileNotFoundException>(() => digitalizer.GetDigitalizedDataAsync(filePath));
    }

    [Fact]
    public async Task GetDigitalizedDataAsync_EmptyFilePath_ThrowsArgumentException()
    {
        string filePath = string.Empty;
        InvoiceDigitalizer digitalizer = new();

        await Assert.ThrowsAsync<ArgumentException>(() => digitalizer.GetDigitalizedDataAsync(filePath));
    }

    private static List<InvoiceData> GetInvoiceData()
    {
        return new List<InvoiceData>
        {
            new()
            {
                InvoiceNumber = "2066861",
                InvoiceDate = new DateTime(2023, 4, 3),
                Buyer = "Lobasoft",
                Seller = "Interneto vizija",
                Subtotal = 12.98M,
                VAT = 2.73M,
                Total = 15.71M,
            },
            new()
            {
                InvoiceNumber = "MS088246",
                InvoiceDate = new DateTime(2023, 11, 23),
                Buyer = "Lobasoft, UAB",
                Seller = "MERITS elektroninės sistemos",
                Subtotal = 971.53M,
                VAT = 204.02M,
                Total = 1175.55M,
            },
            new()
            {
                InvoiceNumber = "13682279",
                InvoiceDate = new DateTime(2023, 6, 30),
                Buyer = "UAB Lobasoft",
                Seller = "Valstybės įmonė Registrų centras",
                Subtotal = 8.04M,
                VAT = 0M,
                Total = 8.04M,
            },
        };
    }
}
