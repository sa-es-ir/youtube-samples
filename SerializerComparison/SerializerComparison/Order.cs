namespace SerializerComparison;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Category { get; set; }

    public int TotalAmount { get; set; }

    public List<Item> Items { get; set; }

    public Order Create()
    {
        return new Order
        {
            Name = "Book Order",
            Category = "Books",

            TotalAmount = 100,
            Items = new List<Item>()
            {
                new()
                {
                    Name = "Item 1",
                    Amount = 50
                },
                new()
                {
                    Name = "Item 2",
                    Amount = 50
                }
            }
        };
    }
}

public class Item
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public int Amount { get; set; }
}
