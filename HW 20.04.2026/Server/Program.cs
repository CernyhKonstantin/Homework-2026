using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Loopback, 5000);
        listener.Start();

        Console.WriteLine("Server started...");

        while (true)
        {
            var client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected");

            HandleClient(client);
        }
    }

    static void HandleClient(TcpClient client)
    {
        var stream = client.GetStream();

        string[] words = { "elephant", "computer", "germany", "banana" };
        Random rnd = new Random();
        string word = words[rnd.Next(words.Length)];

        char[] board = new string('_', word.Length).ToCharArray();
        int attempts = 6;
        string history = "";

        Send(stream, $"WORD:{new string(board)};ATTEMPTS:{attempts};HISTORY:{history}");

        while (attempts > 0)
        {
            byte[] buffer = new byte[1024];
            int bytes = stream.Read(buffer, 0, buffer.Length);

            if (bytes == 0) break;

            string guess = Encoding.UTF8.GetString(buffer, 0, bytes).Trim().ToLower();

            if (guess.Length != 1)
                continue;

            char letter = guess[0];
            history += letter + " ";

            if (word.Contains(letter))
            {
                for (int i = 0; i < word.Length; i++)
                    if (word[i] == letter)
                        board[i] = letter;
            }
            else
            {
                attempts--;
            }

            if (!new string(board).Contains('_'))
            {
                Send(stream, $"WIN;WORD:{word}");
                break;
            }

            if (attempts == 0)
            {
                Send(stream, $"LOSE;WORD:{word}");
                break;
            }

            Send(stream, $"WORD:{new string(board)};ATTEMPTS:{attempts};HISTORY:{history}");
        }

        client.Close();
    }

    static void Send(NetworkStream stream, string msg)
    {
        byte[] data = Encoding.UTF8.GetBytes(msg + "\n");
        stream.Write(data, 0, data.Length);
    }
}
