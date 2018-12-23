using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
namespace Assets.Framework.Excel
{
    public class BaseExcelEditor : UnityEditor.Editor
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

            UnityEditor.AssetDatabase.CreateAsset(mgr, savePath + fileName + ".asset");
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
        
        
    }
}
#endif