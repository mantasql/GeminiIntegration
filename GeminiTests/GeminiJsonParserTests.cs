using GeminiIntegration.Exceptions;
using GeminiIntegration.Utils;
using Google.Cloud.AIPlatform.V1;
using Newtonsoft.Json;

namespace GeminiTests;

public class GeminiJsonParserTests
{
    [Fact]
    public void ParseJsonResponse_ParseJson_Valid()
    {
        // arrange
        string json = "{ \"Name\" : \"John\", \"Surname\" : \"Doe\", \"Age\" : \"24\", \"DateOfBirth\" : \"2000-06-21\" }";
        Person expected = new Person() { Name = "John", Surname = "Doe", Age = 24, DateOfBirth = new DateTime(2000, 6, 21) };

        GeminiJsonParser parser = new GeminiJsonParser();

        // act
        Person actual = parser.ParseJsonResponse<Person>(json);

        // assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ParseJsonResponse_InvalidJsonFormat_ThrowsJsonParseException()
    {
        string json = "{ \"Name\" : \"John\", \"Surname\" : \"Doe\", \"Age\" : \"24\", \"DateOfBirth\" : \"2000-06-21\".";
        Person expected = new Person() { Name = "John", Surname = "Doe", Age = 24, DateOfBirth = new DateTime(2000, 6, 21) };

        GeminiJsonParser parser = new GeminiJsonParser();

        Assert.Throws<JsonParseException>(() => parser.ParseJsonResponse<Person>(json));
    }

    [Fact]
    public void ParseJsonResponse_EmptyJson_ThrowsJsonException()
    {
        string json = string.Empty;
        Person expected = new Person() { Name = "John", Surname = "Doe", Age = 24, DateOfBirth = new DateTime(2000, 6, 21) };

        GeminiJsonParser parser = new GeminiJsonParser();

        Assert.Throws<JsonException>(() => parser.ParseJsonResponse<Person>(json));
    }

    [Fact]
    public void ParseJsonResponse_ParseJsonWithIdentifier_Valid()
    {
        // arrange
        string json = "```json{ \"Name\" : \"John\", \"Surname\" : \"Doe\", \"Age\" : \"24\", \"DateOfBirth\" : \"2000-06-21\" }```";
        Person expected = new Person() { Name = "John", Surname = "Doe", Age = 24, DateOfBirth = new DateTime(2000, 6, 21) };

        GeminiJsonParser parser = new GeminiJsonParser();

        // act
        Person actual = parser.ParseJsonResponse<Person>(json);

        // assert
        Assert.Equivalent(expected, actual);
    }

    private class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public float Age { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
