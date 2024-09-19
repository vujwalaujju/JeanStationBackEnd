using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Helpers;
using MongoDbDemo.Models;
using MongoDbDemo.Repositories;

namespace MongoDbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly SignInRepository _repo;

        public SignInController(SignInRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult Validate([FromBody] SignIn value)
        {
            if (value == null)
            {
                return BadRequest("Invalid request");
            }

            var user = _repo.ValidateUser(value);
            if (user == null)
            {
                return NotFound("New user, please sign up first");
            }

            return Ok(new TokenResult { Status = "success", Token = new TokenHelper().GenerateToken(user) });
        }
    }
}
