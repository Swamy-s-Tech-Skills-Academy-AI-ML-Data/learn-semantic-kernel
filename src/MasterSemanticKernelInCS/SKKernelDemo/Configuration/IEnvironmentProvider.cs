namespace SKKernelDemo.Configuration;

internal interface IEnvironmentProvider
{
    string GetEnvironmentVariable(string key);
}
