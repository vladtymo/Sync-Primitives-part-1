using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _04_monitor
{
    /*
    public void SomeMethod()
    {
        lock (this)
        {
            // using resources
        }
    }
    */
    class Program
    {
        class LockCounter
        {
            int number;
            int evenNumbers;
            public int Number { get { return number; } }
            public int EvenNumbers { get { return evenNumbers; } }
            public void UpdateFields()
            {
                for (int i = 0; i < 1_000_000; ++i)
                {
                    lock (this)
                    {
                        ++number;
                        if (number % 2 == 0)
                            ++evenNumbers;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"Lock Sync:");
            LockCounter c = new LockCounter();

            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", c.Number, c.EvenNumbers); // 5M 2.5M
        }
    }
}
