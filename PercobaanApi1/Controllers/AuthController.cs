using Microsoft.AspNetCore.Mvc;
using PercobaanApi1.DTOs;
using PercobaanApi1.Entities;
using PercobaanApi1.Service;

namespace PercobaanApi1.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private string credentials;
        private IConfiguration configuration;
        private AuthService authService;
        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.credentials = configuration.GetConnectionString("WebApiDatabase");
            this.authService = new AuthService(new Repositories.AuthRepository(new Utils.DbUtil(this.credentials)));
        }

        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] RegisterDTO dto)
        {
            User user = new User();
            user.nama = dto.nama;
            user.alamat = dto.alamat;
            user.email = dto.email;
            user.password = dto.password;
            User registeredUser = this.authService.Register(dto);
            if (registeredUser == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(registeredUser);
            }


        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginDTO dto)
        {
            User user = new User();
            user.email = dto.email;
            user.password = dto.password;
            User LoggedIn = this.authService.Login(dto, configuration);
            if (LoggedIn == null)
            {
                return BadRequest();
            }
            return Ok(this.authService.Login(dto, configuration));
        }


    }
}