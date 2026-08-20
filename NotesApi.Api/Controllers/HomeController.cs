using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "This is the Notes API home page";
    }
}