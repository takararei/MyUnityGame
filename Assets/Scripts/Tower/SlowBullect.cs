using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SlowBullect:Bullect
{
    public float slowTime=2;
    float showTime=0.9f;
    float showTimer;
    protected override void BullectMove()
    {
        if(showTimer<showTime)
        {
            showTimer += Time.deltaTime;
        }
        else
        {
            Attack();
            animator.speed = 0;
            animator.Update(0);
            DestoryBullect();
            showTimer = 0;
        }
    }

     void Attack()
    {
        foreach (var item in bsTower.enemyTargetList)
        {
            if(item.gameObject.activeSelf==false)
            {
                continue;
            }
            item.SendMessage("SlowDebuf", slowTime);
            item.SendMessage("TakeDamage", this);
        }
    }

}