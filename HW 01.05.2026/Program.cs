using System.Net;
using System.Text;
using System.Text.Json;

class Program
{
    static List<Book> books = new List<Book>()
    {
        new Book{Id=1, Title="C# Basics", Author="John", Price=20},
        new Book{Id=2, Title="ASP.NET", Author="Anna", Price=25},
        new Book{Id=3, Title="Java", Author="Mike", Price=18}
    };

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
        var req = context.Request;
        var res = context.Response;

        string path = req.Url.AbsolutePath;
        string method = req.HttpMethod;

        if (path.StartsWith("/books"))
        {
            if (method == "GET") HandleGet(req, res);
            else if (method == "POST") HandlePost(req, res);
            else if (method == "PUT") HandlePut(req, res);
            else if (method == "DELETE") HandleDelete(req, res);
        }
        else
        {
            SendHtml(res, "<h2>Welcome to Book Store</h2>");
        }

        res.Close();
    }

    static void HandleGet(HttpListenerRequest req, HttpListenerResponse res)
    {
        string path = req.Url.AbsolutePath;

        if (path == "/books")
        {
            string title = req.QueryString["title"];
            string author = req.QueryString["author"];

            var result = books.AsEnumerable();

            if (!string.IsNullOrEmpty(title))
                result = result.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrEmpty(author))
                result = result.Where(b => b.Author.Contains(author));

            StringBuilder html = new StringBuilder("<ul>");

            foreach (var b in result)
                html.Append($"<li>{b.Id} {b.Title} - {b.Author} (${b.Price})</li>");

            html.Append("</ul>");

            SendHtml(res, html.ToString());
        }
        else
        {
            var parts = path.Split('/');
            if (parts.Length == 3 && int.TryParse(parts[2], out int id))
            {
                var b = books.FirstOrDefault(x => x.Id == id);

                if (b == null)
                {
                    SendHtml(res, "<p>Not found</p>");
                    return;
                }

                SendHtml(res, $"<p>{b.Title} by {b.Author} - ${b.Price}</p>");
            }
        }
    }

    static void HandlePost(HttpListenerRequest req, HttpListenerResponse res)
    {
        using var reader = new StreamReader(req.InputStream);
        string body = reader.ReadToEnd();

        var book = JsonSerializer.Deserialize<Book>(body);

        book.Id = books.Max(x => x.Id) + 1;
        books.Add(book);

        SendJson(res, book);
    }

    static void HandlePut(HttpListenerRequest req, HttpListenerResponse res)
    {
        var parts = req.Url.AbsolutePath.Split('/');

        if (parts.Length == 3 && int.TryParse(parts[2], out int id))
        {
            using var reader = new StreamReader(req.InputStream);
            string body = reader.ReadToEnd();

            var updated = JsonSerializer.Deserialize<Book>(body);
            var b = books.FirstOrDefault(x => x.Id == id);

            if (b == null)
            {
                SendJson(res, "Not found");
                return;
            }

            b.Title = updated.Title;
            b.Author = updated.Author;
            b.Price = updated.Price;

            SendJson(res, "Updated");
        }
    }

    static void HandleDelete(HttpListenerRequest req, HttpListenerResponse res)
    {
        var parts = req.Url.AbsolutePath.Split('/');

        if (parts.Length == 2)
        {
            books.Clear();
            SendJson(res, "All deleted");
        }
        else if (parts.Length == 3 && int.TryParse(parts[2], out int id))
        {
            books.RemoveAll(b => b.Id == id);
            SendJson(res, "Deleted");
        }
    }

    static void SendJson(HttpListenerResponse res, object data)
    {
        string json = JsonSerializer.Serialize(data);
        byte[] buffer = Encoding.UTF8.GetBytes(json);
        res.ContentType = "application/json";
        res.OutputStream.Write(buffer, 0, buffer.Length);
    }

    static void SendHtml(HttpListenerResponse res, string content)
    {
        string layout = File.Exists("layout.html")
            ? File.ReadAllText("layout.html")
            : "<html><body>{{content}}</body></html>";

        string html = layout.Replace("{{content}}", content);

        byte[] buffer = Encoding.UTF8.GetBytes(html);
        res.ContentType = "text/html";
        res.OutputStream.Write(buffer, 0, buffer.Length);
    }
}

class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
}
