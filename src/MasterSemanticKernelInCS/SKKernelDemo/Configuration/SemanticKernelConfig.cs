namespace SKKernelDemo.Configuration;

internal sealed class SemanticKernelConfig
{
    public string OpenAIKey { get; private set; }

    public string AzureEndpoint { get; private set; }

    public string AzureKey { get; private set; }

    public string OpenAIModel { get; private set; }

    public string AzureModel { get; private set; }

    public SemanticKernelConfig()
    {
        OpenAIKey = GetEnvironmentVariable("OPENAI_API_KEY");

        AzureEndpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");

        AzureKey = GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

        OpenAIModel = "gpt-4o-mini-2024-07-18";

        AzureModel = "gpt-4o-dname";
    }

    private static string GetEnvironmentVariable(string key)
    {
        return Environment.GetEnvironmentVariable(key) ?? throw new InvalidOperationException($"Missing environment variable: {key}");
    }
}
