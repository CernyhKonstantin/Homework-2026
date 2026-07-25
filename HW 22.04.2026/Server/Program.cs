using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static UdpClient server = new UdpClient(5001);
    static Dictionary<string, Player> players = new();

    static void Main()
    {
        Console.WriteLine("UDP Server started on 5001");

        while (true)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            var data = server.Receive(ref ep);
            string msg = Encoding.UTF8.GetString(data).Trim();
            string key = ep.ToString();

            Handle(ep, key, msg);
        }
    }

    static void Handle(IPEndPoint ep, string key, string msg)
    {
        if (msg.StartsWith("JOIN"))
        {
            string name = msg.Split(':')[1];
            players[key] = new Player { Name = name };
            Send(ep, "Welcome " + name);
        }
        else if (msg == "LIST")
        {
            string list = string.Join(",", players.Values.Select(p => p.Name));
            Send(ep, "Players: " + list);
        }
        else if (msg == "GAME")
        {
            var p = players[key];
            p.Number = new Random().Next(1, 51);
            p.Attempts = 10;
            Send(ep, "Game started (1-50)");
        }
        else if (msg.StartsWith("GUESS"))
        {
            var p = players[key];
            int guess = int.Parse(msg.Split(':')[1]);
            p.Attempts--;

            if (guess == p.Number)
                Send(ep, "WIN");
            else if (p.Attempts <= 0)
                Send(ep, "LOSE: " + p.Number);
            else if (guess < p.Number)
                Send(ep, "Higher " + p.Attempts);
            else
                Send(ep, "Lower " + p.Attempts);
        }
        else if (msg == "EXIT")
        {
            players.Remove(key);
            Send(ep, "Bye");
        }
    }

    static void Send(IPEndPoint ep, string msg)
    {
        var data = Encoding.UTF8.GetBytes(msg);
        server.Send(data, data.Length, ep);
    }
}

class Player
{
    public string Name;
    public int Number;
    public int Attempts;
}
