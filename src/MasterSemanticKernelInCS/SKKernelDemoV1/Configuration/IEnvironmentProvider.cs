namespace SKKernelDemoV1.Configuration;

internal interface IEnvironmentProvider
{
    string GetEnvironmentVariable(string key);
}
