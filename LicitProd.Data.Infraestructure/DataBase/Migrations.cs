using System;
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
}