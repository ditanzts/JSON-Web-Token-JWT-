using Npgsql;
using PercobaanApi1.Entities;
using PercobaanApi1.Utils;

namespace PercobaanApi1.Repositories
{
    public class CountryRepository
    {
        private DbUtil dbUtil;
        public CountryRepository(DbUtil dbUtil)
        {
            this.dbUtil = dbUtil;
        }

        public List<Country> findAll()
        {
            List<Country> countries = new List<Country>();
            string sql = "SELECT * FROM countries";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Country country = new Country();
                    country.id = reader.GetInt32(0);
                    country.name = reader.GetString(1);
                    countries.Add(country);
                }
                cmd.Dispose();
                dbUtil.closeConnection();
            }
            catch (Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return countries;
        }

        public Country findById(int id)
        {
            string sql = "SELECT * FROM countries WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Country country = new Country();
                    country.id = reader.GetInt32(0);
                    country.name = reader.GetString(1);
                    cmd.Dispose();
                    dbUtil.closeConnection();
                    return country;
                }
                cmd.Dispose();
                dbUtil.closeConnection();
            }
            catch (Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }

        public Country create(Country entity)
        {
            string sql = "INSERT INTO countries (name) VALUES (@name) RETURNING id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@name", entity.name);
                entity.id = (int)cmd.ExecuteScalar();
                cmd.Dispose();
                dbUtil.closeConnection();
                return entity;
            }
            catch (Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }

        public Country update(Country entity)
        {
            string sql = "UPDATE countries SET name = @name WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@name", entity.name);
                cmd.Parameters.AddWithValue("@id", entity.id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
                return entity;
            }
            catch (Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }

        public Country delete(Country entity)
        {
            string sql = "DELETE FROM countries WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@id", entity.id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
                return entity;
            }
            catch (Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }
    }
}