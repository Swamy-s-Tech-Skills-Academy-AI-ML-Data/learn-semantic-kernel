using Microsoft.Extensions.Configuration;

namespace SKKernelDemo.Configuration;

internal sealed class SemanticKernelConfig
{
    public string OpenAIKey { get; private set; }

    public string AzureEndpoint { get; private set; }

    public string AzureKey { get; private set; }

    public string OpenAIModel { get; private set; }

    public string AzureModel { get; private set; }

    public bool UseOsEnvForSensitive { get; private set; }

    public SemanticKernelConfig(IConfiguration configuration, IEnvironmentProvider envProvider)
    {
        UseOsEnvForSensitive = !bool.TryParse(configuration["UseOsEnvForSensitive"], out bool result) || result;

        // Load non-sensitive values from the configuration
        var semanticKernelSection = configuration.GetSection("SemanticKernel");

        OpenAIModel = semanticKernelSection["OpenAIModel"]
                      ?? throw new InvalidOperationException("Missing OpenAIModel configuration.");

        AzureModel = semanticKernelSection["AzureModel"]
                     ?? throw new InvalidOperationException("Missing AzureModel configuration.");

        if (UseOsEnvForSensitive)
        {
            OpenAIKey = envProvider.GetEnvironmentVariable("OPENAI_API_KEY");

            AzureEndpoint = envProvider.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");

            AzureKey = envProvider.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
        }
        else
        {
            OpenAIKey = semanticKernelSection["OpenAIKey"]
                        ?? throw new InvalidOperationException("Missing OpenAI API Key configuration.");

            AzureEndpoint = semanticKernelSection["AzureEndpoint"]
                            ?? throw new InvalidOperationException("Missing Azure OpenAI Endpoint configuration.");

            AzureKey = semanticKernelSection["AzureKey"]
                       ?? throw new InvalidOperationException("Missing Azure OpenAI API Key configuration.");
        }
    }
}
