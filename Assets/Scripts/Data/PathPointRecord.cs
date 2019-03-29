using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
[Serializable]
public class PathPointRecord:ScriptableObject
{
    public List<Vector3> pathList;
    private static PathPointRecord _instance;
    public static PathPointRecord instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = Resources.Load<PathPointRecord>("AssetData/PathPointRecord");
            }
            return _instance;
        }
    }
}
