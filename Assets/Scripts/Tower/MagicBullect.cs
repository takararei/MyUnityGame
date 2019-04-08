using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MagicBullect : Bullect
{
    float distance;
    private float bullectWidth;
    private float bullectLength;
    private float attackTime = 1f;
    private float damageTimeCell = 0.2f;
    private float damageTimeCellTimer = 0;
    private float attackTimer = 0;


    protected override void BullectMove()//这个需要持续一段时间
    {
        //base.BullectMove();
        if (attackTimer < attackTime)
        {
            attackTimer += Time.deltaTime;//做以下的位置调整
            Vector3 targetPos = new Vector3(targetTrans.position.x, targetTrans.position.y, 2);
            transform.up = targetPos - towerTrans.position;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

            distance = Vector3.Distance(towerTrans.position - new Vector3(0, 0, 2), targetTrans.position);
            bullectWidth = distance / 2;
            //if (bullectWidth <= 0.5f)
            //{
            //    bullectWidth = 0.5f;
            //}
            //else if (bullectWidth >= 1)
            //{
            //    bullectWidth = 1;
            //}
            transform.position = new Vector3((targetTrans.position.x + towerTrans.position.x) / 2,
                (targetTrans.position.y + towerTrans.position.y) / 2, 0);
            transform.localScale = new Vector3(1, bullectWidth, 1);

            if (damageTimeCellTimer < damageTimeCell)
            {
                damageTimeCellTimer += Time.deltaTime;
            }
            else
            {
                targetTrans.SendMessage("TakeDamage", this);
                damageTimeCellTimer = 0;
            }

            Debug.Log(distance);
        }
        else
        {
            DestoryBullect();//回收的时候重置计时器和时间间隔计时器
        }


    }

    protected override void DestoryBullect()
    {
        base.DestoryBullect();
        attackTimer = 0;
        damageTimeCellTimer = 0;
        transform.localScale = new Vector3(1, 0.1f, 1);
    }
}