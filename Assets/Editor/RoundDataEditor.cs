using Assets.Framework.Excel;
using ExcelDataReader;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoundDataEditor : BaseExcelEditor
{
    [UnityEditor.MenuItem("ExcelToAsset/RoundData")]
    public static void ExcelToAsset()
    {
        fileName = "RoundData";
        RoundDataMgr mgr = ScriptableObject.CreateInstance<RoundDataMgr>();
        mgr.roundDataList = ReadExcel(excelPath);
        CreateAsset(mgr);
    }

    public static List<RoundData> ReadExcel(string ePath)
    {
        List<RoundData> list = new List<RoundData>();
        using (FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                excelReader.Read();
                for(int i=1;i<excelReader.RowCount;i++)
                {
                    excelReader.Read();
                    RoundData rdata = new RoundData();
                    rdata.enemyList = new List<int>();
                    rdata.pathList = new List<int>();
                    rdata.levelID = int.Parse(excelReader.GetString(0));
                    rdata.index = int.Parse(excelReader.GetString(1));
                    string[] enemyStrArr=excelReader.GetString(2).Split(',');
                    for(int j=0;j<enemyStrArr.Length;j++)
                    {
                        rdata.enemyList.Add(int.Parse(enemyStrArr[j]));
                    }
                    string[] pathStrArr = excelReader.GetString(3).Split(',');
                    for(int k=0;k<pathStrArr.Length;k++)
                    {
                        rdata.pathList.Add(int.Parse(pathStrArr[k]));
                    }
                    list.Add(rdata);
                }
            }
        }
        return list;
    }
}
