using System.Net;
using System.Net.Mail;
using System.Text;

class Program
{
    static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();

        Console.WriteLine("Server started on http://localhost:8080");

        while (true)
        {
            var context = listener.GetContext();
            Handle(context);
        }
    }

    static void Handle(HttpListenerContext context)
    {
        var request = context.Request;
        var response = context.Response;

        if (request.Url.AbsolutePath == "/")
        {
            SendPage(response, "wwwroot/register.html");
        }
        else if (request.Url.AbsolutePath == "/register" && request.HttpMethod == "POST")
        {
            using var reader = new StreamReader(request.InputStream);
            string body = reader.ReadToEnd();

            var data = ParseForm(body);

            SendEmail(data["Email"], data["Name"]);

            SendPage(response, "wwwroot/success.html");
        }

        response.Close();
    }

    static Dictionary<string, string> ParseForm(string data)
    {
        var dict = new Dictionary<string, string>();

        foreach (var pair in data.Split('&'))
        {
            var kv = pair.Split('=');
            dict[kv[0]] = WebUtility.UrlDecode(kv[1]);
        }

        return dict;
    }

    static void SendPage(HttpListenerResponse response, string file)
    {
        string layout = File.ReadAllText("wwwroot/layout.html");
        string content = File.ReadAllText(file);

        string full = layout.Replace("{{content}}", content);

        byte[] data = Encoding.UTF8.GetBytes(full);
        response.ContentType = "text/html";
        response.OutputStream.Write(data, 0, data.Length);
    }

    static void SendEmail(string email, string name)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("YOUR_EMAIL@gmail.com", "APP_PASSWORD"),
            EnableSsl = true
        };

        var mail = new MailMessage(
            "YOUR_EMAIL@gmail.com",
            email,
            "Registration Successful",
            $"Hello {name}, your registration was successful!"
        );

        client.Send(mail);
    }
}
