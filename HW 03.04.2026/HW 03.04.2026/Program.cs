using ApiClientApp.Models;
using ApiClientApp.Services;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== API CLIENT TEST ===");

        // GET ALL
        var articles = await ArticleService.GetAll();
        Console.WriteLine($"Loaded articles: {articles.Count}");

        // GET BY ID
        var article = await ArticleService.GetById(1);
        Console.WriteLine($"Article 1: {article?.Title}");

        // SEARCH
        var search = await ArticleService.Search("qui");
        Console.WriteLine($"Search results: {search.Count}");

        // ADD
        var newArticle = new Article
        {
            Title = "My Test Article",
            Description = "Hello World",
            Author = "Me",
            Date = DateTime.Now
        };

        int? newId = await ArticleService.Add(newArticle);
        Console.WriteLine($"Created ID: {newId}");

        // PATCH
        if (newId != null)
        {
            bool updated = await ArticleService.Patch(newId.Value, "Updated Title");
            Console.WriteLine($"Updated: {updated}");
        }

        // DELETE
        if (newId != null)
        {
            bool deleted = await ArticleService.Delete(newId.Value);
            Console.WriteLine($"Deleted: {deleted}");
        }

        Console.WriteLine("=== DONE ===");
    }
}