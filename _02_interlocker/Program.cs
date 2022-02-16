using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02_interlocker
{
    /*
        public static Int32 Increment(ref Int32 location); – увеличивает значение на 1;
        public static int Decrement(ref int location); – уменьшает значение на 1;
        public static int Add(ref int location, int value); – увеличивает/уменьшает значение на value;
        public static int Exchange(ref int locationi, int value); – обменивает параметры значениями;
        public static int CompareExchange(ref int location, 
            int value, int comparand) – сравнивает location и comparand и 
                                        присваивает location value в случае успеха.
    */
    class Counter
    {
        public static int count;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(delegate ()
                {
                    for (int j = 1; j <= 1_000_000; ++j)
                    {
                        Interlocked.Increment(ref Counter.count);
                    }
                });
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("counter = {0}", Counter.count);
        }
    }
}
