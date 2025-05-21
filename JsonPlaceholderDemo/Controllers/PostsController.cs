using Microsoft.AspNetCore.Mvc;
using JsonPlaceholderDemo.Services;
using JsonPlaceholderDemo.Models;
using JsonPlaceholderDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace JsonPlaceholderDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IJsonPlaceholderService _service;
    private readonly TareasDbContext _context;

    public PostsController(IJsonPlaceholderService service, TareasDbContext context)
    {
        _service = service;
        _context = context;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> Get()
    {
        var posts = await _service.GetPostsAsync();
        return Ok(posts);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportFromApi()
    {
        var posts = await _service.GetPostsAsync();

        var nuevosPosts = posts
            .Where(p => !_context.Posts.Any(dbPost => dbPost.Title == p.Title && dbPost.Body == p.Body))
            .Select(p => new Post
            {
                // Id = p.Id, // NO incluir el Id
                Title = p.Title,
                Body = p.Body,
                UserId = p.UserId
            }).ToList();

        _context.Posts.AddRange(nuevosPosts);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("dbposts")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _context.Posts.ToListAsync();
        return Ok(posts);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> AddPost([FromBody] Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        return post == null ? NotFound() : Ok(post);
    }



}
