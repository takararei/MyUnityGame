using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Framework.Excel.Test
{
    public class TestDataEditor : BaseExcelEditor
    {
        [MenuItem("ExcelToAsset/TestData")]
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
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    excelReader.Read();
                    for (int i = 1; i < excelReader.RowCount; i++)
                    {
                        excelReader.Read();
                        TestDataObject pdata = new TestDataObject();
                        pdata.id = excelReader.GetString(0);
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
