using System;
using System.Threading;

class Program
{
    static void PrintLow()
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine("Low");
            Thread.Sleep(100);
        }
    }

    static void PrintNormal()
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine("Normal");
            Thread.Sleep(100);
        }
    }

    static void PrintHigh()
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine("High");
            Thread.Sleep(100);
        }
    }

    static void Main(string[] args)
    {
        Thread threadLow = new Thread(PrintLow);
        Thread threadNormal = new Thread(PrintNormal);
        Thread threadHigh = new Thread(PrintHigh);

        // Set priorities
        threadLow.Priority = ThreadPriority.Lowest;
        threadNormal.Priority = ThreadPriority.Normal;
        threadHigh.Priority = ThreadPriority.Highest;

        // Start threads
        threadLow.Start();
        threadNormal.Start();
        threadHigh.Start();

        // Wait for all threads to finish
        threadLow.Join();
        threadNormal.Join();
        threadHigh.Join();

        Console.WriteLine("All threads finished.");
    }
}