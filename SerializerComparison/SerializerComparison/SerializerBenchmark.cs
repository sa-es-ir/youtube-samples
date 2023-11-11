using Avro.IO;
using Avro.Reflect;
using Avro;
using BenchmarkDotNet.Attributes;
using MessagePack;
using MongoDB.Bson;
using ProtoBuf;
using System.Text.Json;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace SerializerComparison;

[Config(typeof(Config))]
[HideColumns(Column.RatioSD, Column.AllocRatio)]
[MemoryDiagnoser(false)]
public class SerializerBenchmark
{
    private Order _order = new Order().Create();

    private string orderSchema = @"
        {
            ""type"": ""record"",
            ""name"": ""Order"",
            ""fields"": [
                { ""name"": ""Id"", ""type"": ""string"" },
                { ""name"": ""Name"", ""type"": ""string"" },
                { ""name"": ""Category"", ""type"": ""string"" },
                { ""name"": ""User"", ""type"": ""string"" },
                { ""name"": ""TotalAmount"", ""type"": ""long"" }
            ]
        }";

    private ReflectWriter<Order> _avroWriter;

    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
        }
    }

    [GlobalSetup]
    public void Setup()
    {
        Schema schema = Schema.Parse(orderSchema);

        _avroWriter = new ReflectWriter<Order>(schema);
    }

    [Benchmark(Baseline = true)]
    public void Json()
        => Newtonsoft.Json.JsonConvert.SerializeObject(_order);

    [Benchmark]
    public void JsonText()
       => JsonSerializer.Serialize(_order);

    [Benchmark]
    public void Protobuf()
    {
        using var protobufMs = new MemoryStream();
        Serializer.Serialize(protobufMs, _order);
    }

    [Benchmark]
    public void Avro()
    {
        using var avroMs = new MemoryStream();

        _avroWriter.Write(_order, new BinaryEncoder(avroMs));
    }

    [Benchmark]
    public void MessagePack() => MessagePackSerializer.Serialize(_order);

    [Benchmark]
    public void Bson() => _order.ToBson();
}
