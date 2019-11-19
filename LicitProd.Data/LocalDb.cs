using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using LicitProd.Data.Infrastructure.DataBase;
using LicitProd.Infrastructure;

namespace LicitProd.Data
{
    public static class LocalDb
    {
        public const string DbDirectory = "Data";
        private static string _connectionString;

        private static readonly DataBaseManager DataBaseManager = new DataBaseManager();
        public static void CreateLocalDb(string databaseName, List<string> scriptsName, bool deleteIfExists = false)
        {
            _connectionString =  ConfigurationManagerKeys.Configuration().ConnectionString;

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            // return Path.GetDirectoryName(path);

            string outputFolder = Path.Combine(Path.GetDirectoryName(path), DbDirectory);
            string mdfFilename = databaseName + ".mdf";
            string databaseFileName = Path.Combine(outputFolder, mdfFilename);

            // Create Data Directory If It Doesn't Already Exist.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            if (!CheckDatabaseExists(databaseName))
            {
                ExecuteScript(databaseName, scriptsName);
            }
        }

        private static void ExecuteScript(string databaseName, List<string> scriptsName)
        {
            string connectionString = CreateConnectionstring(databaseName);

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);

            string outputFolder = Path.Combine(Path.GetDirectoryName(path), DbDirectory);
            foreach (var scriptName in scriptsName)
            {
                string scriptPath = Path.Combine(outputFolder, scriptName);
                var file = new FileInfo(scriptPath);
                string script = file.OpenText().ReadToEnd().Replace("LicitProd", databaseName);

                string[] commands = script.Split(new[] { "GO\r\n", "GO ", "GO\t" }, StringSplitOptions.RemoveEmptyEntries);

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (string c in commands)
                    {
                        var command = new SqlCommand(c, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private static string CreateConnectionstring(string databaseName) => string.Format(_connectionString, databaseName);

        private static bool CheckDatabaseExists(string databaseName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = string.Format("SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = '{0}' OR name = '{1}')", databaseName, databaseName);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return true;
                }
            }

            return false;
        }

      
        public static bool DropDatabase(string databaseName)
        {
            return DataBaseManager.CallDataBase(cmd =>
            {
                cmd.CommandText = string.Format("USE master;ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE {0} ;", databaseName);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return true;
                }
                return false;
            });
        }
        private static void DropDatabaseObjects()=>
            DataBaseManager.CallDataBase(cmd =>
                {
                    cmd.CommandText = @"declare @ord int, @cmd varchar(8000)

                    declare objs cursor for
                    select 0, 'drop trigger [' + name + '] on database' from sys.triggers
                    where parent_class = 0 and is_ms_shipped = 0
                    union
                    select 1, 'drop synonym [' + schema_name(schema_id) + '].[' + name + ']' from sys.objects o
                    where o.type = 'SN'
                    union
                    select 2, 'drop procedure [' + schema_name(schema_id) + '].[' + name + ']' from sys.objects o
                    where o.type = 'P'
                    union
                    select 3, 'drop view [' + schema_name(schema_id) + '].[' + name + ']' from sys.objects o
                    where o.type = 'V'
                    union
                    select 4, 'drop function [' + schema_name(schema_id) + '].[' + name + ']' from sys.objects o
                    where o.type IN ('FN','IF', 'TF')
                    union
                    select 5, 'alter table [' + schema_name(schema_id) + '].[' + object_name(parent_object_id) + '] drop constraint [' + name + ']'
                    from sys.objects
                    where type = 'F'
                    union
                    select 6, 'drop table [' + schema_name(schema_id) + '].[' + name + ']' from sys.objects o
                    where o.type = 'U'
                    union
                    select 7, 'drop type [' + schema_name(schema_id) + '].[' + name + ']' from sys.types
                    where is_user_defined = 1
                    union
                    select 8, 'drop default [' + schema_name(schema_id) + '].[' + name + ']' from sys.objects o
                    where o.type = 'D'
                    order by 1

                    open objs
                    fetch next from objs into @ord, @cmd

                    while @@FETCH_STATUS = 0
                    begin
                      print @cmd
                      execute (@cmd)
                      fetch next from objs into @ord, @cmd
                    end

                    close objs
                    deallocate objs";
                    return cmd.ExecuteNonQuery();
                });

        private static void CreateDatabase(string databaseName, string databaseFileName)
        {
            string connectionString = CreateConnectionstring("master").Replace("TestDb","master");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                // TryDetachDatabase(databaseName);
                cmd.CommandText = string.Format("CREATE DATABASE {0} ON (NAME = N'{0}', FILENAME = '{1}')", databaseName, databaseFileName);
                cmd.ExecuteNonQuery();
            }
        }
    }

}
