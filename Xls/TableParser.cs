using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TableParser
{
    private IParser parser;

    public TableParser(IParser parser)
    {
        this.parser = parser;
    }

    public T Parse<T>(string path)
    {
        return this.parser.Parse<T>(path);
    }
}
