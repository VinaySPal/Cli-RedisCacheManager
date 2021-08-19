using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayerSQL
{
    /// <summary>
    /// It provides the common commands to perform operation on data base.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DBCommandBase<T> where T : new()
    {
        /// <summary>
        /// It creates the list of poco after performing execution of query.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected IEnumerable<T> ToList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<T> items = new List<T>();

                while (reader.Read())
                {
                    var item = CreateEntity();
                    Map(reader, item);
                    items.Add(item);
                }
                return items;
            }
        }

        /// <summary>
        /// It create the poco object.
        /// </summary>
        /// <returns></returns>
        protected abstract T CreateEntity();

        /// <summary>
        /// It maps the data base value to poco object.
        /// </summary>
        /// <param name="record"></param>
        /// <param name="entity"></param>
        protected abstract void Map(IDataRecord record, T entity);

    }

    /// <summary>
    /// Extension method to add parameter to command
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// It is used to create parameter to data base command.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AddParameter(this IDbCommand command, CommandParameter param)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (string.IsNullOrEmpty(param.Name)) throw new ArgumentNullException("name");
            
            var sqlParam = new SqlParameter();
            sqlParam.ParameterName = param.Name;
            sqlParam.Value = param.Value;
            sqlParam.SqlDbType = param.DataTypeInDb;

            command.Parameters.Add(sqlParam);
        }
    }

}
