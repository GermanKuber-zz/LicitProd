﻿using LicitProd.Entities;
using LicitProd.Mappers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LicitProd.Data
{

    public class SqlAccessService<TEntity> where TEntity : IEntityToDb, new()
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LicitProd;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private string _dataTableName;

        public SqlAccessService()
        {
            var objectToDbMapper = ObjectToDbMapperFactory<TEntity>.Create();
            _dataTableName = objectToDbMapper.TableName;
        }
        public DataTable SelectData(List<string> selectColumns) =>
            SelectData(null, selectColumns);
        public DataTable SelectData(List<Parameter> parameters) =>
            SelectData(parameters, null);

        public DataTable SelectData(string query)
        {            
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();
            return dataTable;
        }

        public DataTable SelectData(List<Parameter> parameters, List<string> selectColumns)
        {
            string query = "SELECT ";

            if (selectColumns != null)
                query = string.Concat(query, string.Join(",", selectColumns), " ");
            else
                query = string.Concat(query, "*", " ");

            query = string.Concat(query, $"FROM {_dataTableName}", " ");

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
        public void InsertData(List<Parameter> parameters) =>
            ExcecuteQuery($"INSERT INTO dbo.{_dataTableName} ({string.Join(",", parameters.Select(x => x.ColumnName).ToList())}) " +
                           $"VALUES ({string.Join(",", parameters.Select(key => $"@{key.ColumnName}").ToList())}) ", parameters);

        public void UpdateData(List<Parameter> parameters, List<Parameter> where = default)
        {
            string query = $"UPDATE  dbo.{_dataTableName} SET {string.Join(",", parameters.Select(value => $"{value.ColumnName} = @{value.ColumnName}").ToList())}";

            if (where != null)
                query = string.Concat(query, " WHERE ", string.Join(" AND ", where.Select(x => $"{x.ColumnName}=@{x.ColumnName}")));
            parameters.AddRange(where);
            ExcecuteQuery(query, parameters);
        }
        private void ExcecuteQuery(string query, List<Parameter> parameters)
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
}
