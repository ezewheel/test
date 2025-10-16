using System.Net.Http.Json;
public class PostService : IPostService
{
    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Post?> GetPostAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Post>($"posts/{id}");
    }

    public async Task<List<Post>?> GetAllPostsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Post>>("posts");
    }
}

public class Post
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
