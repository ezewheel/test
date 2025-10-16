public interface IPostService
{
    Task<Post?> GetPostAsync(int id);
    Task<List<Post>?> GetAllPostsAsync();
}
