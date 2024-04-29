namespace GeminiIntegration;

public interface IDigitalizable<T>
{
    public string Prompt { get; }

    public Task<T> GetDigitalizedDataAsync(string filePath);
}
