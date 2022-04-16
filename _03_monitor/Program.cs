using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03_monitor
{
    /*
        static void Enter(object obj);

        static bool TryEnter(object obj);
        static bool TryEnter(object obj, int millisecondsTimeout);
        static bool TryEnter(object obj, TimeSpan timeout);
     
        static void Exit(object obj); SynchronizationLockException
     */
    class Program
    {
        #region Sync with Interlocker
        class InterlockedCounter
        {
            int number = 0;
            int even = 0;
            public int Number
            {
                get { return number; }
            }
            public int Even
            {
                get { return even; }
            }
            public void UpdateFields()                         
            {                                                  
                for (int i = 0; i < 1_000_000; ++i)              
                {
                    // lock this
                    Interlocked.Increment(ref number);
                    if (number % 2 == 0)
                        Interlocked.Increment(ref even);
                    // unlock this
                }
            }
        }
        /*
       ■ Thread 1 reads count into register → 0
       ■ Thread 1 increments the register value → 1
       ■ Thread 1 saves the value to memory → 1
       ■ Scheduler disables Thread 1
       ■ Scheduler connects Thread 2
       ■ Thread 2 reads count into register → 1
       ■ Thread 2 increases the value of the register → 2
       ■ Thread 2 stores the value in memory → 2
       ■ Scheduler disables Thread 2
       ■ Scheduler connects Thread 1
       */
        private static void BadAsync()
        {
            Console.WriteLine("Sync Interlocked-methods:");
            InterlockedCounter c = new InterlockedCounter();
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", c.Number, c.Even);
        }
        #endregion
        #region Sync with Monitor
        class MonitorLockCounter
        {
            int number;
            int even;
            public int Number { get { return number; } }
            public int Even { get { return even; } }
            public void UpdateFields()
            {
                for (int i = 0; i < 1000000; ++i)
                {
                    //Monitor.Enter(this);

                    //string str = null; str.ToString(); // NullReferenceException

                    //++number;
                    //if (number % 2 == 0)
                    //    ++even;

                    //Monitor.Exit(this);

                    Monitor.Enter(this); // block this class fileds in other threads
                    try
                    {
                        ++number;
                        if (number % 2 == 0)
                            ++even;
                    }
                    finally
                    {
                        Monitor.Exit(this); // unblock
                    }
                }
            }
        }
        private static void GoodAsync()
        {
            Console.WriteLine("Sync Monitor-methods:");
            MonitorLockCounter c = new MonitorLockCounter();
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", c.Number, c.Even);
        }
        #endregion
        static void Main(string[] args)
        {
            BadAsync();
            //GoodAsync();
        }
    }
}
