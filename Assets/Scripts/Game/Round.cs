using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{

    protected int id;
    protected Level mLevel;
    protected Round nextRound;
    public List<RoundData> roundInfo;
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
