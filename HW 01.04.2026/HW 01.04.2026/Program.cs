using System;
using System.Threading;

class Program
{
    static Semaphore semaphore = new Semaphore(1, 1);

    static void Work(object id)
    {
        Console.WriteLine($"Thread {id} is waiting...");

        bool entered = false;

        try
        {
            entered = semaphore.WaitOne(10000);

            if (!entered)
            {
                Console.WriteLine($"Thread {id} timeout - could not enter");
                return;
            }

            Console.WriteLine($"Thread {id} entered");
            Thread.Sleep(2000);
        }
        finally
        {
            if (entered)
            {
                // Only release if successfully entered!
                Console.WriteLine($"Thread {id} leaving");
                semaphore.Release();
            }
        }
    }

    static void Main()
    {
        for (int i = 1; i <= 3; i++)
        {
            new Thread(Work).Start(i);
        }
    }
}



//The error is caused by this line:
//semaphore.WaitOne(10000);