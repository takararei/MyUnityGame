using Assets.Framework.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class EnemyInfoEditor:BaseExcelEditor
{
    [UnityEditor.MenuItem("ExcelToAsset/EnemyInfo")]
    public static void ExcelToAsset()
    {
        fileName = "EnemyInfo";
        EnemyInfoMgr mgr = UnityEngine.ScriptableObject.CreateInstance<EnemyInfoMgr>();
        mgr.enemyInfoList = ReadExcel(excelPath);
        CreateAsset(mgr);
    }

    public static List<EnemyInfo> ReadExcel(string ePath)
    {
        List<EnemyInfo> list = new List<EnemyInfo>();
        using (FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                SetReader(excelReader);
                excelReader.Read();
                for (int i = 1; i < excelReader.RowCount; i++)
                {
                    excelReader.Read();
                    EnemyInfo info = new EnemyInfo();
                    info.enemyId = GetInt(0);
                    info.Name = GetString(1);
                    info.Introduce = GetString(2);
                    info.killCoin = GetInt(3);
                    info.killDO = GetInt(4);
                    info.life = GetInt(5);
                    info.speed = GetInt(6);
                    info.Def = GetInt(7);
                    info.Mdef = GetInt(8);
                    info.heart = GetInt(9);
                    info.path = GetString(10);
                    info.audio = GetString(11);
                    list.Add(info);
                }
            }
        }
        return list;
    }
}
