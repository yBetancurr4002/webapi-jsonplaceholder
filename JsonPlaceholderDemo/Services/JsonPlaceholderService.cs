using System.Net.Http.Json;
using JsonPlaceholderDemo.Models;

namespace JsonPlaceholderDemo.Services;

public class JsonPlaceholderService : IJsonPlaceholderService
{
    private readonly HttpClient _httpClient;

    public JsonPlaceholderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        var posts = await _httpClient.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");
        return posts ?? new List<Post>();
    }
}
