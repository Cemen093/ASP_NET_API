using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_API.Model
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : Controller
    {

        IRagionRepository repoReg;
        IUserRepository repoUser;
        public RegionController(IRagionRepository _repoReg, IUserRepository _repoUser)
        {
            repoReg = _repoReg;
            repoUser = _repoUser;
        }
        [HttpGet("init")]
        public IActionResult init()
        {
            if (repoReg.Init())
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(repoReg.GetAll());
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
                Region reg = repoReg.Get(id);
                if (reg == null)
                    return NotFound();
                else
                    return Ok(reg);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(Region reg, int idAdmin)
        {
            try
            {
                if (!repoUser.isAdmin(idAdmin))
                    return StatusCode(403);

                if (repoReg.Create(reg))
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
        public IActionResult Edit(Region reg, int idAdmin)
        {
            try
            {
                if (!repoUser.isAdmin(idAdmin))
                    return StatusCode(403);

                if (repoReg.Get(reg.id) == null)
                    return NotFound();

                if (repoReg.Update(reg))
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
                if (repoReg.Get(id) == null)
                    return NotFound();

                if (repoReg.Delete(id))
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