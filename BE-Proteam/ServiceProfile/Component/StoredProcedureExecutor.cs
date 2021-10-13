using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ServiceProfile.Component
{
    public static class StoredProcedureExecutor
    {
        public static async Task<T> ExecuteScalarSFAsync<T>(this DbContext context, string storedFunctionName, string parameters = "", string schema = "dbo")
        {
            T returnObject = default(T);

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT {schema}.{storedFunctionName}({parameters});";

                context.Database.OpenConnection();

                try
                {
                    returnObject = (T)await command.ExecuteScalarAsync();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }

            return returnObject;
        }

        public static T ExecuteScalarSF<T>(this DbContext context, string storedFunctionName, string parameters = "", string schema = "dbo")
        {
            T returnObject = default(T);

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT {schema}.{storedFunctionName}({parameters});";

                context.Database.OpenConnection();

                try
                {
                    returnObject = (T)command.ExecuteScalar();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }

            return returnObject;
        }

        public static async Task<T> ExecuteScalarSPAsync<T>(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            T returnObject = default(T);

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    returnObject = (T)await command.ExecuteScalarAsync();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }

            return returnObject;
        }

        public static T ExecuteScalarSP<T>(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            T returnObject = default(T);

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    returnObject = (T)command.ExecuteScalar();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }

            return returnObject;
        }

        public static async Task ExecuteSPVoidAsync(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    await command.ExecuteReaderAsync();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }
        }

        public static void ExecuteSPVoid(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    command.ExecuteReader();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }
        }

        public static async Task<T> ExecuteSPSingleAsync<T>(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            T returnObject = default(T);

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    var dataReader = await command.ExecuteReaderAsync();
                    returnObject = await dataReader.ToSingleAsync<T>();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }

            return returnObject;
        }

        public static T ExecuteSPSingle<T>(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            T returnObject = default(T);

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    var dataReader = command.ExecuteReader();
                    returnObject = dataReader.ToSingle<T>();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }

            return returnObject;
        }

        public static async Task<List<T>> ExecuteSPListAsync<T>(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            var returnObject = new List<T>();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    var dataReader = await command.ExecuteReaderAsync();
                    returnObject = await dataReader.ToListAsync<T>();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }

            return returnObject;
        }

        public static List<T> ExecuteSPList<T>(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            var returnObject = new List<T>();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    var dataReader = command.ExecuteReader();
                    returnObject = dataReader.ToList<T>();
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }

            return returnObject;
        }

        public static int ExecuteScalarInt(this DbContext context, string storedProcedureName, SqlParameter[] parameters)
        {
            int recordsTotal = 0;
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                try
                {
                    string result = command.ExecuteScalar().ToString();
                    int.TryParse(result, out recordsTotal);
                }
                catch (Exception)
                {
                    context.Database.CloseConnection();
                    throw;
                }

                context.Database.CloseConnection();
            }
            return recordsTotal;
        }
    }
}
