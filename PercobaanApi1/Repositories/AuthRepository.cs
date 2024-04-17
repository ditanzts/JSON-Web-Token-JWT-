using PercobaanApi1.Utils;
using PercobaanApi1.Entities;
using Npgsql;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PercobaanApi1.DTOs;

namespace PercobaanApi1.Repositories
{
    public class AuthRepository
    {
        private DbUtil dbUtil;
        public AuthRepository(DbUtil dbUtil)
        {
            this.dbUtil = dbUtil;
        }

        public User register(User user)
        {
            string query = string.Format(@"INSERT INTO person (nama, alamat, email, password) VALUES (@nama, @alamat, @email, @password);");
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("nama", user.nama);
                cmd.Parameters.AddWithValue("alamat", user.alamat);
                cmd.Parameters.AddWithValue("email", user.email);
                cmd.Parameters.AddWithValue("password", user.password);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
                return user;
            }
            catch (NpgsqlException ex)
            {
                dbUtil.closeConnection();
                throw new NpgsqlException(ex.Message);
            }
            return null;
        }

        public User Login(User user, IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(user.email) || string.IsNullOrEmpty(user.password))
            {
                throw new ArgumentNullException("email or password cannot be null or empty.");
            }
            string query = "SELECT * FROM person WHERE email = @email AND password = @password;";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("email", user.email);
                cmd.Parameters.AddWithValue("password", user.password);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user.id_person = int.Parse(reader["id_person"].ToString());
                    user.nama = reader["nama"].ToString();
                    user.alamat = reader["alamat"].ToString();
                    user.email = reader["email"].ToString();
                    user.password = reader["password"].ToString();
                    user.token = GenereJwtToken(user, configuration);
                    cmd.Dispose();
                    dbUtil.closeConnection();
                    return user;
                }
                cmd.Dispose();
                dbUtil.closeConnection();
            }
            catch (Exception ex)
            {
                dbUtil.closeConnection();
                throw new NpgsqlException(ex.Message);
            }
            return user;
        }



        private string GenereJwtToken(User user, IConfiguration pConfig)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(pConfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.nama),
                new Claim(ClaimTypes.Email, user.email),
            };
            var token = new JwtSecurityToken(pConfig["Jwt:Issuer"],
                pConfig["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}