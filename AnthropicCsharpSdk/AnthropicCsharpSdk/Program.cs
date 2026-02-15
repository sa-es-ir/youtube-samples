using Anthropic;
using Anthropic.Models.Messages;
using Microsoft.Extensions.AI;

var apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY");
const string prompt = "Hello Claude, tell me a very short story about a cat in the garden";

AnthropicClient client = new()
{
    ApiKey = apiKey,

};

MessageCreateParams parameters = new()
{
    MaxTokens = 1024,
    Messages =
    [
        new()
        {
            Role = Role.User,
            Content = prompt,
        },
    ],

    Model = Model.ClaudeSonnet4_5_20250929,
};

// You can choose to use either the SyncMessage or StreamingMessage method, or the IChatClient method.
// await SyncMessage(client, parameters);
// await StreamingMessage(client, parameters);

await WithIChatClient(client, prompt);


static async Task SyncMessage(AnthropicClient client, MessageCreateParams parameters)
{
    var response = await client.Messages.Create(parameters);

    var message = string.Join(
        "",
        response
            .Content.Select(message => message.Value)
            .OfType<TextBlock>()
            .Select((textBlock) => textBlock.Text)
    );

    Console.WriteLine(message);
    Console.ReadLine();
}

static async Task StreamingMessage(AnthropicClient client, MessageCreateParams parameters)
{
    IAsyncEnumerable<RawMessageStreamEvent> responseUpdates = client.Messages.CreateStreaming(
        parameters
    );

    await foreach (RawMessageStreamEvent rawEvent in responseUpdates)
    {
        if (rawEvent.TryPickContentBlockDelta(out var delta) && delta.Delta.TryPickText(out var text))
        {
            Console.Write(text.Text);
        }
    }

    Console.ReadLine();
}

static async Task WithIChatClient(AnthropicClient client, string prompt)
{
    IChatClient chatClient = client
        .AsIChatClient("claude-haiku-4-5")
        .AsBuilder()
        .UseFunctionInvocation()
        .Build();

    ChatOptions options = new()
    {
        MaxOutputTokens = 1024,
        Temperature = 0.1f
    };

    await foreach (
        var update in chatClient.GetStreamingResponseAsync(
            prompt,
            options
        )
    )
    {
        Console.Write(update);
    }
    Console.ReadLine();
}