using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

class ApiService
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GetAllProductsAsync()
    {
        string url = "https://fakestoreapi.com/products";
        return await client.GetStringAsync(url);
    }

    public async Task<string> GetProductByIdAsync(int id)
    {
        string url = $"https://fakestoreapi.com/products/{id}";
        return await client.GetStringAsync(url);
    }
}

class Program
{
    static async Task Main()
    {
        ApiService api = new ApiService();

        Console.WriteLine("1 - Get all products");
        Console.WriteLine("2 - Get product by ID");
        Console.Write("Choose option: ");

        string choice = Console.ReadLine();

        Task<string> apiTask = null;

        if (choice == "1")
        {
            apiTask = api.GetAllProductsAsync();
        }
        else if (choice == "2")
        {
            Console.Write("Enter product ID: ");
            int id = int.Parse(Console.ReadLine());
            apiTask = api.GetProductByIdAsync(id);
        }
        else
        {
            Console.WriteLine("Invalid option.");
            return;
        }

        while (!apiTask.IsCompleted)
        {
            Console.Write("*");
            Thread.Sleep(200);
        }

        Console.WriteLine("\n\nResult:\n");

        string result = await apiTask;
        Console.WriteLine(result);
    }
}