namespace KafkaWithBlockingCollection.Brokers;

public class MessageModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }
}
