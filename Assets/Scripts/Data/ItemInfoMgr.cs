using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class ItemInfoMgr : ScriptableObject
{
    public List<ItemInfo> itemInfoList;

    private static ItemInfoMgr _instance;
    public static ItemInfoMgr instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<ItemInfoMgr>("AssetData/ItemInfo");
            }
            return _instance;
        }
    }

}