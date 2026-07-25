//Task 1 – Bank Queue (ConcurrentQueue)

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class Client
{
    public string Name { get; set; }
    public string Purpose { get; set; }

    public Client(string name, string purpose)
    {
        Name = name;
        Purpose = purpose;
    }
}

class BankSimulation
{
    static ConcurrentQueue<Client> queue = new ConcurrentQueue<Client>();
    static bool isRunning = true;

    static async Task Main()
    {
        var producer1 = Task.Run(() => AddClients("Thread-1"));
        var producer2 = Task.Run(() => AddClients("Thread-2"));

        var consumer = Task.Run(ProcessClients);

        await Task.WhenAll(producer1, producer2);

        isRunning = false;

        await consumer;

        Console.WriteLine("Bank closed.");
    }

    static void AddClients(string source)
    {
        string[] purposes = { "Deposit", "Withdraw", "Consultation" };

        for (int i = 1; i <= 5; i++)
        {
            var client = new Client($"{source}-Client-{i}", purposes[i % purposes.Length]);
            queue.Enqueue(client);

            Console.WriteLine($"[ENQUEUE] {client.Name} ({client.Purpose})");
            Thread.Sleep(300);
        }
    }

    static void ProcessClients()
    {
        while (isRunning || !queue.IsEmpty)
        {
            if (queue.TryDequeue(out Client client))
            {
                Console.WriteLine($"[PROCESS] Serving {client.Name} ({client.Purpose})");
                Thread.Sleep(500);
            }
        }
    }
}

//=========================================================================================================================================================================

//Task 2 – User Action Stack (ConcurrentStack)

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class UserActionSimulation
{
    static ConcurrentStack<string> stack = new ConcurrentStack<string>();

    static async Task Main()
    {
        var t1 = Task.Run(() => AddActions("User-1"));
        var t2 = Task.Run(() => AddActions("User-2"));

        await Task.WhenAll(t1, t2);

        Console.WriteLine("\n--- Undo Actions ---");

        while (!stack.IsEmpty)
        {
            if (stack.TryPop(out string action))
            {
                Console.WriteLine($"Undo: {action}");
                Thread.Sleep(300);
            }
        }

        Console.WriteLine("All actions undone.");
    }

    static void AddActions(string user)
    {
        string[] actions = { "Opened document", "Edited text", "Saved file", "Closed document" };

        for (int i = 0; i < actions.Length; i++)
        {
            string action = $"{user}: {actions[i]}";
            stack.Push(action);

            Console.WriteLine($"[PUSH] {action}");
            Thread.Sleep(200);
        }
    }
}