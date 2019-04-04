using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class TowerProperty:MonoBehaviour
{
    public Transform target;
    private BaseTower baseTower;
    private Animator animator;
    protected float timeVal;//攻击计时器
    bool isBeginCD;
    GameObject bullectGO;
    private void Awake()
    {
        baseTower = GetComponent<BaseTower>();
        animator = GetComponent<Animator>();
        
    }
    
    private void Update()
    {
        if(isBeginCD)
        {
            if(timeVal>=baseTower.towerInfo.CD)
            {
                timeVal = 0;
                isBeginCD = false;
            }
            else
            {
                timeVal += Time.deltaTime;
            }
        }

        if(target==null||GameController.Instance.isPause==true)
        {
            return;
        }
        Vector3 targetPos=new Vector3(target.position.x, target.position.y, 2);
        transform.up = targetPos - transform.position;

        if(isBeginCD==false)
        {
            Attack();
        }


    }

    protected virtual void Attack()
    {
        if(target==null)
        {
            return;
        }
        animator.Play("Attack");
        isBeginCD = true;
        bullectGO = FactoryManager.Instance.GetGame(baseTower.towerInfo.bullectPath);
        bullectGO.transform.position = transform.position-new Vector3(0,0,2);
        Bullect bu = bullectGO.GetComponent<Bullect>();
        bu.targetTrans = target;
        bu.towerInfo = baseTower.towerInfo;
        //bu.towerID = baseTower.towerInfo.towerId;
        //bu.attackValue = baseTower.towerInfo.damage;
        //bu.attackType = baseTower.towerInfo.damageType;
        bu.isSetData = true;
    }

}