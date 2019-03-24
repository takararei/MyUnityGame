using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class LevelInfo
{
    public int levelID;
    public string levelName;
    public int totalRound;
    public int beginCoin;
    public int life;
    public string levelIntroduce;
    public string mapPath;
    public List<int> towerList;
    public Vector3 levelPos;
}