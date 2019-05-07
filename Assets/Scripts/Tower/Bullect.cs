using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Bullect : MonoBehaviour
{
    [HideInInspector]
    protected Animator animator;
    protected float moveSpeed = 1;
    [HideInInspector]
    public bool isSetData;
    public BaseTower bsTower;
    [HideInInspector]
    public Transform target;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //public void SetData(BaseTower baseTower)
    //{
    //    this.bsTower = baseTower;
    //    targetTrans = bsTower.towerProperty.target;
    //}
    protected virtual void Update()
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
        if (!target.gameObject.activeSelf || (bsTower.towerProperty.target != target)||bsTower.towerProperty.target == null || !bsTower.towerProperty.target.gameObject.activeSelf)//|| GameController.Instance.isGameOver)
        {
            DestoryBullect();
            return;
        }
        animator.speed = 1;
        BullectMove();
    }

    protected virtual void BullectMove()
    {
        transform.position = Vector3.Lerp(transform.position, bsTower.towerProperty.target.position,
              1 / Vector3.Distance(transform.position, bsTower.towerProperty.target.position) * Time.deltaTime * moveSpeed * 10);
        transform.right = bsTower.towerProperty.target.position - transform.position;

    }

    protected virtual void DestoryBullect()
    {
        animator.speed = 0;
        //bsTower.towerProperty.target = null;
        if (gameObject.activeSelf == true)//未被回收
        {
            FactoryMgr.Instance.PushGame(bsTower.towerInfo.bullectPath, gameObject);
        }
        isSetData = false;
    }

    void CreateEffect()
    {

    }

    protected virtual void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy" && other.gameObject.activeSelf)
        {
            //敌人调用自身的受伤函数
            other.SendMessage("TakeDamage", this);
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