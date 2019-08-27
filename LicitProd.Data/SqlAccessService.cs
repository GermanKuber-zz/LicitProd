using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LicitProd.Data
{
    public class SqlAccessService
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LicitProd;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DataTable SelectData(string table, List<Parameter> parameters = null, List<string> selectColumns = null)
        {
            string query = "SELECT ";

            if (selectColumns != null)
                query = string.Concat(query, string.Join(",", selectColumns), " ");
            else
                query = string.Concat(query, "*", " ");

            query = string.Concat(query, $"FROM {table}", " ");

            if (parameters != null)
            {
                query = string.Concat(query, " WHERE ", string.Join(" AND ", parameters.Select(value =>
                {
                    return $" {value.ColumnName}=@{value.ColumnName}";
                }).ToList()));
            }

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            if (parameters != null)
                command.Parameters.AddRange(parameters.Select(parameter =>
                {
                    return new SqlParameter($"@{parameter.ColumnName}", parameter.Value);
                }).ToArray());
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();

            return dataTable;

        }
        public void InsertData(string table, List<Parameter> parameters) =>
            ExcecuteQuery(table, $"INSERT INTO dbo.{table} ({string.Join(",", parameters.Select(x=> x.ColumnName).ToList())}) " +
                           $"VALUES ({string.Join(",", parameters.Select(key => $"@{key.ColumnName}").ToList())}) ", parameters);

        public void UpdateData(string table, List<Parameter> parameters, List<Parameter> where = default)
        {
            string query = $"UPDATE  dbo.{table} SET {string.Join(",", parameters.Select(value => $"{value.ColumnName} = @{value.ColumnName}").ToList())}";

            if (where != null)
                query = string.Concat(query, " WHERE ", string.Join(" AND ", where.Select(x => $"{x.ColumnName}=@{x.ColumnName}")));
            parameters.AddRange(where);
            ExcecuteQuery(table, query, parameters);
        }
        private void ExcecuteQuery(string table, string query, List<Parameter> parameters)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddRange(parameters.Select(parameter =>
                {
                    return new SqlParameter($"@{parameter.ColumnName}", parameter.Value)
                    {
                        SqlDbType = parameter.Type
                    };
                }).ToArray());

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
    public class Parameter
    {
        public string ColumnName { get; }
        public string Value { get; }
        public SqlDbType Type { get; }

        public Parameter(string columnName, string value, SqlDbType type)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Type = type;
        }
    }
    public class Column {
        public string Name { get;  }

        public Column(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
