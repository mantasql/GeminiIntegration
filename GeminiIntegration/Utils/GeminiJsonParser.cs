using GeminiIntegration.Exceptions;
using Newtonsoft.Json;

namespace GeminiIntegration.Utils;

public class GeminiJsonParser
{
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

        T? data = JsonConvert.DeserializeObject<T>(jsonData);

        if (data is null)
        {
            throw new JsonParseException($"Cound not parse JSON");
        }

        return data;
    }
}
