using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKKernelDemo.Services;

internal interface IAzurePromptService
{
    Task<string?> GetPromptResponseAsync(string prompt);

    IAsyncEnumerable<string?> StreamPromptResponseAsync(string prompt);
}
