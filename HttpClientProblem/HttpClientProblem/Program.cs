
Console.WriteLine("Start connections...");

for (int i = 0; i < 10; i++)
{
    using (var client = new HttpClient())
    {
        var result = await client.GetAsync("http://google.com");
        Console.WriteLine($"Request sent with status: {result.StatusCode}");
    }
}

Console.WriteLine("Stop connections...");