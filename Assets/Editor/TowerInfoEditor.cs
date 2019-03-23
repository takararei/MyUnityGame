using Assets.Framework.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class TowerInfoEditor:BaseExcelEditor
{
    [UnityEditor.MenuItem("ExcelToAsset/TowerInfo")]
    public static void ExcelToAsset()
    {
        fileName = "TowerInfo";
        TowerInfoMgr mgr = ScriptableObject.CreateInstance<TowerInfoMgr>();
        mgr.towerInfoList = ReadExcel(excelPath);
        CreateAsset(mgr);
    }

    public static List<TowerInfo> ReadExcel(string ePath)
    {
        List<TowerInfo> list = new List<TowerInfo>();
        using (FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                SetReader(excelReader);
                excelReader.Read();
                for (int i = 1; i < excelReader.RowCount; i++)
                {
                    excelReader.Read();
                    TowerInfo info = new TowerInfo();
                    info.towerId = GetInt(0);
                    
                    list.Add(info);
                }
            }
        }
        return list;
    }
}