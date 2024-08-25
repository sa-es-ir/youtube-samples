// See https://aka.ms/new-console-template for more information
using ManageConcurrencyWays;

Console.WriteLine("Hello, World!");

//Console.WriteLine(BenchmarkRunner.Run<ConcurrencyBenchmark>());

var _readerWriterLockCheck = new ReaderWriterLockCheck();

var readTask1 = _readerWriterLockCheck.Set();
var writeTask = _readerWriterLockCheck.Set();
var writeTask2 = _readerWriterLockCheck.Set();
var readTask2 = _readerWriterLockCheck.Get();
var readTask4 = _readerWriterLockCheck.Get();
var writeTask3 = _readerWriterLockCheck.Set();
var readTask3 = _readerWriterLockCheck.Get();


await Task.WhenAll(readTask1, readTask2, writeTask, writeTask2, readTask3, writeTask3, readTask4);
