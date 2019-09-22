using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LicitProd.Data.Infraestructure.DataBase
{
    public class DataBaseManager
    {
        private string _connectionString;
        public DataBaseManager()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LictProd"].ConnectionString;

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
