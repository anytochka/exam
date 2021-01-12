using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Task1
{
    class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            Console.Write("Enter the count of threads: ");
            int.TryParse(Console.ReadLine(), out var maxThreads);
            ThreadPool.SetMaxThreads(maxThreads, maxThreads);
            var watch = Stopwatch.StartNew();

            var tasks = new Task[maxThreads];
            for (var i = 0; i < maxThreads; i++)
                tasks[i] = Task.Run(() => Job());

            Task.WaitAll(tasks);
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
                    if (IsPrime(i))
                        simpleNums.Add(i);
            }
        }

        public static bool IsPrime(int n)
        {
            var result = true;
            if (n > 1)
            {
                for (var i = 2u; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }

}
