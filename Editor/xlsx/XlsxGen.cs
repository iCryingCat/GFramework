using System.Net.Mime;
using System;
using System.IO;
using Excel;
using System.Data;
using gframework;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using XlsxGen.core;
using GFramework;

public class XlsxParser : Editor
{
    private static Conf conf = new Conf()
    {
        sourcePath = PlayerPrefs.GetString(EditorSave.CONFIG_TABLE_PATH),
        genPath = PlayerPrefs.GetString(EditorSave.CONFIG_TABLE_OUT_PATH),
        genType = ".xlsx",
    };

    [MenuItem(GFM.MENU_ROOT + "xlsx Gen", false)]
    public static void Gen()
    {
        DirectoryInfo root = new DirectoryInfo(conf.sourcePath);
        if (root == null) throw new DirectoryNotFoundException(root.FullName);
        ScanMenu(root);
    }

    private static void ScanMenu(DirectoryInfo root)
    {
        FileInfo[] files = root.GetFiles();
        DirectoryInfo[] directories = root.GetDirectories();
        foreach (var directory in directories)
        {
            ScanMenu(directory);
        }
        foreach (var file in files)
        {
            string fullPath = file.FullName;
            string suff = file.Extension;
            if (suff == conf.genType)
            {
                switch (suff)
                {
                    case ".xlsx":
                        ReadXlsx(fullPath);
                        break;
                }
            }
        }
    }

    private static void ReadXlsx(string file)
    {
        GLog.P($"读取 {file}");
        using (FileStream fs = new FileStream(file, FileMode.Open))
        {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet set = excelDataReader.AsDataSet();
            foreach (DataTable sheet in set.Tables)
            {
                string tableName = sheet.TableName;
                string targetFilePath = Path.Combine(conf.genPath, tableName);
                if (File.Exists(targetFilePath))
                {
                    File.Delete(targetFilePath);
                }
                GenLuaData(sheet);
            }
        }
    }

    private static void GenCS(DataTable sheet)
    {
        int columnsNum = sheet.Columns.Count;
        int rowsNum = sheet.Rows.Count;
        if (rowsNum < 2) return;
        // 第1行：表注释
        // 2: 注释|字段类型|字段名称
        string tableName = sheet.TableName;
        string genTargetPath = Path.Combine(conf.genPath, tableName + ".cs");
        GLog.P($"生成 {genTargetPath}");
        CSClassGenner classGenner = new CSClassGenner(tableName);
        using (FileStream fs = new FileStream(genTargetPath, FileMode.Create))
        {
            DataRow row0 = sheet.Rows[0];
            DataRow row1 = sheet.Rows[1];
            classGenner.description = row0.IsNull(0) ? string.Empty : row0[0].ToString();
            for (int i = 0; i < columnsNum; ++i)
            {
                string unit = (string)(row1[i] ?? string.Empty);
                string[] field = unit.Split('|');
                if (field.Length < 3)
                    throw new Exception("请检查字段格式：注释|字段类型|字段名称");
                classGenner.AddDiscription(field[0]);
                classGenner.AddPublicField(field[1], field[2]);
            }

            byte[] bytes = classGenner.EncodeToByte();
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();
        }
        AssetDatabase.Refresh();
    }

    private static void GenLuaData(DataTable sheet)
    {
        int columnsNum = sheet.Columns.Count;
        int rowsNum = sheet.Rows.Count;
        if (rowsNum < 2) return;
        // 第1行：表注释
        // 2: 注释|字段类型|字段名称
        string tableName = sheet.TableName;
        string genTargetPath = Path.Combine(conf.genPath, tableName + ".cs");
        GLog.P($"生成 {genTargetPath}");
        CSClassGenner classGenner = new CSClassGenner(tableName);
        using (FileStream fs = new FileStream(genTargetPath, FileMode.Create))
        {
            DataRow row0 = sheet.Rows[0];
            DataRow row1 = sheet.Rows[1];
            classGenner.description = row0.IsNull(0) ? string.Empty : row0[0].ToString();
            for (int i = 0; i < columnsNum; ++i)
            {
                string unit = (string)(row1[i] ?? string.Empty);
                string[] field = unit.Split('|');
                if (field.Length < 3)
                    throw new Exception("请检查字段格式：注释|字段类型|字段名称");
                classGenner.AddDiscription(field[0]);
                classGenner.AddPublicField(field[1], field[2]);
            }

            byte[] bytes = classGenner.EncodeToByte();
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();
        }
        AssetDatabase.Refresh();
    }
}