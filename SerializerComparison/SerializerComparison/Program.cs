using Avro;
using Avro.IO;
using Avro.Reflect;
using BenchmarkDotNet.Running;
using MessagePack;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using ProtoBuf;
using SerializerComparison;
using System.Text;
using System.Text.Json;

var order = new Order().Create();

BenchmarkRunner.Run<SerializerBenchmark>();

return;

Console.WriteLine("=========== Serializer Comparison ===========");

// Newtonsoft
var json = Newtonsoft.Json.JsonConvert.SerializeObject(order);

Console.WriteLine($"Json size: {Encoding.UTF8.GetBytes(json).Length}");

// Text.Json
var jsonText = JsonSerializer.Serialize(order);

Console.WriteLine($"JsonText size: {Encoding.UTF8.GetBytes(jsonText).Length}");

// Protobuf
using var protobufMs = new MemoryStream();
Serializer.Serialize(protobufMs, order);

Console.WriteLine($"Protobuf size: {protobufMs.Length}");

string orderSchema = @"
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

// Avro
var schema = Schema.Parse(orderSchema);

var avroWriter = new ReflectWriter<Order>(schema);

using var avroMs = new MemoryStream();

avroWriter.Write(order, new BinaryEncoder(avroMs));

Console.WriteLine($"Avro size: {avroMs.Length}");


// MessagePack
var messagePack = MessagePackSerializer.Serialize(order);
Console.WriteLine($"MessagePack size: {messagePack.Length}");


// Bson
var bson = order.ToBson();

Console.WriteLine($"Bson size: {bson.Length}");

Console.ReadKey();
