using Assets.Framework.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
namespace Assets.Example
{ 
    public class TestDataEditor : BaseExcelEditor
    {

        [UnityEditor.MenuItem("ExcelToAsset/TestData")]
        public static void ExcelToAsset()
        {
            fileName = "TestData";
            TestDataManager mgr = ScriptableObject.CreateInstance<TestDataManager>();
            mgr.TestDataList = ReadExcel(excelPath);

            CreateAsset(mgr);
        }

        public static List<TestDataObject> ReadExcel(string ePath)
        {
            List<TestDataObject> testDict = new List<TestDataObject>();
            using (FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    excelReader.Read();
                    //除了第一行的字段名 和最后一行空白
                    for (int i = 1; i < excelReader.RowCount-1; i++)
                    {
                        excelReader.Read();
                        TestDataObject pdata = new TestDataObject();
                        //数字类型需要转换
                        pdata.id = int.Parse(excelReader.GetString(0));
                        pdata.name = excelReader.GetString(1);
                        pdata.path = excelReader.GetString(2);
                        testDict.Add(pdata);

                    }
                }
            }

            return testDict;
        }

    }
}
#endif