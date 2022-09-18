using System.Net;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Excel;
using System.Data;

public class XlsxParser
{
    private const string confPath = "/conf.json";
    private const string sourcePath = "/xlsx";
    private const string genPath = "../GameLogic/xls/";
    private const string suffix = ".xlsx";
    public static void Main(string[] args)
    {
        DirectoryInfo root = new DirectoryInfo(sourcePath);
        if(root == null) throw new DirectoryNotFoundException(root.FullName);
        ScanXlsx(root);
    }

    public static void ScanXlsx(DirectoryInfo root){
        FileInfo[] files = root.GetFiles();
        DirectoryInfo[] directories = root.GetDirectories();
        foreach(var directory in directories){
            ScanXlsx(directory);
        }
        foreach(var file in files){
            string fullPath = file.FullName;
            string suff = file.Extension;
            if(suff == suffix){
                GenData(fullPath);
            }
        }
    }

    public static void GenData(string file){
        using(FileStream fs = new FileStream(file, FileMode.Open)){
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet set = excelDataReader.AsDataSet();
            foreach(var sheet in set.Tables){
                
            }
        }
    }
}