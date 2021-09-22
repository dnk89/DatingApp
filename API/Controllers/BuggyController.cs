using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly IUsersRepository _repo;

        public BuggyController(IUsersRepository repo)
        {
            _repo = repo;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public async Task<ActionResult<UserDto>> GetNotFoundUser()
        {
            var usr = await _repo.GetUser(-1);

            if (usr == null) return NotFound();
            
            return Ok(usr);
        }

        [HttpGet("server-error")]
        public async Task<ActionResult<string>> GetServerError()
        {
            var usr = await _repo.GetUser(-1);

            return usr.ToString();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request!");
        }
    }
}