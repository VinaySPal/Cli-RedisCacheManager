using CustomException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityMetadataBL.Helper
{
    public static class DataTableHelper
    {
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();
            try
            { 
                PropertyInfo[] props = typeof(T).GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    PropertyInfo prop = props[i];
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                object[] values = new object[props.Length];
                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
            }
            catch (Exception ex)
            {
                throw new HandledException("Internal transformation error check Console log", ex);
            }
            return table;
        }

        public static DataTable ParseDictToDataTable(Dictionary<string, List<string>> fieldDict)
        {
            DataTable dt = new DataTable();
            try
            {
                string keyName = fieldDict.Keys.FirstOrDefault();
                dt.TableName = keyName;
                dt.Columns.Add(keyName, typeof(String));

                fieldDict[keyName].ForEach(field =>
                {
                    dt.Rows.Add(new object[] { field });
                });

            }
            catch (Exception ex)
            {
                throw new HandledException("Internal transformation error check Console log", ex);
            }
            return dt;
        }
    }
}
