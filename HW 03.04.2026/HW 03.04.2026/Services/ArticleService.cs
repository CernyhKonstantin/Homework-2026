using System.Text.Json;
using ApiClientApp.Models;

namespace ApiClientApp.Services
{
    public static class ArticleService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _URL = "https://jsonplaceholder.typicode.com/";

        public static async Task<List<Article>> GetAll()
        {
            var response = await _httpClient.GetAsync($"{_URL}posts");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"HTTP Error: {response.StatusCode}");

            var raw = JsonSerializer.Deserialize<List<PostDto>>(content);

            var result = new List<Article>();

            foreach (var item in raw)
            {
                result.Add(new Article
                {
                    Id = item.id,
                    Title = item.title,
                    Description = item.body,
                    Author = $"User {item.userId}",
                    Date = DateTime.Now
                });
            }

            return result;
        }

        public static async Task<Article?> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"{_URL}posts/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            var item = JsonSerializer.Deserialize<PostDto>(content);

            return new Article
            {
                Id = item.id,
                Title = item.title,
                Description = item.body,
                Author = $"User {item.userId}",
                Date = DateTime.Now
            };
        }

        public static async Task<List<Article>> Search(string keyword)
        {
            var all = await GetAll();

            var result = new List<Article>();

            foreach (var article in all)
            {
                if (article.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(article);
                }
            }

            return result;
        }

        public static async Task<int?> Add(Article article)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = article.Title,
                body = article.Description,
                userId = 1
            });

            var response = await _httpClient.PostAsync(
                $"{_URL}posts",
                new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PostDto>(content);

            return result?.id;
        }

        public static async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_URL}posts/{id}");
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> Patch(int id, string newTitle)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = newTitle
            });

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_URL}posts/{id}")
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        private class PostDto
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public string body { get; set; }
        }
    }
}