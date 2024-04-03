C# Serializers

I made a comparison between these libraries:

- NewtonSoft.Json (as well as System.Text.Json)
- Protobuf
- Apache.Avro
- MessagePack
- Bson

## Performace Benchmark
```bash
| Method      | Mean       | Error    | StdDev   | Ratio        | Allocated |
|------------ |-----------:|---------:|---------:|-------------:|----------:|
| NewtonSoft  |   896.9 ns | 10.61 ns |  9.93 ns |     baseline |    1632 B |
| System.Text |   557.0 ns |  6.61 ns |  6.19 ns | 1.61x faster |     320 B |
| Protobuf    |   474.2 ns |  4.92 ns |  4.36 ns | 1.89x faster |     344 B |
| Apache.Avro |   880.8 ns |  6.70 ns |  6.26 ns | 1.02x faster |     952 B |
| MessagePack |   290.7 ns |  5.76 ns |  7.07 ns | 3.08x faster |     120 B |
| Bson        | 1,123.0 ns | 21.38 ns | 21.00 ns | 1.25x slower |    1208 B |
```

## Output size
```bash
| Library     | Output size|
|------------ |-----------:|
| NewtonSoft  |   148 B    |
| System.Text |   148 B    |
| Protobuf    |   97  B    |
| Apache.Avro |   93  B    |
| MessagePack |   95  B    |
| Bson        |   160 B    |
```

## You can watch the video here: 👇
[![Watch the video](https://img.youtube.com/vi/qWacutAW3e8/hqdefault.jpg)](https://youtu.be/qWacutAW3e8)
