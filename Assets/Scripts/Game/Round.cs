using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    protected int id;
    public RoundData info;
    protected Round nextRound;
    public Round(RoundData data)
    {
        info = data;
        id = info.index-1;
    }
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
            GameController.Instance.CreateEnemy(info.enemyList);
            //GameController.Instance.enemyIdList = info.enemyList;
            //GameController.Instance.isNeedCreateEnemy = true;
        }
    }

}
