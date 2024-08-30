using SemaphoreSlimWithCancellationToken;


var semaphore = new LockAndSemaphore();

Console.WriteLine("---SemaphoreSlim---");

var task1s = semaphore.DoWithSemaphore(1);
var task2s = semaphore.DoWithSemaphore(2);
var task3s = semaphore.DoWithSemaphore(3);
var task4s = semaphore.DoWithSemaphore(4);

await Task.WhenAll(task1s, task2s, task3s, task4s);


Console.WriteLine("---Lock---");
var task1l = semaphore.DoWithLock(1);
var task2l = semaphore.DoWithLock(2);
var task3l = semaphore.DoWithLock(3);
var task4l = semaphore.DoWithLock(4);


await Task.WhenAll(task1l, task2l, task3l, task4l);

