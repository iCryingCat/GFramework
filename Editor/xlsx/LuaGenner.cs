using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gframework
{
    public class LuaGenner
    {
        private StringBuilder body;
        private string tableName;

        public LuaGenner(string tableName)
        {
            this.body = new StringBuilder();
            this.tableName = tableName;
        }

        private string AddNumber(string key, string value)
        {
            return $"{key} = {value}";
        }

        private string AddString(string key, string value)
        {
            return $"{key} = \"{value}\"";
        }

        private string AddReturn()
        {
            return $"return {this.tableName};";
        }
    }
}