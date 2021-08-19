using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerSQL
{
    /// <summary>
    /// It represents as parameter entity.
    /// </summary>
    public class CommandParameter
    {
        public string Name { get; set; }
        public Object Value { get; set; }
        public SqlDbType DataTypeInDb { get; set; }
    }
}
