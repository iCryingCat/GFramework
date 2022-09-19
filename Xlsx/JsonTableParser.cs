using GFramework;
using System.IO;

public class JsonTableParser : IParser
{
    public T Parse<T>(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException(path);
        using (StreamReader reader = new StreamReader(path))
        {
            string text = reader.ReadToEnd();
            T map = JsonStream.Decode<T>(text);
            return map;
        }
    }
}
