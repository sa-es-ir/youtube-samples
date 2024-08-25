// See https://aka.ms/new-console-template for more information
using SemaphoreSlimWithCancellationToken;

Console.WriteLine("Hello, World!");


var fooService = new FooService();

var cts = new CancellationTokenSource();

var task1 = fooService.DoSomethingAsync(cts.Token);
var task2 = fooService.DoSomethingAsync(cts.Token);
var task3 = fooService.DoSomethingAsync(cts.Token);
var task4 = fooService.DoSomethingAsync(cts.Token);
var task5 = fooService.DoSomethingAsync(cts.Token);

await Task.WhenAll(task1, task2, task3, task4, task5);