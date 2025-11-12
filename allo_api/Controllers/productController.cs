using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class productController : ControllerBase
  {

    [HttpGet]
    public IActionResult Index()
    {
      return Ok("Hello World");
    }

    // /product/23

    [HttpGet("product/{productId}")]
    public IActionResult getProduct(int productId)
    {
      if (productId <= 0)
      {
        return BadRequest("value sent is 0.");
      }

      return Ok($"prodId received : {productId}");
    }

    [HttpPost("insert")]
    public IActionResult insertProd(UserLoginCred userLoginCred)
    {
      if (userLoginCred.Password == "")
        return BadRequest("Password Is Empty");

      if (userLoginCred.UserName == "")
        return BadRequest("UserName Is Empty");


      return Ok($"Username : {userLoginCred.UserName} :: {userLoginCred.Password}");
    }

    [HttpDelete]
    public IActionResult deleteProd(int productId)
    {
      // validation for data passed to endpoint

      // delete record from database tables


      return Ok();
    }

    [HttpPut]
    public IActionResult updateProd(int productId)
    {
      return Ok();
    }

  }
}

public record UserLoginCred
{
  public string UserName { get; set; } = null!;
  public string Password { get; set; } = null!;
}