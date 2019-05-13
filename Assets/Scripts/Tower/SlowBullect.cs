using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SlowBullect : Bullect
{
    public float slowTime = 2;
    float showTime =0.9f;
    float showTimer;
    bool isAttacked = false;
    protected override void BullectMove()
    {
        if(!isAttacked)
        {
            Attack();
        }
        if (showTimer < showTime)
        {
            showTimer += Time.deltaTime;
        }
        else
        {
            DestoryBullect();
        }
    }
    protected override void Update()
    {
        if (GameController.Instance.isGameOver)
        {
            DestoryBullect();
            return;
        }
        if (GameController.Instance.isPause || !isSetData)
        {
            animator.speed = 0;
            return;
        }
        
        animator.speed = 1;
        BullectMove();
    }
    void Attack()
    {
        isAttacked = true;
        //transform.localScale = Vector3.one;
        //GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        animator.Play("Attack",-1,0);
        animator.Update(0);
        for(int i=0;i<bsTower.enemyTargetList.Count;i++)
        {
            Transform item = bsTower.enemyTargetList[i];
            if (item.gameObject.activeSelf == false)
            {
                continue;
            }
            item.SendMessage("SlowDebuf", slowTime);
            item.SendMessage("TakeDamage", this);
        }
        //foreach (var item in bsTower.enemyTargetList)
        //{
            
        //}
    }

    protected override void DestoryBullect()
    {
        base.DestoryBullect();
        
        isAttacked = false;
        showTimer = 0;
    }
    
}