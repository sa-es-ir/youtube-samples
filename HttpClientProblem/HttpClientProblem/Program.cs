
Console.WriteLine("Start connections...");
var client = new HttpClient();

for (int i = 0; i < 1000; i++)
{
    var result = await client.GetAsync("http://google.com");
    Console.WriteLine($"Request sent with status: {result.StatusCode}");
}

Console.WriteLine("Stop connections...");