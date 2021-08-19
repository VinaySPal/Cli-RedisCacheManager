using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayerSQL
{
    /// <summary>
    /// It is used to establish the sql connection with data source.
    /// </summary>
    public class Connection : IDisposable
    {
        private readonly string _dbName = "EntityMetadata";
        private SqlConnection connection = null;

        public Connection()
        {
        }

        public Connection(string dbName)
        {
            if (!string.IsNullOrEmpty(dbName))
                _dbName = dbName;
        }

        public string GetSQLConnectionString()
        {
            return "Data Source = (local); Initial Catalog = " + _dbName + "; Integrated Security = SSPI";           
        }

        /// <summary>
        /// It establishes the connection.
        /// </summary>
        /// <returns></returns>
        public SqlConnection CreateConnection()
        {
            try
            {
                connection = new SqlConnection(GetSQLConnectionString());
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to connect to Sql Server. Please provide correct Data Sourcec and Database.", ex);
            }
            return connection;
        }

        /// <summary>
        /// It disposes the connection
        /// </summary>
        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }

    /// <summary>
    /// It extends the sql connection object with additional functionalities.
    /// </summary>
    public static class SqlConnectionExtensions
    {
        /// <summary>
        /// It returns the command object.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <param name="spParams"></param>
        /// <param name="isUSP"></param>
        /// <param name="spOutputParams"></param>
        /// <returns></returns>
        public static IDbCommand GetCommand(this SqlConnection connection, string commandText, CommandParameters spParams = null, bool isUSP = false)
        {
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;

                    if (isUSP)
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                    if (spParams != null)
                        spParams.Parameters.ForEach(p =>
                        {
                            command.AddParameter(p);
                        });

                    return command;
                }
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Please specify valid database settings.");
            }
        }
    }
}
