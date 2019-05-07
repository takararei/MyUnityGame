using Assets.Framework.Audio;
using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class TowerProperty : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    protected BaseTower baseTower;
    protected Animator animator;
    protected float timeVal;//攻击计时器
    protected bool isBeginCD;
    [HideInInspector]
    //public Transform bullectBornTrans;
    GameObject bullectGO;
    SpriteRenderer towerSpriteRender;
    private void Awake()
    {
        baseTower = GetComponent<BaseTower>();
        animator = GetComponent<Animator>();
        towerSpriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isBeginCD)
        {
            if (timeVal >= baseTower.towerInfo.CD)
            {
                timeVal = 0;
                isBeginCD = false;
            }
            else
            {
                timeVal += Time.deltaTime;
            }
        }
        if (target == null || GameController.Instance.isPause == true)
        {
            return;
        }
       

        TransformRotate();
        if (isBeginCD == false)
        {
            Attack();
        }


    }

    protected virtual void TransformRotate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, 2);
        transform.up = targetPos - transform.position;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    protected virtual void Attack()
    {
        if (target == null)
        {
            return;
        }
        animator.Play("Attack", -1, 0);
        animator.Update(0);
        isBeginCD = true;
        GameController.Instance.CreateBullect(baseTower);
        AudioMgr.Instance.PlayEffectMusic(baseTower.towerInfo.audio);
    }

    public virtual void Recycle()
    {
        timeVal = 0;//攻击计时器
        isBeginCD = false;
        target = null;

        towerSpriteRender.sprite = FactoryMgr.Instance.GetSprite("Tower/Recycle/" + baseTower.towerInfo.towerId);
    }

    public virtual void GetBullectProperty(Bullect obj)
    {
    }

   
}