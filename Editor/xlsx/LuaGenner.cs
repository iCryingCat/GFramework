using System.Security.Cryptography.X509Certificates;
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
        public string description = string.Empty;


        public LuaGenner(string tableName)
        {
            this.body = new StringBuilder();
            this.tableName = tableName;
        }

        public byte[] EncodeToByte()
        {
            StringBuilder sb = new StringBuilder();
            string tableBegin = $"local {this.tableName} = {{";
            string tableEnd = "}";
            string module = $"return {this.tableName};";
            sb.AppendLine(this.GenDescription(this.description));
            sb.AppendLine(tableBegin);
            sb.Append(this.body);
            sb.AppendLine(tableEnd);
            sb.AppendLine(module);
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public void AddDescription(string desc)
        {
            this.body.AppendLine(GenDescription(desc));
        }

        public void AddUnitBegin(string index)
        {
            this.body.AppendLine(GenUnitBegin(index));
        }

        public void AddUnitEnd()
        {
            this.body.AppendLine(GenListEnd());
        }

        public void AddNumber(string key, string value)
        {
            this.body.AppendLine(GenNumberPair(key, value));
        }

        public void AddString(string key, string value)
        {
            this.body.AppendLine(GenStringPair(key, value));
        }

        public void AddListNumber(string key, string[] items)
        {
            this.body.AppendLine(GenListBegin(key));
            foreach (var item in items)
            {
                this.body.AppendLine(GenNumber(item));
            }
            this.body.AppendLine(GenListEnd());
        }

        public void AddListString(string key, string[] items)
        {
            this.body.AppendLine(GenListBegin(key));
            foreach (var item in items)
            {
                this.body.AppendLine(GenString(item));
            }
            this.body.AppendLine(GenListEnd());
        }

        private string GenTableBegin()
        {
            return $"local {this.tableName} = {{";
        }

        private string GenTableEnd()
        {
            return "}";
        }

        private string GenDescription(string desc)
        {
            return $"-- {desc}";
        }

        private string GenUnitBegin(string index)
        {
            return $"[{index}] = {{";
        }

        private string GenUnitEnd(string index)
        {
            return "}";
        }

        private string GenListBegin(string key)
        {
            return $"{key} = {{";
        }

        private string GenListEnd()
        {
            return "}";
        }

        private string GenNumberPair(string key, string value)
        {
            return $"{key} = {value},";
        }

        private string GenStringPair(string key, string value)
        {
            return $"{key} = \"{value}\",";
        }

        private string GenNumber(string value)
        {
            return $"{value},";
        }

        private string GenString(string value)
        {
            return $"\"{value}\",";
        }
    }
}