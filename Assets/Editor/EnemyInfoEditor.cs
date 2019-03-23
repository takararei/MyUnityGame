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

                    list.Add(info);
                }
            }
        }
        return list;
    }
}
