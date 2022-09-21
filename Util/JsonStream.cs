using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace GFramework
{
    public class JsonStream
    {
        private string filePath;

        public string Data { get; }

        public JsonStream(string path)
        {
            if (!File.Exists(path))
                throw new Exception(string.Format("file path {0} is not exist", path));
            this.filePath = path;
            using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
            {
                this.Data = streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// 写入json
        /// </summary>
        /// <param name="json"></param>
        /// <param name="pos"></param>
        public void Write(string json, int pos = 0)
        {
            if (!File.Exists(this.filePath))
                return;
            using (FileStream fileStream = new FileStream(this.filePath, FileMode.Open, FileAccess.Write))
            {
                fileStream.Position = pos;
                byte[] bytes = Encoding.Default.GetBytes(json);
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

        public static T Decode<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        public static string Decode<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static Dictionary<T1, T2> ToMap<T1, T2>(string jsonText)
        {
            Dictionary<T1, T2> map = JsonConvert.DeserializeObject<Dictionary<T1, T2>>(jsonText);
            return map;
        }

        public static string ToJson<T1, T2>(Dictionary<T1, T2> keyMap)
        {
            string json = JsonConvert.SerializeObject(keyMap);
            return json;
        }
    }


}