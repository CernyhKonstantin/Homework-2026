using System.Net.Sockets;
using System.Text;

namespace MauiClient;

public partial class MainPage : ContentPage
{
    TcpClient client;
    NetworkStream stream;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void Connect_Clicked(object sender, EventArgs e)
    {
        client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 5000);
        stream = client.GetStream();
    }
}
