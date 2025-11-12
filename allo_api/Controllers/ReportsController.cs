using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
  [HttpGet]
  public IActionResult Index()
  {
    return Ok("Hello World");
  }
}