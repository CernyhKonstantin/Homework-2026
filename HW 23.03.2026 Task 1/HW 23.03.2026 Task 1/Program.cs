using System;
using System.Threading;

class BankAccount
{
    private int balance;
    private bool isLocked = false;
    private readonly object lockObj = new object();

    public BankAccount(int initialBalance)
    {
        balance = initialBalance;
    }

    public void Deposit(int amount)
    {
        lock (lockObj)
        {
            if (isLocked)
            {
                Console.WriteLine("Account is locked. Deposit stopped.");
                return;
            }

            balance += amount;
            Console.WriteLine($"Deposit: +{amount}, Balance: {balance}");
        }
    }

    public void Withdraw(int amount)
    {
        lock (lockObj)
        {
            if (isLocked)
            {
                Console.WriteLine("Account is locked. Withdraw stopped.");
                return;
            }

            if (balance >= amount)
            {
                balance -= amount;
                Console.WriteLine($"Withdraw: -{amount}, Balance: {balance}");
            }
            else
            {
                Console.WriteLine("Not enough money.");
                isLocked = true;
                Console.WriteLine("Account is now LOCKED!");
            }
        }
    }

    public int GetBalance()
    {
        return balance;
    }
}

class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount(100);

        Thread t1 = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                account.Deposit(50);
                Thread.Sleep(50);
            }
        });

        Thread t2 = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                account.Withdraw(30);
                Thread.Sleep(50);
            }
        });

        Thread t3 = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                account.Withdraw(80);
                Thread.Sleep(50);
            }
        });

        t1.Start();
        t2.Start();
        t3.Start();

        t1.Join();
        t2.Join();
        t3.Join();

        Console.WriteLine($"Final Balance: {account.GetBalance()}");
    }
}