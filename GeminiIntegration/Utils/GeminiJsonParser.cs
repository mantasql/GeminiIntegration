using GeminiIntegration.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeminiIntegration.Utils;

public class GeminiJsonParser
{
    private JsonSerializerOptions options;

    public GeminiJsonParser()
    {
        options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString |
            JsonNumberHandling.WriteAsString
        };
    }

    public T ParseJsonResponse<T>(string json)
    {
        json = json.Trim();
        int startIndex = json.IndexOf("{");
        int endIndex = json.LastIndexOf("}");

        if (startIndex == -1 && endIndex == -1)
        {
            throw new JsonException("Invalid JSON string format");
        }

        string jsonData = json[startIndex..(endIndex + 1)];

        T? data = JsonSerializer.Deserialize<T>(jsonData, options);

        if (data is null)
        {
            throw new JsonParseException($"Cound not parse JSON");
        }

        return data;
    }
}
