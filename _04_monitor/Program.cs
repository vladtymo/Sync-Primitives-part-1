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
            int field1;
            int field2;
            public int Field1 { get { return field1; } }
            public int Field2 { get { return field2; } }
            public void UpdateFields()
            {
                for (int i = 0; i < 1_000_000; ++i)
                {
                    lock (this)
                    {
                        ++field1;
                        if (field1 % 2 == 0)
                            ++field2;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Lock Sync:");
            LockCounter c = new LockCounter();

            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(c.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();

            Console.WriteLine("Field1: {0}, Field2: {1}\n\n", c.Field1, c.Field2); // 5M 2.5M
        }
    }
}
