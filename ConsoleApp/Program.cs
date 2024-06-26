﻿using GeminiIntegration;
using GeminiIntegration.Models;

namespace ConsoleApp;

internal class Program
{
    static async Task Main()
    {
        string filePath1 = @"..\..\..\..\SF1.png";

        InvoiceDigitalizer digitalizer = new();
        InvoiceData data = await digitalizer.GetDigitalizedDataAsync(filePath1);

        Console.WriteLine(data.ToString());
    }
}
