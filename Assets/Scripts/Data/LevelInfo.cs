using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}