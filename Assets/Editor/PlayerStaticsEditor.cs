using Assets.Framework.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerStaticsEditor : BaseExcelEditor
{
    [UnityEditor.MenuItem("ExcelToAsset/PlayerStatics")]
    public static void Click()
    {
        fileName = "PlayerStatics";
        PlayerStatics player = ScriptableObject.CreateInstance<PlayerStatics>();
        CreateAsset(player);
    }
}