# webapi-jsonplaceholder
Project Built for academic purposes with dotnet

# WebApi - JSONPlaceholder

## 📦 Estructura del proyecto

```css
JsonPlaceholderDemo/
│
├── Controllers/
│   └── PostsController.cs
│
├── Models/
│   └── Post.cs
│
├── Services/
│   ├── IJsonPlaceholderService.cs
│   └── JsonPlaceholderService.cs
│
└── Program.cs
```

# Primera fase

## **Pasos para implementarlo**

1. Crear el proyecto
    
    ```bash
    dotnet new webapi -n JsonPlaceholderDemo
    cd JsonPlaceholderDemo
    ```
    
2. Crear el modelo `Post.cs`
    
    ```csharp
    namespace JsonPlaceholderDemo.Models;
    
    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
    ```
    
3. Crear el servicio `JsonPlaceholderService`
    
    ```csharp
    using JsonPlaceholderDemo.Models;
    
    namespace JsonPlaceholderDemo.Services;
    
    public interface IJsonPlaceholderService
    {
        Task<IEnumerable<Post>> GetPostsAsync();
    }
    
    ```
    
    Implementación:
    
    ```csharp
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
    
    ```
    
4. Registrar el servicio en `Program.cs`
    
    ```csharp
    builder.Services.AddHttpClient<IJsonPlaceholderService, JsonPlaceholderService>();
    ```
    
5. Crear el controlador `PostsController`
    
    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using JsonPlaceholderDemo.Services;
    using JsonPlaceholderDemo.Models;
    
    namespace JsonPlaceholderDemo.Controllers;
    
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IJsonPlaceholderService _service;
    
        public PostsController(IJsonPlaceholderService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            var posts = await _service.GetPostsAsync();
            return Ok(posts);
        }
    }
    
    ```
    
6. Probar la API
    1. Ejecuta el proyecto: `dotnet run`
    2. navega:  [`https://localhost:{PORT}/api/posts`](https://localhost:{PORT}/api/posts)
