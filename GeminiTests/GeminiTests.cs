using GeminiIntegration;
using Grpc.Core;
using Xunit.Abstractions;

namespace GeminiTests;

public class GeminiTests
{
    private readonly ITestOutputHelper _output;

    public GeminiTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task GenerateContentAsync_GenerateContentWithoutImages_Valid()
    {
        // arrange
        string prompt = "Hello";
        Gemini gemini = new Gemini();

        // act
        string actual = await gemini.GenerateContentAsync(prompt);
        _output.WriteLine(actual);

        // assert
        Assert.False(string.IsNullOrEmpty(actual));
    }

    [Fact]
    public async Task GenerateContentAsync_GenerateContentWithImages_Valid()
    {
        // arrange
        string filePath = @"..\..\..\..\SF1.png";
        string prompt = "What do you see in the image?";
        byte[] image = File.ReadAllBytes(filePath);
        Gemini gemini = new Gemini();

        // act
        string actual = await gemini.GenerateContentAsync(prompt, image);
        _output.WriteLine(actual);

        // assert
        Assert.False(string.IsNullOrEmpty(actual));
    }

    [Fact]
    public async Task GenerateContentAsync_NullPrompt_ThrowsArgumentException()
    {
        string prompt = null!;
        Gemini gemini = new Gemini();

        await Assert.ThrowsAsync<ArgumentException>(() => gemini.GenerateContentAsync(prompt));
    }

    [Fact]
    public async Task GenerateContentAsync_EmptyPrompt_ThrowsArgumentException()
    {
        string prompt = string.Empty;
        Gemini gemini = new Gemini();

        await Assert.ThrowsAsync<ArgumentException>(() => gemini.GenerateContentAsync(prompt));
    }

    [Fact]
    public async Task GenerateContentAsync_InvalidImage_Valid()
    {
        string prompt = "What do you see in the image?";
        byte[] image = new byte[] { 0 };

        Gemini gemini = new Gemini();

        await Assert.ThrowsAsync<RpcException>(() => gemini.GenerateContentAsync(prompt, image));
    }
}