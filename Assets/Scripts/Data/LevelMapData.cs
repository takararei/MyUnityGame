using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelMapData
{
    public int levelID;
    public List<GridPoint.GridState> gridStateList;
}
