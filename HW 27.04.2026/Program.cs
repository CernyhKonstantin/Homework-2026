using System.Net;
using System.Text;
using System.Text.Json;

class Program
{
    static List<Student> students = new List<Student>()
    {
        new Student{Id=1, Name="John", Surname="Smith", Group="A1"},
        new Student{Id=2, Name="Anna", Surname="Brown", Group="A1"},
        new Student{Id=3, Name="Mike", Surname="Taylor", Group="B1"},
        new Student{Id=4, Name="Sara", Surname="Wilson", Group="B1"},
        new Student{Id=5, Name="Tom", Surname="Moore", Group="C1"},
        new Student{Id=6, Name="Kate", Surname="White", Group="C1"},
        new Student{Id=7, Name="Alex", Surname="Green", Group="A2"},
        new Student{Id=8, Name="Emma", Surname="Hall", Group="A2"},
        new Student{Id=9, Name="Leo", Surname="King", Group="B2"},
        new Student{Id=10, Name="Nina", Surname="Scott", Group="B2"}
    };

    static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();

        Console.WriteLine("Server running on http://localhost:8080/");

        while (true)
        {
            var context = listener.GetContext();
            HandleRequest(context);
        }
    }

    static void HandleRequest(HttpListenerContext context)
    {
        var request = context.Request;
        var response = context.Response;

        string path = request.Url.AbsolutePath;
        string method = request.HttpMethod;

        if (path.StartsWith("/student"))
        {
            if (method == "GET")
                HandleGet(request, response);
            else if (method == "POST")
                HandlePost(request, response);
            else if (method == "PUT")
                HandlePut(request, response);
        }
        else
        {
            Write(response, "<h1>404 Not Found</h1>");
        }

        response.Close();
    }

    static void HandleGet(HttpListenerRequest request, HttpListenerResponse response)
    {
        string path = request.Url.AbsolutePath;

        if (path == "/student")
        {
            string nameFilter = request.QueryString["Name"];
            string groupFilter = request.QueryString["Group"];

            var result = students.AsEnumerable();

            if (!string.IsNullOrEmpty(nameFilter))
                result = result.Where(s => s.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(groupFilter))
                result = result.Where(s => s.Group == groupFilter);

            StringBuilder html = new StringBuilder("<ul>");

            foreach (var s in result)
                html.Append($"<li>{s.Id} {s.Name} {s.Surname} ({s.Group})</li>");

            html.Append("</ul>");

            Write(response, html.ToString());
        }
        else
        {
            var parts = path.Split('/');
            if (parts.Length == 3 && int.TryParse(parts[2], out int id))
            {
                var student = students.FirstOrDefault(s => s.Id == id);

                if (student == null)
                {
                    Write(response, "<p>Student not found</p>");
                    return;
                }

                Write(response, $"<p>{student.Name} {student.Surname} - {student.Group}</p>");
            }
        }
    }

    static void HandlePost(HttpListenerRequest request, HttpListenerResponse response)
    {
        using var reader = new StreamReader(request.InputStream);
        string body = reader.ReadToEnd();

        var student = JsonSerializer.Deserialize<Student>(body);

        if (student != null)
        {
            student.Id = students.Max(s => s.Id) + 1;
            students.Add(student);

            Write(response, $"Added student with ID {student.Id}");
        }
    }

    static void HandlePut(HttpListenerRequest request, HttpListenerResponse response)
    {
        var parts = request.Url.AbsolutePath.Split('/');

        if (parts.Length == 3 && int.TryParse(parts[2], out int id))
        {
            using var reader = new StreamReader(request.InputStream);
            string body = reader.ReadToEnd();

            var updated = JsonSerializer.Deserialize<Student>(body);

            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Write(response, "Student not found");
                return;
            }

            student.Name = updated.Name;
            student.Surname = updated.Surname;
            student.Group = updated.Group;

            Write(response, "Student updated");
        }
    }

    static void Write(HttpListenerResponse response, string content)
    {
        byte[] data = Encoding.UTF8.GetBytes(content);
        response.ContentType = "text/html";
        response.OutputStream.Write(data, 0, data.Length);
    }
}

class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Group { get; set; }
}
