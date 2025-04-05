namespace SKKernelDemo.Configuration;

internal sealed class DefaultEnvironmentProvider : IEnvironmentProvider
{
    public string GetEnvironmentVariable(string key) =>
        Environment.GetEnvironmentVariable(key) ?? throw new InvalidOperationException($"Missing environment variable: {key}");
}