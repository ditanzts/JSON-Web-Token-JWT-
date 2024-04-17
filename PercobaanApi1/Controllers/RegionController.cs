using PercobaanApi1.Entities;
using PercobaanApi1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace PercobaanApi1.Controllers
{
    [ApiController]
    [Route("api/regions/")]
    public class RegionController : Controller
    {
        private string credentials;
        private RegionService regionService;
        public RegionController(IConfiguration configuration)
        {
            this.credentials = configuration.GetConnectionString("WebApiDatabase");
            this.regionService = new RegionService(new Repositories.RegionRepository(new Utils.DbUtil(this.credentials)));
        }
        [HttpGet, Authorize]
        public ActionResult<Region> findAll()
        {
            return Ok(this.regionService.findAll());
        }
        [HttpGet("{id}"), Authorize]
        public ActionResult<Region> findById(int region_id)
        {
            if (this.regionService.findById(region_id) == null)
            {
                return NotFound();
            }
            return Ok(this.regionService.findById(region_id));
        }
        [HttpPost, Authorize]
        public ActionResult<Region> create(Region entity)
        {
            return Ok(this.regionService.create(entity));
        }
        [HttpPut("{id}"), Authorize]
        public ActionResult<Region> update(int region_id, Region entity)
        {

            Region region = this.regionService.findById(region_id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(this.regionService.update(region_id, entity));
        }
        [HttpDelete("{id}"), Authorize]
        public ActionResult<Region> delete(int region_id)
        {
            Region region = this.regionService.findById(region_id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(this.regionService.delete(region_id));
        }
    }
}