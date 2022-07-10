using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_API.Model
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        IUserRepository repo;
        public UserController(IUserRepository r)
        {
            repo = r;
        }

        [HttpGet("isAdmin")]
        public IActionResult IsAdmin(int id)
        {
            try
            {
                User user = repo.Get(id);
                if (user == null)
                    return NotFound();

                return Ok(new { IsAdmin = user.isAdmin });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("all")]
        public IActionResult GetAll(int idAdmin)
        {
            try
            {
                if (!repo.isAdmin(idAdmin))
                    return StatusCode(403);
                else
                    return Ok(repo.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                User user = repo.Get(id);
                if (user == null)
                    return NotFound();
                else
                    return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{email}/{password}")]
        public IActionResult Get(string email, string password)
        {
            try
            {
                User user = repo.Get(email, Hash.Hashing(password));
                if (user == null)
                    return NotFound();
                else
                    return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(string email, string password, string passwordRepeat, string? key)
        {
            try
            {
                User user = new User();
                if (password != passwordRepeat || repo.Get(email) != null)
                    return BadRequest();

                user.email = email;
                user.hashPassword = Hash.Hashing(password);
                if (key != "qwerty123")
                    user.isAdmin = false;
                else
                    user.isAdmin = true;
                if (repo.Create(user))
                    return Created(new Uri($"User/{user.id}", UriKind.Relative), repo.Get(email, Hash.Hashing(password)));
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("admin")]
        public IActionResult Create(User user, int idAdmin)
        {
            try
            {
                if (!repo.isAdmin(idAdmin))
                    return StatusCode(403);
                if (repo.Get(user.email) != null)
                    return BadRequest();
                if (repo.Create(user))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            /*return Created(new Uri($"User/{user.id}", UriKind.Relative), repo.Get(email, Hash.Hashing(password)));*/
        }

        [HttpPut]
        public IActionResult Edit(int id, string email, string password, string passwordRepeat, bool? isAdmin, int? idAdmin)
        {
            try
            {
                if (password != passwordRepeat || repo.Get(email) != null)
                    return BadRequest();
                User user = repo.Get(email, Hash.Hashing(password));
                if (user == null)
                    return NotFound();
                if (isAdmin != null && repo.isAdmin(idAdmin.GetValueOrDefault()))
                    return StatusCode(403);
                else
                    user.isAdmin = isAdmin.GetValueOrDefault();
                user.email = email;
                user.hashPassword = Hash.Hashing(password);

                if (repo.Update(user))
                    return Created(new Uri($"api/User/{user.id}"), repo.Get(email, Hash.Hashing(password)));
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("admin")]
        public IActionResult Edit(User user, int idAdmin)
        {
            try
            {
                if (!repo.isAdmin(idAdmin))
                    return StatusCode(403);
                if (repo.Get(user.email) != null)
                    if (repo.Get(user.id).id != user.id)
                        return BadRequest();
                if (repo.Update(user))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id, int idAdmin)
        {
            try
            {
                if (repo.Get(id) == null || !repo.isAdmin(idAdmin))
                    return BadRequest();

                if (repo.Delete(id))
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}