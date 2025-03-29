using Microsoft.SemanticKernel;

var kernel = Kernel.CreateBuilder().Build();

string valuesString = string.Join(", ", kernel.Data.Values.Select(v => v?.ToString()));

WriteLine($"Semantic Kernel is running. Data Value: {valuesString}");

WriteLine("Press any key to exit...");