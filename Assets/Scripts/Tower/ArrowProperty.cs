﻿using Assets.Framework.Audio;
using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class ArrowProperty : TowerProperty
{
    Transform arrow1;
    Transform arrow2;
    Animator animator1;
    Animator animator2;
    Transform bullectBornTrans;
    bool isArrow1Shotted;
    SpriteRenderer arrowRender1;
    SpriteRenderer arrowRender2;

    private void Awake()
    {

        baseTower = GetComponent<BaseTower>();
        arrow1 = transform.Find("arrow1");
        arrow2 = transform.Find("arrow2");
        arrowRender1=arrow1.GetComponent<SpriteRenderer>();
        arrowRender2 = arrow2.GetComponent<SpriteRenderer>();
        animator1 = arrow1.GetComponent<Animator>();
        animator2 = arrow2.GetComponent<Animator>();
    }

    protected override void TransformRotate()
    {
        //base.TransformRotate();
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, 2);
        arrow1.up = targetPos - arrow1.position;
        arrow1.eulerAngles = new Vector3(0, 0, arrow1.eulerAngles.z);

        arrow2.up = targetPos - arrow2.position;
        arrow2.eulerAngles = new Vector3(0, 0, arrow2.eulerAngles.z);

    }

    protected override void Attack()
    {
        //base.Awake();
        if (target == null)
        {
            return;
        }
        if(!isArrow1Shotted)
        {
            animator1.Play("Attack",-1,0);
            animator1.Update(0);
            isArrow1Shotted = true;
            bullectBornTrans = arrow1;
        }
        else
        {
            animator2.Play("Attack", -1, 0);
            animator2.Update(0);
            isArrow1Shotted = false;
            bullectBornTrans = arrow2;
        }
        isBeginCD = true;
        GameController.Instance.CreateBullect(baseTower);
        AudioMgr.Instance.PlayEffectMusic(baseTower.towerInfo.audio);
        
    }

    public override void Recycle()
    {
        //base.Recycle();
        timeVal = 0;//攻击计时器
        isBeginCD = false;
        target = null;
        isArrow1Shotted = false;
        arrow1.up = Vector3.up;
        arrow2.up = -Vector3.up;
        bullectBornTrans = null;
        arrowRender1.sprite = FactoryMgr.Instance.GetSprite("Tower/Recycle/" + baseTower.towerInfo.towerId);
        arrowRender2.sprite = FactoryMgr.Instance.GetSprite("Tower/Recycle/" + baseTower.towerInfo.towerId);
    }

    public override void GetBullectProperty(Bullect obj)
    {
        obj.transform.position = bullectBornTrans.position-new Vector3(0,0,2);
        
    }

}