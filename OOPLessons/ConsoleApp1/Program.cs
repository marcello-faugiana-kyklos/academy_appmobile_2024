//// See https://aka.ms/new-console-template for more information


//using OOPClassLibrary.Support;
//using System.Runtime.CompilerServices;

//var result = @".\BelfioreCodes.txt".GetPlaceAndCodesFromFile();

//string mazara = (await result)[0].Place;

//Console.WriteLine(mazara);

using CancellationTokenSource cancellationTokenSource =
    new CancellationTokenSource();

cancellationTokenSource.CancelAfter(50);

var t1 = PrintValues("A", 50, cancellationTokenSource.Token);

var t2 = PrintValues("B", 10, cancellationTokenSource.Token);

var t3 = Task.Run(() => PrintValues2("C", 10));

await Task.WhenAll(t1, t2, t3);

await Task.Delay(1000);

Console.WriteLine("Completed");



async Task<int> PrintValues(string s, int n, CancellationToken cancellationToken = default)
{

    for (int i = 0; i < n; i++)
    {
        await Task.Delay(10);
        await Console.Out.WriteLineAsync($"{s}_{i + 1:00}");
        if (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"Cancellation requested for {s}");
            return n / 2;
        }
    }
    return n * 2;
}

int PrintValues2(string s, int n)
{
    for (int i = 0; i < n; i++)
    {
        Thread.Sleep(10);
        Console.WriteLine($"{s}_{i + 1:00}");
    }
    return n * 2;
}