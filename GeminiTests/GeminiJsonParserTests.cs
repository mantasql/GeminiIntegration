using GeminiIntegration.Utils;
using System.Text.Json;

namespace GeminiTests;

public class GeminiJsonParserTests
{
    [Fact]
    public void ParseJsonResponse_ParseJson_Valid()
    {
        // arrange
        string json = "{ \"Name\" : \"John\", \"Surname\" : \"Doe\", \"Age\" : \"24\", \"DateOfBirth\" : \"2000-06-21\" }";
        Person expected = new() { Name = "John", Surname = "Doe", Age = 24, DateOfBirth = new DateTime(2000, 6, 21) };

        GeminiJsonParser parser = new();

        // act
        Person actual = parser.ParseJsonResponse<Person>(json);

        // assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ParseJsonResponse_InvalidJsonFormat_ThrowsJsonParseException()
    {
        string json = "{ \"Name\" : \"John\", \"Surname\" : \"Doe\", \"Age\" : \"24\", \"DateOfBirth\" : \"2000-06-21\".";
        Person expected = new() { Name = "John", Surname = "Doe", Age = 24, DateOfBirth = new DateTime(2000, 6, 21) };

        GeminiJsonParser parser = new();

        Assert.Throws<JsonException>(() => parser.ParseJsonResponse<Person>(json));
    }

    [Fact]
    public void ParseJsonResponse_EmptyJson_ThrowsJsonException()
    {
        string json = string.Empty;
        Person expected = new() { Name = "John", Surname = "Doe", Age = 24, DateOfBirth = new DateTime(2000, 6, 21) };

        GeminiJsonParser parser = new();

        Assert.Throws<JsonException>(() => parser.ParseJsonResponse<Person>(json));
    }

    [Fact]
    public void ParseJsonResponse_ParseJsonWithIdentifier_Valid()
    {
        // arrange
        string json = "```json{ \"Name\" : \"John\", \"Surname\" : \"Doe\", \"Age\" : \"24\", \"DateOfBirth\" : \"2000-06-21\" }```";
        Person expected = new() { Name = "John", Surname = "Doe", Age = 24, DateOfBirth = new DateTime(2000, 6, 21) };

        GeminiJsonParser parser = new();

        // act
        Person actual = parser.ParseJsonResponse<Person>(json);

        // assert
        Assert.Equivalent(expected, actual);
    }

    private class Person
    {
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
