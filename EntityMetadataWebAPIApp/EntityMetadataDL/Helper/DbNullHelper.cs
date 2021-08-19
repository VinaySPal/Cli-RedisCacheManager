using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerSQL.Helper
{
    internal class DbNullHelper<T>
    {
        /// <summary>
        /// It checks the db null value and return the null of the nullable datatype.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        internal static T DBNullToNull(object val)
        {
            Type type = typeof(T);

            try
            {
                if (val is System.DBNull && type == typeof(int?)) return (T)((object)null);
                if (val is System.DBNull && type == typeof(DateTime?)) return (T)((object)null);
                if (val is System.DBNull && type == typeof(decimal?)) return (T)((object)null);
                if (val is System.DBNull && type == typeof(Int64?)) return (T)((object)null);
                if (val is System.DBNull && type == typeof(bool?)) return (T)((object)null);
                if (val is System.DBNull && type == typeof(string)) return (T)((object)null);

                if (type == typeof(int?))
                    val = Convert.ToInt32(val);

                if (type == typeof(Int64?))
                    val = Convert.ToInt64(val);

                if (type == typeof(string))
                    val = Convert.ToString(val);

                if (type == typeof(DateTime?))
                    val = Convert.ToDateTime(val);

                if (type == typeof(decimal?))
                    val = Convert.ToDecimal(val);

                if (type == typeof(bool?))
                    val = Convert.ToBoolean(val);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return (T)((object)val);
        }
    }
}
