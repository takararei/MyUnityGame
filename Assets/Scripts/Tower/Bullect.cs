using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Bullect:MonoBehaviour
{
    public Transform targetTrans;
    public TowerInfo towerInfo;
    public Transform towerTrans;
    protected Animator animator;
    public float moveSpeed=1;
    public bool isSetData;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //if(GameController.Instance.isGameOver)
        //{
        //    DestoryBullect();
        //    return;
        //}
        if (GameController.Instance.isPause||!isSetData)
        {
            animator.speed = 0;
            return;
        }
            
        //如果飞到一半突然物体已经消失 则子弹也消失
        if (targetTrans == null || !targetTrans.gameObject.activeSelf)//|| GameController.Instance.isGameOver)
        {
            DestoryBullect();
            return;
        }
        animator.speed = 1;
        BullectMove();
    }

    protected virtual void BullectMove()
    {
        transform.position = Vector3.Lerp(transform.position, targetTrans.position,
              1 / Vector3.Distance(transform.position, targetTrans.position * Time.deltaTime * moveSpeed));
        transform.right = targetTrans.position - transform.position;
    }

    protected virtual void DestoryBullect()
    {
        targetTrans = null;
        if(gameObject.activeSelf==true)//未被回收
        {
            FactoryManager.Instance.PushGame(towerInfo.bullectPath,gameObject);
        }
        isSetData = false;
    }

    void CreateEffect()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="enemy"&&other.gameObject.activeSelf)
        {
            //敌人调用自身的受伤函数
            other.SendMessage("TakeDamage",this);
            //爆炸特效等
            CreateEffect();
            DestoryBullect();
        }
    }
    
    void Recycle()
    {
        DestoryBullect();
    }


}