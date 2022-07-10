using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_API.Model
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : Controller
    {

        IAdvertisementRepository repoAd;
        IUserRepository repoUser;
        public AdvertisementController(IAdvertisementRepository _repoAd, IUserRepository _repoUser)
        {
            repoAd = _repoAd;
            repoUser = _repoUser;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(repoAd.GetAll());
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
                Advertisement ad = repoAd.Get(id);
                if (ad == null)
                    return NotFound();
                return Ok(ad);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(Advertisement ad, int idAdmin)
        {
            try
            {
                if (!repoUser.isAdmin(idAdmin))
                    return StatusCode(403);

                if (repoAd.Create(ad))
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
        public IActionResult Edit(Advertisement ad, int idAdmin)
        {
            try
            {
                if (!repoUser.isAdmin(idAdmin))
                    return StatusCode(403);
                if (repoAd.Update(ad))
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
                if (repoAd.Get(id) == null)
                    return NotFound();

                if (repoAd.Delete(id))
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