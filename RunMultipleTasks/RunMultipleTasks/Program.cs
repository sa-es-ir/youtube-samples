Console.WriteLine("\r\n-----------Running Tasks-------------\r\n");

var cts = new CancellationTokenSource();

try
{
    Console.WriteLine($"In Parallel Start: {TimeProvider.System.GetLocalNow()}\r\n");

    var taskOne = DoOne(cts.Token);
    var taskTwo = DoTwo(cts.Token);
    var taskThree = DoThree(cts.Token);

    //await Task.WhenAll(taskOne, taskTwo, taskThree);

    await TaskExtentions.CustomWhenAll(new List<Task> { taskOne, taskTwo, taskThree });

    Console.WriteLine($"In Parallel End: {TimeProvider.System.GetLocalNow()}\r\n");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {TimeProvider.System.GetLocalNow()} -- {ex.Message}");

    cts.Cancel();
}

await Task.Delay(5000);

Console.WriteLine("DONE");

async Task DoOne(CancellationToken cancellationToken)
{
    await Task.Delay(1000, cancellationToken);
    throw new Exception("Error in DoOne");
}

async Task DoTwo(CancellationToken cancellationToken)
{
    await Task.Delay(2000, cancellationToken);
    Console.WriteLine("\tDone two!");
}

async Task DoThree(CancellationToken cancellationToken)
{
    await Task.Delay(3000, cancellationToken);
    Console.WriteLine("\tDone three!\r\n");
}


public static class TaskExtentions
{
    public static async Task CustomWhenAll(List<Task> tasks)
    {

        foreach (var task in tasks)
        {
            try
            {
                await task;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}