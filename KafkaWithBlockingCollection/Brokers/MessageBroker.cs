﻿using System.Collections.Concurrent;
using System.Text.Json;

namespace KafkaWithBlockingCollection.Brokers;

public class MessageBroker : IMessageBroker
{
    private readonly ConcurrentDictionary<string, BlockingCollection<string>> _topics = new();
    private readonly ILogger<MessageBroker> _logger;

    public MessageBroker(ILogger<MessageBroker> logger)
    {
        _logger = logger;
    }

    public void Produce<T>(string topic, T message) where T : class
    {
        _logger.LogInformation("Got a message to produce");

        if (!_topics.ContainsKey(topic))
            _topics[topic] = new BlockingCollection<string>();

        _topics[topic].TryAdd(JsonSerializer.Serialize(message));
    }

    public T? Consume<T>(string topic, TimeSpan timeout) where T : class
    {
        if (!_topics.ContainsKey(topic))
            _topics[topic] = new BlockingCollection<string>();

        if (!_topics[topic].IsCompleted && _topics[topic].TryTake(out var message, timeout))
        {
            return JsonSerializer.Deserialize<T>(message);
        }

        return default;
    }

    public void Close(string topic)
    {
        if (!_topics.ContainsKey(topic))
            throw new Exception("There is no topic");

        _topics[topic].CompleteAdding();

        _topics.TryRemove(topic, out _);
    }
}
