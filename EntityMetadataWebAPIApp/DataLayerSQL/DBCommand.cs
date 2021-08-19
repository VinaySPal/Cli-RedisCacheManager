using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataLayerSQL
{
    /// <summary>
    /// It provides the common data base commands to perform operation on data base.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DBCommand<T> : DBCommandBase<T> where T : new()
    {
        string _dbName = string.Empty;
        public DBCommand()
        {
        }
        public DBCommand(string dbName)
        {
            _dbName = dbName;
        }
        
        /// <summary>
        /// It is used to get list of entity.
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<T> Get(string commandText, CommandParameters spParams = null, bool isUSP = false,bool IsCommandTimeOutIndefinite = false)
        {
            try
            {
                using (var objConnection = new Connection(_dbName))
                {
                    using (var command = objConnection.CreateConnection().GetCommand(commandText, spParams, isUSP))
                    {
                        if (IsCommandTimeOutIndefinite) command.CommandTimeout = 0;

                        return ToList(command);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Please specify valid database settings.");
            }
        }

        /// <summary>
        /// It peforms the update operation.
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <returns>no of rows affected</returns>
        protected int ExecuteNonQuery(string commandText, CommandParameters spParams = null, bool isUSP = false)
        {
            int affectedRows = 0;
            try
            {
                using (var objConnection = new Connection(_dbName))
                {
                    using (var command = objConnection.CreateConnection().GetCommand(commandText, spParams, isUSP))
                    {
                        affectedRows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Please specify valid database settings.");
            }

            return affectedRows;
        }
    }
}
