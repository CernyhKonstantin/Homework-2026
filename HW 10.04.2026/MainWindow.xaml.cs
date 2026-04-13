using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TcpClientApp
{
    public partial class MainWindow : Window
    {
        private TcpClient? client;
        private NetworkStream? stream;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client = new TcpClient();

                string address = AddressBox.Text;
                int port = int.Parse(PortBox.Text);

                await client.ConnectAsync(address, port);
                stream = client.GetStream();

                ChatBox.AppendText("Connected to server\n");

                _ = ReceiveMessages();
            }
            catch (Exception ex)
            {
                ChatBox.AppendText("Error: " + ex.Message + "\n");
            }
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            if (stream == null) return;

            string message = MessageBox.Text;

            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            await stream.WriteAsync(data, 0, data.Length);

            ChatBox.AppendText("You: " + message + "\n");
            MessageBox.Clear();
        }

        private async Task ReceiveMessages()
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                if (stream == null) break;

                int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytes == 0) break;

                string response = Encoding.UTF8.GetString(buffer, 0, bytes);

                Dispatcher.Invoke(() =>
                {
                    ChatBox.AppendText("Server: " + response + "\n");
                });
            }
        }
    }
}
