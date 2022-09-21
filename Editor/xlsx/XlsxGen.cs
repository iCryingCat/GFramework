using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using Excel;

using gframework;

using GFramework;

using UnityEditor;

using UnityEngine;

using XlsxGen.core;

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
        GLog.P("XlsxGen", $"read {file}");
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
        GLog.P("XlsxGen", $"gen cs {genTargetPath}");
        CSClassGenner classGenner = new CSClassGenner(tableName);
        using (FileStream fs = new FileStream(genTargetPath, FileMode.Create))
        {
            DataRow row0 = sheet.Rows[0];
            DataRow row1 = sheet.Rows[1];
            if (!row0.IsNull(0))
                classGenner.description = row0[0].ToString();
            for (int i = 0; i < columnsNum; ++i)
            {
                if (row1.IsNull(i)) throw new NoNullAllowedException();
                string unit = row1[i].ToString();
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
        if (rowsNum < 4) return;
        // 第1行：表注释
        // 2: 注释
        // 3: 字段类型
        // 4: 字段名称
        string tableName = sheet.TableName;
        string genTargetPath = Path.Combine(conf.genPath, tableName + ".lua");
        GLog.P("XlsxGen", $"gen lua {genTargetPath}");
        LuaGenner luaGenner = new LuaGenner(tableName);
        using (FileStream fs = new FileStream(genTargetPath, FileMode.Create))
        {
            DataRow row0 = sheet.Rows[0];
            DataRow row1 = sheet.Rows[1];
            DataRow row2 = sheet.Rows[2];
            DataRow row3 = sheet.Rows[3];
            List<string> descList = new List<string>();
            List<string> typeList = new List<string>();
            List<string> nameList = new List<string>();
            if (!row0.IsNull(0))
                luaGenner.description = row0[0].ToString();
            for (int i = 0; i < columnsNum; ++i)
            {
                if (row2.IsNull(i)) throw new NoNullAllowedException();
                if (row3.IsNull(i)) throw new NoNullAllowedException();
                string desc = (string)(row1[i] ?? string.Empty);
                string type = row2[i].ToString();
                string name = row3[i].ToString();
                descList.Add(desc);
                typeList.Add(type);
                nameList.Add(name);
            }
            for (int i = 4; i < rowsNum; ++i)
            {
                for (int j = 0; j < columnsNum; ++j)
                {
                    DataRow row = sheet.Rows[i];
                    switch (typeList[j])
                    {
                        case "number":
                            luaGenner.AddNumber(nameList[j], row[j].ToString());
                            break;
                        case "string":
                            luaGenner.AddString(nameList[j], row[j].ToString());
                            break;
                        case "list<number>":
                            luaGenner.AddListNumber(nameList[j], row[j].ToString().Split('|'));
                            break;
                        case "list<string>":
                            luaGenner.AddListString(nameList[j], row[j].ToString().Split('|'));
                            break;
                        default:
                            throw new Exception($"不支持该类型{typeList[j]}");
                    }
                }
            }
            byte[] bytes = luaGenner.EncodeToByte();
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();
        }
        AssetDatabase.Refresh();
    }
}