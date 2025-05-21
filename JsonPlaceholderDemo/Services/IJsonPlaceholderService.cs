using JsonPlaceholderDemo.Models;

namespace JsonPlaceholderDemo.Services;

public interface IJsonPlaceholderService
{
    Task<IEnumerable<Post>> GetPostsAsync();
}
