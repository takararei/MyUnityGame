using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int levelID;
    public List<GridPoint.GridSate> gridPointsState;
    //本关卡的路径
    public List<GridPoint.GridIndex> path;
    //该关卡的每波怪的信息
    public List<Round.RoundInfo> roundInfoList;
    //多攻击线 int[] 记录一下每个路径的起始点 便于从path中提取片段
}
