﻿using GeminiIntegration;
using GeminiIntegration.Models;

namespace ConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        string filePath1 = @"..\..\..\..\SF1.png";
        string filePath2 = @"..\..\..\..\SF2.png";
        string filePath3 = @"..\..\..\..\SF3.png";

        InvoiceDigitalizer digitalizer = new InvoiceDigitalizer();
        InvoiceData data = await digitalizer.GetDigitalizedDataAsync(filePath1);

        Console.WriteLine(data.ToString());
    }
}
