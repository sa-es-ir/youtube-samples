// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using EFCoreLargeData;

Console.WriteLine("EF Large Data");

BenchmarkRunner.Run<EFBenchmark>();

//var dbcontext = new TestDbContext();

//dbcontext.TextTable1MBs.Add(new TextTable1MB { Text = new string('x', 1024 * 1024 * 1) });
//dbcontext.TextTable2MBs.Add(new TextTable2MB { Text = new string('x', 1024 * 1024 * 2) });

//await dbcontext.SaveChangesAsync();

//await dbcontext.TextTables.ToListAsync();