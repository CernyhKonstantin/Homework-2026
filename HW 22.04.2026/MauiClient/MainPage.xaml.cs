using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MauiClient;

public partial class MainPage : ContentPage
{
    UdpClient client = new UdpClient();
    IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"),5001);

    public MainPage()
    {
        InitializeComponent();
    }

    async void Join_Clicked(object sender, EventArgs e)
    {
        Send("JOIN:" + NameEntry.Text);
        Output.Text = Receive();
    }

    async void Game_Clicked(object sender, EventArgs e)
    {
        Send("GAME");
        Output.Text = Receive();
    }

    async void Guess_Clicked(object sender, EventArgs e)
    {
        Send("GUESS:" + GuessEntry.Text);
        Output.Text = Receive();
    }

    void Send(string msg)
    {
        var data = Encoding.UTF8.GetBytes(msg);
        client.Send(data, data.Length, server);
    }

    string Receive()
    {
        var ep = new IPEndPoint(IPAddress.Any,0);
        var data = client.Receive(ref ep);
        return Encoding.UTF8.GetString(data);
    }
}
