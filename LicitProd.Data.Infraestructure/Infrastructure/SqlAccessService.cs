using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LicitProd.Data.Infrastructure.Objects;
using LicitProd.Entities;
using LicitProd.Infrastructure;

namespace LicitProd.Data.Infrastructure.Infrastructure
{

    public class SqlAccessService<TEntity> where TEntity : IEntityToDb, new()
    {
        private readonly string _connectionString;
        private readonly IObjectToDbMapper<TEntity> _objectToDbMapper;
        private readonly string _dataTableName;

        public SqlAccessService()
        {
            _connectionString = ConfigurationManagerKeys.Configuration().ConnectionString;
            _objectToDbMapper = ObjectToDbMapperFactory<TEntity>.Create();
            _dataTableName = _objectToDbMapper.TableName;
        }
        public DataTable SelectData(List<string> selectColumns) =>
            SelectData(null, selectColumns);
        public DataTable SelectData(List<Parameter> parameters) =>
            SelectData(parameters, null);

        public DataTable SelectData(string query)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
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

            SqlConnection conn = new SqlConnection(_connectionString);
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
        public int InsertData(List<Parameter> parameters)
        {
            return ExecuteScalar($"INSERT INTO dbo.{_dataTableName} ({string.Join(",", parameters.Select(x => x.ColumnName).ToList())}) " +
                          $"VALUES ({string.Join(",", parameters.Select(key => $"@{key.ColumnName}").ToList())}) ; SELECT SCOPE_IDENTITY()", parameters);
        }
        public int InsertData(TEntity entity)
        {
            var id = InsertData(_objectToDbMapper.GetParameters(entity).Send());
            entity.Id = id;
            return id;
        }
        public int InsertData(TEntity entity, Parameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            var list = _objectToDbMapper.GetParameters(entity).Send();
            list.AddRange(parameters.Send());
            return InsertData(list);
        }
        public void UpdateData(List<Parameter> parameters, List<Parameter> where = default)
        {
            string query = $"UPDATE  dbo.{_dataTableName} SET {string.Join(",", parameters.Select(value => $"{value.ColumnName} = @{value.ColumnName}").ToList())}";

            if (where != null)
                query = string.Concat(query, " WHERE ", string.Join(" AND ", where.Select(x => $"{x.ColumnName}=@{x.ColumnName}")));
            query = string.Concat(query, "; SELECT SCOPE_IDENTITY()");
            parameters.AddRange(where);
            ExcecuteQuery(query, parameters);
        }

        private int ExecuteScalar(string query, List<Parameter> parameters)
        {
            var identity = 0;
            ExecuteCommand(query, parameters, cmd =>
            {
                identity = decimal.ToInt32((decimal)cmd.ExecuteScalar());
            });
            return identity;
        }

        private void ExecuteCommand(string query, List<Parameter> parameters, Action<SqlCommand> executeFunction)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
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
                executeFunction(cmd);
                cn.Close();
            }

        }

        public void ExcecuteQuery(string query, List<Parameter> parameters) =>
            ExecuteCommand(query, parameters, cmd => cmd.ExecuteNonQuery());
    }
}
