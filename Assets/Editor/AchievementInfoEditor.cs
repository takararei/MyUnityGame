using Assets.Framework.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AchievementInfoEditor:BaseExcelEditor
{
    [UnityEditor.MenuItem("ExcelToAsset/AchievementInfo")]
    public static void ExcelToAsset()
    {
        fileName = "AchievementInfo";
        AchievementInfoMgr mgr = UnityEngine.ScriptableObject.CreateInstance<AchievementInfoMgr>();
        mgr.infoList = ReadExcel(excelPath);
        CreateAsset(mgr);
    }

    public static List<AchievementInfo> ReadExcel(string ePath)
    {
        List<AchievementInfo> list = new List<AchievementInfo>();
        using (System.IO.FileStream stream = System.IO.File.Open(excelPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
        {
            using (ExcelDataReader.IExcelDataReader excelReader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                SetReader(excelReader);
                excelReader.Read();
                for (int i = 1; i < excelReader.RowCount; i++)
                {
                    excelReader.Read();
                    AchievementInfo info = new AchievementInfo();
                    info.id = GetInt(0);
                    info.name = GetString(1);
                    info.introduce = GetString(2);
                    info.unFinishSprite = GetString(3);
                    info.FinshedSprite = GetString(4);
                    info.Count = GetInt(5);
                    list.Add(info);
                }
            }
        }
        return list;
    }
}
