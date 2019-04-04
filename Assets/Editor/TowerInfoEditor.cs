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
                    info.Name = GetString(1);
                    info.Introduce = GetString(2);
                    info.buildCoin = GetInt(3);
                    info.sellCoin = GetInt(4);
                    info.damageType = GetInt(5);
                    info.damageRange = GetFloat(6);
                    info.CD = GetInt(7);
                    info.damage = GetInt(8);
                    info.Range = GetInt(9);
                    info.bullectPath = GetString(10);
                    info.nextTowerId = GetInt(11);
                    info.path = GetString(12);
                    list.Add(info);
                }
            }
        }
        return list;
    }
}