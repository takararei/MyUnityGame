using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
[Serializable]
public class PlayerStatics:ScriptableObject
{
    public List<int> levelStar;
    public int finishedLevelCount;//当前完成的关卡总数
    public int DO;//钻石数
    public int itemNum;//某道具持有数
    //成就列表记录
    public bool isMusicOff;//音乐开关
    public bool isEffectOff;//音效开关

    private static PlayerStatics _instance;
    public static PlayerStatics Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = Resources.Load<PlayerStatics>("AssetData/PlayerStatics");
            }
            return _instance;
        }
    }

}
#if UNITY_EDITOR
public class PlayerStaticsEditor:Editor
{ 
    [MenuItem("ExcelToAsset/PlayerStatics")]
    public static void Click()
    {
        PlayerStatics player = ScriptableObject.CreateInstance<PlayerStatics>();
        UnityEditor.AssetDatabase.CreateAsset(player, "Assets/Resources/AssetData/PlayerStatics.asset");
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        Debug.Log("PlayerStatics生成成功");
    }
}

#endif
