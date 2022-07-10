using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_API.Model
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadingsController : Controller
    {

        IHeadingRepository repo;
        IUserRepository repoUser;
        public HeadingsController(IHeadingRepository _repo, IUserRepository _repoUser)
        {
            repo = _repo;
            repoUser = _repoUser;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
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
                Heading el = repo.Get(id);
                if (el == null)
                    return NotFound();
                else
                    return Ok(el);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(Heading el, int idAdmin)
        {
            try
            {
                if (!repoUser.isAdmin(idAdmin))
                    return StatusCode(403);

                if (repo.Create(el))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Edit(Heading el, int idAdmin)
        {
            try
            {
                if (!repoUser.isAdmin(idAdmin))
                    return StatusCode(403);

                if (repo.Get(el.id) == null)
                    return NotFound();

                if (repo.Update(el))
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
                if (!repoUser.isAdmin(idAdmin))
                    return StatusCode(403);
                if (repo.Get(id) == null)
                    return NotFound();

                if (repo.Delete(id))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}