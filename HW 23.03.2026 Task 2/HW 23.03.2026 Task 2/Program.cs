using System;
using System.Threading;

class Program
{
    static CountdownEvent countdown = new CountdownEvent(10);

    static void ProcessOrder(object state)
    {
        int orderNumber = (int)state;

        Thread.Sleep(500);

        Console.WriteLine(
            $"Order {orderNumber} processed on Thread {Thread.CurrentThread.ManagedThreadId}"
        );

        countdown.Signal();
    }

    static void Main()
    {
        for (int i = 1; i <= 10; i++)
        {
            ThreadPool.QueueUserWorkItem(ProcessOrder, i);
        }

        countdown.Wait();

        Console.WriteLine("All orders processed.");
    }
}