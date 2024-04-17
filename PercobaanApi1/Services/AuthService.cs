
using PercobaanApi1.DTOs;
using PercobaanApi1.Entities;
using PercobaanApi1.Repositories;

namespace PercobaanApi1.Service
{
    public class AuthService
    {

        private AuthRepository authRepository;
        public AuthService(AuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public User Register(RegisterDTO dto)
        {
            User user = new User();
            user.nama = dto.nama;
            user.alamat = dto.alamat;
            user.email = dto.email;
            user.password = dto.password;
            return this.authRepository.register(user);
        }

        public User Login(LoginDTO dto, IConfiguration configuration)
        {
            User user = new User();
            user.email = dto.email;
            user.password = dto.password;
            if (this.authRepository.Login(user, configuration) == null)
            {
                return null;
            }
            return this.authRepository.Login(user, configuration);
        }

    }
}