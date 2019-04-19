using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[Serializable]
public class PlayerData
{
    public int finishedLevelCount;//当前完成的关卡总数
    public int DO;//钻石数
    //成就列表记录
    //public List<int> levelStar;
    //public List<int> itemNum;
    //public List<AchievementRecord> achievementList;
    public int[] levelStar;
    public int[] itemNum;
    public AchievementRecord[] achievementList;

    public PlayerData()
    {
        DO = 0;
        finishedLevelCount = 0;

        int levelCount = 30;
        int itemCount = 5;
        int AcCount = 20;
        levelStar = new int[levelCount];
        itemNum = new int[itemCount];
        achievementList = new AchievementRecord[AcCount];
        //levelStar = new List<int>();
        //for(int i=0;i<levelCount;i++)
        //{
        //    levelStar.Add(0);
        //}


        //itemNum = new List<int>();
        //for(int k=0;k<itemCount;k++)
        //{
        //    itemNum.Add(0);
        //}


        //achievementList = new List<AchievementRecord>();
        //for(int j=0;j<AcCount;j++)
        //{
        //    achievementList.Add(new AchievementRecord());
        //}
    }



}