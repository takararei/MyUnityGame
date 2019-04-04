using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class TowerProperty : MonoBehaviour
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
        if (target == null || GameController.Instance.isPause == true)
        {
            return;
        }
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

        Vector3 targetPos = new Vector3(target.position.x, target.position.y, 2);
        transform.up = targetPos - transform.position;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        if (isBeginCD == false)
        {
            Attack();
        }


    }

    protected virtual void Attack()
    {
        if (target == null)
        {
            return;
        }
        animator.Play("Attack");
        isBeginCD = true;
        bullectGO = FactoryManager.Instance.GetGame(baseTower.towerInfo.bullectPath);
        bullectGO.transform.SetParent(GameController.Instance.gameTrans);
        bullectGO.transform.position = transform.position - new Vector3(0, 0, 2);
        bullectGO.transform.right = Vector3.right;
        Bullect bu = bullectGO.GetComponent<Bullect>();
        bu.targetTrans = target;
        bu.towerInfo = baseTower.towerInfo;
        bu.isSetData = true;
    }

    public void Recycle()
    {
        timeVal = 0;//攻击计时器
        isBeginCD = false;
        target = null;
    }
}