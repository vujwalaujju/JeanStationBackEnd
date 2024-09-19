using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Models;
using MongoDbDemo.Repositories;


namespace MongoDbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly SignUpRepository _repo;

        public SignUpController(SignUpRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SignUp value)
        {
            _repo.AddUser(value);
            return CreatedAtAction(nameof(Post), new { id = value.id }, value);
        }

        [HttpGet]
        public ActionResult<List<SignUp>> Get()
        {
            var users = _repo.GetUsers();
            return Ok(users);
        }
    }
}
