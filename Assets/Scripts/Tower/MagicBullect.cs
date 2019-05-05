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
    private float attackTimer = 0;
    private float damageTimeCell = 0.2f;
    private float damageTimeCellTimer = 0;
    Transform target;

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

        //如果飞到一半突然物体已经消失 则子弹也消失
        if (!target.gameObject.activeSelf || (bsTower.towerProperty.target != target)|| bsTower.towerProperty.target == null || !bsTower.towerProperty.target.gameObject.activeSelf)//|| GameController.Instance.isGameOver)
        {
            DestoryBullect();
            return;
        }
        animator.speed = 1;
        BullectMove();
    }

    protected override void BullectMove()//这个需要持续一段时间
    {
        //base.BullectMove();
        if (attackTimer < attackTime)
        {
            attackTimer += Time.deltaTime;//做以下的位置调整
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, 2);
            transform.up = targetPos - bsTower.transform.position;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

            distance = Vector3.Distance(bsTower.transform.position - new Vector3(0, 0, 2), target.position);
            bullectWidth = distance / 2;
            transform.position = new Vector3((target.position.x + bsTower.transform.position.x) / 2,
                (target.position.y + bsTower.transform.position.y) / 2, 0);
            transform.localScale = new Vector3(1, bullectWidth, 1);

            if (damageTimeCellTimer < damageTimeCell)
            {
                damageTimeCellTimer += Time.deltaTime;
            }
            else
            {
                bsTower.towerProperty.target.SendMessage("TakeDamage", this);
                damageTimeCellTimer = 0;
            }
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

    void SetTrans(Transform trans)
    {
        target = trans;
    }
}