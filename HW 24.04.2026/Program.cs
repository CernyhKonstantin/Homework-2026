using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5000/");
        listener.Start();

        Console.WriteLine("Server running on http://localhost:5000/");

        while (true)
        {
            var context = listener.GetContext();
            var request = context.Request;
            var response = context.Response;

            string path = request.Url.AbsolutePath;
            if (path == "/") path = "/index.html";

            string filePath = "wwwroot" + path;

            if (File.Exists(filePath))
            {
                byte[] content = File.ReadAllBytes(filePath);
                response.ContentType = GetContentType(filePath);
                response.OutputStream.Write(content, 0, content.Length);
            }
            else
            {
                string notFound = "<h1>404 Not Found</h1>";
                byte[] data = Encoding.UTF8.GetBytes(notFound);
                response.StatusCode = 404;
                response.OutputStream.Write(data, 0, data.Length);
            }

            response.Close();
        }
    }

    static string GetContentType(string path)
    {
        if (path.EndsWith(".html")) return "text/html";
        if (path.EndsWith(".css")) return "text/css";
        if (path.EndsWith(".png")) return "image/png";
        if (path.EndsWith(".jpg")) return "image/jpeg";
        return "text/plain";
    }
}
