using Google.Api.Gax.Grpc;
using Google.Cloud.AIPlatform.V1;
using Google.Protobuf;
using System.Text;

namespace GeminiIntegration;

public class Gemini
{
    private const string ProjectId = "{YOUR_PROJECT_ID}";
    private const string CredentialsPath = @"{PATH_TO_YOUR_SERVICE_ACCOUNT_KEY}";

    private const string Location = "europe-west1";
    private const string ModelId = "gemini-pro-vision";
    private const string Publisher = "google";
    private const string AiPlatformUrl = $"https://{Location}-aiplatform.googleapis.com";
    private const string ModelPath = $"projects/{ProjectId}/locations/{Location}/publishers/{Publisher}/models/{ModelId}";
    private const string EndpointUrl = $"{AiPlatformUrl}/v1/{ModelPath}:streamGenerateContent";

    private readonly GenerationConfig _config;

    public Gemini()
    {
        _config = new GenerationConfig
        {
            Temperature = 0.4f,
            TopP = 1,
            TopK = 34,
            MaxOutputTokens = 2048,
        };
    }

    public Gemini (GenerationConfig config)
    {
        _config = config;
    }

    public async Task<string> GenerateContentAsync(string prompt, byte[]? imageBuffer = null)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            throw new ArgumentException($"{nameof(prompt)} is null or empty");
        }

        GenerateContentRequest request = BuildRequest(prompt, imageBuffer);

        return await ContentResponseAsync(request);
    }

    private async Task<string> ContentResponseAsync(GenerateContentRequest request)
    {
        var predictionServiceClient = new PredictionServiceClientBuilder
        {
            CredentialsPath = CredentialsPath,
            Endpoint = EndpointUrl,
        }.Build();

        using PredictionServiceClient.StreamGenerateContentStream response = predictionServiceClient.StreamGenerateContent(request);

        AsyncResponseStream<GenerateContentResponse> responseStream = response.GetResponseStream();

        return await GetResponseText(responseStream);
    }

    private async Task<string> GetResponseText(AsyncResponseStream<GenerateContentResponse> responseStream)
    {
        StringBuilder fullText = new StringBuilder();

        await foreach (GenerateContentResponse responseItem in responseStream)
        {
            string? responseText = responseItem?.Candidates?[0]?.Content?.Parts?[0]?.Text;

            if (responseText != null)
            {
                fullText.Append(responseText);
            }
        }

        return fullText.ToString();
    }

    private GenerateContentRequest BuildRequest(string prompt, byte[]? imageBuffer = null)
    {
        var content = new Content
        {
            Role = "user"
        };

        content.Parts.Add(new Part { Text = prompt });

        if (imageBuffer != null)
        {
            ByteString imageData = ByteString.CopyFrom(imageBuffer);

            content.Parts.Add(new Part
            {
                InlineData = new()
                {
                    MimeType = "image/png",
                    Data = imageData,
                },
            });
        }

        var contentRequest = new GenerateContentRequest
        {
            Model = ModelPath,
            GenerationConfig = _config,
        };

        contentRequest.Contents.Add(content);

        return contentRequest;
    }
}
