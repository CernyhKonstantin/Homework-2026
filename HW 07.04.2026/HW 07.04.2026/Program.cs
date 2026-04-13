using System;
using System.IO.Ports;

class Program
{
    static SerialPort port;

    static void Main()
    {
        try
        {
            port = new SerialPort("COM3", 9600);

            port.NewLine = "\r\n";
            port.ReadTimeout = 5000;
            port.WriteTimeout = 5000;

            port.DataReceived += Port_DataReceived;

            port.Open();

            Console.WriteLine("✅ Echo server started on COM3 (9600 baud)");
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();

            port.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error: " + ex.Message);
        }
    }

    private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            string data = port.ReadLine();

            Console.WriteLine($"[RECEIVED] {data}");

            port.WriteLine(data);

            Console.WriteLine($"[SENT] {data}");
        }
        catch (TimeoutException)
        {
            Console.WriteLine("⏱ Read timeout");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error: " + ex.Message);
        }
    }
}