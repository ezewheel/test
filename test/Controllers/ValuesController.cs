using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var post = await _postService.GetPostAsync(id);
        if (post == null) return NotFound();
        return Ok(post);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _postService.GetAllPostsAsync();
        if (posts == null || posts.Count == 0) return NotFound();
        return Ok(posts);
    }
}
