using Npgsql;

namespace PercobaanApi1.Utils
{
    public class DbUtil
    {
        private NpgsqlConnection connection;
        private string credentials;

        public DbUtil(string credentials)
        {
            this.credentials = credentials;
            this.connection = new NpgsqlConnection();
            this.connection.ConnectionString = credentials;
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = this.connection;
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        public void closeConnection()
        {
            this.connection.Close();
        }

    }
}