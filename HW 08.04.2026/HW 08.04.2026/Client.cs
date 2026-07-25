using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        try
        {
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();

            Console.WriteLine("Connected to server!");

            while (true)
            {
                Console.Write("Enter a number (1-50): ");
                string input = Console.ReadLine();

                byte[] data = Encoding.UTF8.GetBytes(input + "\n");
                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                Console.WriteLine("Server: " + response);

                if (response.StartsWith("Correct"))
                    break;
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}