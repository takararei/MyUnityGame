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
    }

}