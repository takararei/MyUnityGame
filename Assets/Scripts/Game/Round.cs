using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    [System.Serializable]
    public struct RoundInfo
    {
        public int[] enemyIDList;
    }

    protected int id;
    protected Level mLevel;
    public RoundInfo roundInfo;
    protected Round nextRound;

    public void SetNextRound(Round round)
    {
        nextRound = round;
    }

    public void Handle(int roundID)
    {
        if(id<roundID)
        {
            nextRound.Handle(roundID);
        }
        else
        {
            //敌人处理
        }
    }

}
