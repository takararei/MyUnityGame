using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Framework.Excel;
using ExcelDataReader;
using UnityEngine;

public class LevelInfoEditor:BaseExcelEditor
{
    [UnityEditor.MenuItem("ExcelToAsset/LevelInfo")]
    public static void ExcelToAsset()
    {
        fileName = "LevelInfo";
        LevelInfoMgr mgr = ScriptableObject.CreateInstance<LevelInfoMgr>();
        mgr.levelInfoList = ReadExcel(excelPath);
        CreateAsset(mgr);
    }

    public static List<LevelInfo> ReadExcel(string ePath)
    {
        List<LevelInfo> list = new List<LevelInfo>();
        using (FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                excelReader.Read();
                for (int i = 1; i < excelReader.RowCount; i++)
                {
                    excelReader.Read();
                    LevelInfo info = new LevelInfo();
                    info.towerList = new List<int>();
                    info.levelID = int.Parse(excelReader.GetString(0));
                    info.levelName = excelReader.GetString(1);
                    info.totalRound = int.Parse(excelReader.GetString(2));
                    info.beginCoin = int.Parse(excelReader.GetString(3));
                    info.life = int.Parse(excelReader.GetString(4));
                    info.levelIntroduce = excelReader.GetString(5);
                    info.mapPath = excelReader.GetString(6);
                    string[] towerStrArr = excelReader.GetString(7).Split(',');
                    for(int j=0;j<towerStrArr.Length;j++)
                    {
                        info.towerList.Add(int.Parse(towerStrArr[j]));
                    }
                    if(i<=LevelInfoMgr.Instance.levelInfoList.Count)
                    {
                        info.levelPos = LevelInfoMgr.Instance.levelInfoList[i-1].levelPos;
                    }
                        
                    list.Add(info);
                }
            }
        }
        return list;
    }
}