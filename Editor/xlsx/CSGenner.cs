using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gframework
{
    public class CSClassGenner
    {
        public string className { get; }
        public string description = string.Empty;
        private StringBuilder body;

        public CSClassGenner(string className)
        {
            this.body = new StringBuilder();
            this.className = className;
        }

        public byte[] EncodeToByte()
        {
            StringBuilder sb = new StringBuilder();
            string classBegin = $"public class {this.className} {{";
            string classEnd = $"}}";
            sb.AppendLine(this.GetDiscription(this.description));
            sb.AppendLine(classBegin);
            sb.Append(this.body);
            sb.AppendLine(classEnd);
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public void AddDiscription(string disc)
        {
            this.body.AppendLine(GetDiscription(disc));
        }

        private string GetDiscription(string disc)
        {
            return $"// {disc}";
        }

        public void AddPublicField(string fieldType, string fieldName)
        {
            this.body.AppendLine(GetPublicField(fieldType, fieldName));
        }

        private string GetPublicField(string fieldType, string fieldName)
        {
            return $"public {fieldType} {fieldName};";
        }
    }
}