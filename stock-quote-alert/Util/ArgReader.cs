using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Util
{
    public class ArgReader
    {
        private string[] args;
        public ArgReader(string[] _args)
        {
            args = _args;
        }
        public string SafeGetPositionalArgValue(int position, string? argName)
        {
            try
            {
                return args[position];
            }
            catch
            {
                throw new Exception(
                    "Could not read the "
                    + (!string.IsNullOrEmpty(argName) ? "'" + argName + "'" : position + "'st")
                    + " argument"
                );
            }
        }
    }
}
