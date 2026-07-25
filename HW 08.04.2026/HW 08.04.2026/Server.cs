using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Loopback, 5000);
        listener.Start();
        Console.WriteLine("Server started on port 5000...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected!");

            Thread t = new Thread(() => HandleClient(client));
            t.Start();
        }
    }

    static void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        Random rnd = new Random();

        int number = rnd.Next(1, 51);
        int failedAttempts = 0;

        Console.WriteLine($"[DEBUG] Number: {number}");

        DateTime startTime = DateTime.Now;

        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead == 0)
                break;

            string input = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

            if (!int.TryParse(input, out int guess))
            {
                Send(stream, "Please send a valid number!");
                continue;
            }

            if (guess < number)
            {
                failedAttempts++;
                Send(stream, "Higher");
            }
            else if (guess > number)
            {
                failedAttempts++;
                Send(stream, "Lower");
            }
            else
            {
                TimeSpan duration = DateTime.Now - startTime;

                Send(stream,
                    $"Correct! Time: {duration.TotalSeconds:F2}s, Failed attempts: {failedAttempts}");

                break;
            }
        }

        client.Close();
        Console.WriteLine("Client disconnected.");
    }

    static void Send(NetworkStream stream, string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message + "\n");
        stream.Write(data, 0, data.Length);
    }
}