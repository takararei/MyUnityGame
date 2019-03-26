using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[Serializable]
public class AchievementInfoMgr:UnityEngine.ScriptableObject
{
    public List<AchievementInfo> infoList;
    private static AchievementInfoMgr _instance;
    public static AchievementInfoMgr Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = UnityEngine.Resources.Load<AchievementInfoMgr>("AssetData/AchievementInfo");
            }
            return _instance;
        }
    }
}
