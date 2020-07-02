using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskInterruption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            Task t = new Task(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    if (ct.IsCancellationRequested)
                    {
                        Console.WriteLine($"Iteration {i} wurde abgebrochen!");
                        ct.ThrowIfCancellationRequested();
                        break;
                    }
                    else
                    {
                        Thread.Sleep(25);
                    }
                }
            }, ct);

            try
            {
                t.Start();
                cts.CancelAfter(1000);
                t.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"Exception: {ex.InnerException.Message}");
            }

            Console.WriteLine($"Cancelled: {t.IsCanceled}, Finsihed: {t.IsCompleted}, Error: {t.IsFaulted}");
            Console.ReadLine();
        }
    }
}
