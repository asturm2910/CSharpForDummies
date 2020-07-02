using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpForDummies
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Tasks and ContinuoWith Tasks...");
            //Task t1 = 
            Task.Run<long>(() => { return DoRunning("Runner 1"); }).ContinueWith((taskOne) => { PrintStuff(taskOne.Result); Task.Run<long>(() => { return DoRunning("Runner 2"); }).ContinueWith((t2) => { PrintStuff(t2.Result); }); }).Wait();
            //t1.Wait();

            Console.ReadLine();
        }

        private static long DoRunning(string runnerName)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 1000; i++)
            {
                Console.Write($"{runnerName}");
                Thread.Sleep(20);
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private static void PrintStuff(long stuff)
        {
            Console.WriteLine($"{stuff}");
        }
    }
}
