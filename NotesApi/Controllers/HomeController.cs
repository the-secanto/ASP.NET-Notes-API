using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Controllers;

[ApiController]
[Route("")] // The root (/) directory
public class HomeController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "This is the Notes API home page";
    }
}
