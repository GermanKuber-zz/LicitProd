using LicitProd.Infrastructure;
using System;
using System.Data.SqlClient;

namespace LicitProd.Data.Infrastructure.DataBase
{
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
