using Assets.Framework.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class ItemInfoEditor:BaseExcelEditor
{
    [UnityEditor.MenuItem("ExcelToAsset/ItemInfo")]
    public static void ExcelToAsset()
    {
        fileName = "ItemInfo";
        ItemInfoMgr mgr = UnityEngine.ScriptableObject.CreateInstance<ItemInfoMgr>();
        mgr.itemInfoList = ReadExcel(excelPath);
        CreateAsset(mgr);
    }

    public static List<ItemInfo> ReadExcel(string ePath)
    {
        List<ItemInfo> list = new List<ItemInfo>();
        using (System.IO.FileStream stream = System.IO.File.Open(excelPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
        {
            using (ExcelDataReader.IExcelDataReader excelReader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                SetReader(excelReader);
                excelReader.Read();
                for (int i = 1; i < excelReader.RowCount; i++)
                {
                    excelReader.Read();
                    ItemInfo info = new ItemInfo();
                    info.id = GetInt(0);
                    info.name = GetString(1);
                    info.introduce = GetString(2);
                    info.CD = GetInt(3);
                    info.time = GetInt(4);
                    info.price = GetInt(5);
                    list.Add(info);
                }
            }
        }
        return list;
    }
}