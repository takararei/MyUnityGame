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
    private void Awake()
    {
        baseTower = GetComponent<BaseTower>();
        animator = GetComponent<Animator>();
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
        animator.Play("Attack");
        isBeginCD = true;
        GameController.Instance.CreateBullect(baseTower);
        //bullectGO = FactoryManager.Instance.GetGame(baseTower.towerInfo.bullectPath);//可以考虑做成BullectBuilderTODO
        //bullectGO.transform.SetParent(GameController.Instance.gameTrans);
        //bullectGO.transform.position = transform.position - new Vector3(0, 0, 2);
        //bullectGO.transform.right = Vector3.right;
        //Bullect bu = bullectGO.GetComponent<Bullect>();
        //bu.targetTrans = target;
        //bu.towerInfo = baseTower.towerInfo;
        //bu.isSetData = true;
    }

    public virtual void Recycle()
    {
        timeVal = 0;//攻击计时器
        isBeginCD = false;
        target = null;
    }

    public virtual void GetBullectProperty(Bullect obj)
    {
    }

   
}