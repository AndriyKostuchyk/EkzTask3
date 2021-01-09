using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Console.Write("Enter the count of threads: ");
            int.TryParse(Console.ReadLine(), out var maxThreads);
            ThreadPool.SetMaxThreads(maxThreads, maxThreads);
            var watch = Stopwatch.StartNew();

            var arrOfTasks = new Task[maxThreads];
            for (var i = 0; i < maxThreads; i++)
                arrOfTasks[i] = Task.Run(() => Job());

            await Task.WhenAll(arrOfTasks);
            watch.Stop();
            Console.WriteLine("Time: " + watch.ElapsedMilliseconds + "ms");
            Console.ReadLine();
        }

        public static void Job()
        {
            while (count < 100)
            {
                count++;
                var simpleNums = new List<int>();
                foreach (var i in Enumerable.Range(1, 10000))
                    if (isPrime(i))
                        simpleNums.Add(i);
            }
        }

        static bool isPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
