using LicitProd.Infrastructure;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace LicitProd.Data.Infrastructure.DataBase
{
    public  class Migrations
    {
        private DataBaseManager _dataBaseManager = new DataBaseManager();
        public Migrations()
        {
        }

        public void Run()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);

            string creationSqlScript = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Creation.sql");
            string script = string.Join(" ",File.ReadAllText(creationSqlScript));

            _dataBaseManager.CallDataBase(cmd =>
            {
                cmd.CommandText = script;
                return cmd.ExecuteNonQuery();
            });
        }
    }

    public class DataBaseManager
    {
        private string _connectionString;
        public DataBaseManager()
        {
            _connectionString = ConfigurationManagerKeys.Configuration().ConnectionString;

        }
        public TReturn CallDataBase<TReturn>(Func<SqlCommand, TReturn> call)
        {
            TReturn returnValue = default;
            string connectionString = CreateConnectionstring("");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                returnValue = call(cmd);
            }
            return returnValue;
        }

        private string CreateConnectionstring(string databaseName) => string.Format(_connectionString, databaseName);

    }
}
