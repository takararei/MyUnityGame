using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Assets.Framework.Excel
{
    public class BaseExcelEditor : Editor
    {
        public static string excelPath
        {
            get
            {
                return "Assets/Excel/" + fileName + ".xlsx";
            }
        }

        public static string savePath= "Assets/Resources/AssetData/";

        public static string fileName;

        public static void CreateAsset(UnityEngine.Object mgr)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            AssetDatabase.CreateAsset(mgr, savePath + fileName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
    }
}
