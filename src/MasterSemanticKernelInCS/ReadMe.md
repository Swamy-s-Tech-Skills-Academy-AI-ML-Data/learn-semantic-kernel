# Updated Semantic Kernel Demo Script

 Lovable AI, Bolt.new, GetLazy.ai, Agent.ai, Cloud.dify.ai
 
## What is OpenAI?

OpenAI is an AI research and deployment company that develops advanced AI models, such as GPT (Generative Pre-trained Transformer). These models are capable of understanding and generating human-like text, enabling applications like chatbots, content generation, and more.

## What is Azure OpenAI?

Azure OpenAI is a service provided by Microsoft that integrates OpenAI's powerful models into the Azure cloud platform. It allows developers to leverage OpenAI's capabilities with the scalability, security, and compliance features of Azure.

## What is Hugging Face?

Hugging Face is a leading open-source platform for natural language processing (NLP) and machine learning. It provides a wide range of pre-trained models and tools for tasks like text classification, translation, and question answering. Hugging Face also offers APIs and libraries to integrate these models into applications easily.

## What is Semantic Kernel?

Semantic Kernel is an open-source framework designed to integrate AI models, such as OpenAI, Azure OpenAI, and Hugging Face, into applications. It provides a unified interface to interact with these models, enabling developers to build intelligent applications that leverage natural language processing, machine learning, and other AI capabilities.

## Why Use Semantic Kernel?

1. **Unified AI Integration**: Simplifies the integration of multiple AI services into a single application.
2. **Streamlined Development**: Provides abstractions and tools to build AI-powered features efficiently.
3. **Flexibility**: Allows switching between different AI providers with minimal code changes.
4. **Scalability**: Supports advanced features like streaming responses for real-time interactions.

## Overview of the Demo

This demo showcases the capabilities of the Semantic Kernel using three different services:

- **OpenAI**
- **Azure OpenAI**
- **Hugging Face**

The demo allows users to input a prompt and receive responses from these services, both as a single response and as a streaming response.

## Prerequisites

1. Ensure the following environment variables are set:

   - `OPENAI_API_KEY`
   - `AZURE_OPENAI_ENDPOINT`
   - `AZURE_OPENAI_API_KEY`
   - `HUGGINGFACE_API_KEY`

2. Update the `appsettings.json` file with the required configurations for models and endpoints.

## Steps to Run the Demo

1. Build the project using your preferred IDE or command line.
2. Run the `SKKernelDemoV1` project.
3. Enter a prompt when prompted in the console.

## Expected Output

The demo will display responses from the following services:

### OpenAI

- **Single Response**: A complete response to the input prompt.
- **Streaming Response**: A streamed response to the input prompt.

### Azure OpenAI

- **Single Response**: A complete response to the input prompt.
- **Streaming Response**: A streamed response to the input prompt.

### Hugging Face

- **Single Response**: A complete response to the input prompt.
- **Streaming Response**: A streamed response to the input prompt.

## Example

```
============================= Semantic Kernel Demo =============================
This demo showcases the OpenAI, Azure OpenAI, and Hugging Face prompt services.
----------------------------- Semantic Kernel Demo -----------------------------

Enter your prompt: Write a short poem about AI.

******************** OpenAI Response ********************
AI is here to stay, brightening every day.

******************** OpenAI Streaming Response ********************
AI is here to stay, brightening every day.

******************** Azure OpenAI Response ********************
AI is the future, a bright and shining suture.

******************** Azure Streaming Response ********************
AI is the future, a bright and shining suture.

******************** Hugging Face Response ********************
AI, a marvel of our time, solving problems sublime.

******************** Hugging Face Streaming Response ********************
AI, a marvel of our time, solving problems sublime.

Press any key to exit...
```
