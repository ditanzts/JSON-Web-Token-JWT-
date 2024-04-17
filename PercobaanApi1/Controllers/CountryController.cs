using PercobaanApi1.Entities;
using PercobaanApi1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace PercobaanApi1.Controllers
{
    [ApiController]
    [Route("api/countries/")]
    public class CountryController : Controller
    {
        private string credentials;
        private CountryService countryService;
        public CountryController(IConfiguration configuration)
        {
            this.credentials = configuration.GetConnectionString("WebApiDatabase");
            this.countryService = new CountryService(new Repositories.CountryRepository(new Utils.DbUtil(this.credentials)));
        }
        [HttpGet, Authorize]
        public ActionResult<Country> findAll()
        {
            return Ok(this.countryService.findAll());
        }
        [HttpGet("{id}"), Authorize]
        public ActionResult<Country> findById(int id)
        {
            if (this.countryService.findById(id) == null)
            {
                return NotFound();
            }
            return Ok(this.countryService.findById(id));
        }
        [HttpPost, Authorize]
        public ActionResult<Country> create(Country entity)
        {
            return Ok(this.countryService.create(entity));
        }
        [HttpPut("{id}"), Authorize]
        public ActionResult<Country> update(int id, Country entity)
        {

            Country country = this.countryService.findById(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(this.countryService.update(id, entity));
        }
        [HttpDelete("{id}"), Authorize]
        public ActionResult<Country> delete(int id)
        {
            Country country = this.countryService.findById(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(this.countryService.delete(id));
        }
    }
}