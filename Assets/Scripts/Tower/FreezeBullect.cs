using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FreezeBullect:Bullect
{

    public float freezeTime = 2;
    float showTime = 0.9f;
    float showTimer;
    protected override void BullectMove()
    {
        if (showTimer < showTime)
        {
            showTimer += Time.deltaTime;
        }
        else
        {
            Attack();
            DestoryBullect();
        }
    }

    void Attack()
    {
        foreach (var item in bsTower.enemyTargetList)
        {
            if (item.gameObject.activeSelf == false)
            {
                continue;
            }
            item.SendMessage("FreezeDebuf", freezeTime);
            item.SendMessage("TakeDamage", this);
        }
    }

    protected override void DestoryBullect()
    {
        showTimer = 0;
        animator.Update(0);
        base.DestoryBullect();
    }

}