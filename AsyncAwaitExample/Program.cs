using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting an ASYNC and AWAIT example...");
            var tIntResult = startAsync("1");
            Thread.Sleep(2000);
            var tIntResult2 = startAsync("2");
            Thread.Sleep(3000);
            var tIntResult3 = startAsync("3");
            Thread.Sleep(5000);

            Console.WriteLine($"Alle Tasks gestartet: {tIntResult.Result} / {tIntResult2.Result} / {tIntResult3.Result} ...");

            await Task.WhenAll(tIntResult, tIntResult2, tIntResult3);

            Console.WriteLine("Alle Tasks abgeschlossen!");
            
            Console.ReadLine();
        }

        public static async Task<string> startAsync(string tName)
        {
            
            var runnerTask =  myRunner(1000, tName);
            string result = await runnerTask;

            return result;
        }

        private static async Task<string> myRunner(int counter, string tName)
        {
            int i = 1;
            while (counter > i)
            {
                Console.WriteLine($"Thread No: {tName}");
                i++;
                await Task.Delay(25);
            }

            return i.ToString();
        }
    }
}
