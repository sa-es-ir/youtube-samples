using MessagePack;
using ProtoBuf;

namespace SerializerComparison;

[ProtoContract]
[MessagePackObject]
public class Order
{
    [ProtoMember(1)] // protobuf
    [Key(0)] // messagePack
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [ProtoMember(2)]
    [Key(1)]
    public string Name { get; set; }

    [ProtoMember(3)]
    [Key(2)]
    public string Category { get; set; }

    [ProtoMember(4)]
    [Key(3)]
    public long TotalAmount { get; set; }

    [ProtoMember(5)]
    [Key(4)]
    public string User { get; set; }

    public Order Create()
    {
        return new Order
        {
            Name = "Book Order",
            Category = "Books",
            TotalAmount = 100,
            User = Guid.NewGuid().ToString()
        };
    }
}
