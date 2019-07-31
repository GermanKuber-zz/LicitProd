using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LicitProd.Data
{
    public class SqlAccessService
    {
        string connectionString = "Data Source=DESKTOP-L51S99M;Initial Catalog=LicitProd;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DataTable SelectData(string table, Dictionary<string, string> parameters = null, List<string> selectColumns = null)
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
                    return $" {value.Key}=@{value.Key}";
                }).ToList()));
            }

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            if (parameters != null)
                command.Parameters.AddRange(parameters.Select(parameter =>
                {
                    return new SqlParameter($"@{parameter.Key}", parameter.Value);
                }).ToArray());
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();

            return dataTable;

        }
        public void InsertData(string table, Dictionary<string, string> parameters) =>
            ExcecuteQuery(table, $"INSERT INTO dbo.{table} ({string.Join(",", parameters.Keys.ToList())}) " +
                           $"VALUES ({string.Join(",", parameters.Keys.Select(key => $"@{key}").ToList())}) ", parameters);

        public void UpdateData(string table, Dictionary<string, string> parameters, Dictionary<string, string> where = default)
        {
            string query = $"UPDATE  dbo.{table} SET {string.Join(",", parameters.Select(value => $"{value.Key} = @{value.Key}").ToList())}";

            if (where != null)
                query = string.Concat(query, " WHERE ", string.Join(" AND ", where.Select(x => $"{x.Key}=@{x.Key}")));

            ExcecuteQuery(table, query, parameters.Union(where).ToDictionary(k => k.Key, v => v.Value));
        }
        private void ExcecuteQuery(string table, string query, Dictionary<string, string> parameters)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddRange(parameters.Select(parameter =>
                {
                    return new SqlParameter($"@{parameter.Key}", parameter.Value);
                }).ToArray());

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
}
