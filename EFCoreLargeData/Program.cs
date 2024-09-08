using BenchmarkDotNet.Running;
using EFCoreLargeData;

Console.WriteLine("----EF Large Data Benchmark------");

BenchmarkRunner.Run<EFBenchmark>();