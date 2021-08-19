using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerSQL
{
    /// <summary>
    /// It is used to generate parameters for the command.
    /// </summary>
    public class CommandParameters
    {
        List<CommandParameter> _params = null;

        public List<CommandParameter> Parameters { get { return this._params; } }

        public CommandParameters()
        {
            this._params = new List<CommandParameter>();
        }

        public bool Add(CommandParameter param)
        {
            if (param.Value == null || string.IsNullOrEmpty(param.Value.ToString()) || param.Value.ToString() == "0")
            {
                param.Value = DBNull.Value;
            }
            this._params.Add(param);
            return true;
        }
    }
}
