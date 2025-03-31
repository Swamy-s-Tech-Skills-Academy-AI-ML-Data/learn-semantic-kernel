# Mastering Semantic Kernel



// Load configuration
//var config = new SemanticKernelConfig();

//Kernel openAIKernel = Kernel.CreateBuilder()
//    .AddOpenAIChatCompletion(config.OpenAIModel, config.OpenAIKey)
//    .Build();

//IKernelBuilder azureKernelBuilder = Kernel.CreateBuilder()
//    .AddAzureOpenAIChatCompletion(config.AzureModel, config.AzureEndpoint, config.AzureKey);

//// Add logging
//azureKernelBuilder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

//// Build the kernel
//Kernel azureKernel = azureKernelBuilder.Build();

//#region Chat Completion
//var options = new OpenAIPromptExecutionSettings
//{
//    MaxTokens = 150,
//    Temperature = 0.9
//};

//var prompt = "Write a short poem about Semantic Kernel";

//// Open AI
//var openAIResult = await openAIKernel.InvokePromptAsync(prompt, new KernelArguments(options)).ConfigureAwait(false);
//ForegroundColor = ConsoleColor.DarkCyan;
//WriteLine(openAIResult);

//// Azure Open AI
//var azOpenAIResult = await azureKernel.InvokePromptAsync(prompt, new KernelArguments(options)).ConfigureAwait(false);
//ForegroundColor = ConsoleColor.DarkYellow;
//WriteLine($"\n\n{azOpenAIResult}");
//#endregion

//#region Chat Completion Streaming

//ForegroundColor = ConsoleColor.DarkMagenta;

//var fullMessageBuilder = new StringBuilder();

//await foreach (var chatUpdate in azureKernel.InvokePromptStreamingAsync<StreamingChatMessageContent>(prompt).ConfigureAwait(false))
//{
//    if (!string.IsNullOrEmpty(chatUpdate.Content))
//    {
//        fullMessageBuilder.Append(chatUpdate.Content);
//        Write(chatUpdate.Content);
//    }
//}

//ForegroundColor = ConsoleColor.Green;
//string fullMessage = fullMessageBuilder.ToString();
//WriteLine($"\n\n{fullMessage}");

//#endregion

//ResetColor();
//WriteLine("\n\nPress any key to exit...");
//ReadKey();