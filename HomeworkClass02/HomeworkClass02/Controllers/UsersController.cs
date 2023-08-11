using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkClass02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> GetAll()
        {
            return Ok(StaticDb.Users);
        }

        [HttpGet("{index}")]
        public ActionResult<string> GetOneUser(int index) 
        {
            try
            {
                if(index < 0)
                {
                   return BadRequest("The index should be a positive number");
                }
                if( index >=  StaticDb.Users.Count)
                {
                   return NotFound($"There is no user on index {index}");
                }

                return Ok(StaticDb.Users[index]);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

    }
}
