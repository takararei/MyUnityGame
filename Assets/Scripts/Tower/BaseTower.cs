using Assets.Framework.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour,IBaseTower
{
    public TowerInfo towerInfo;
    //public GameObject towerGO;
    public GameObject bullectGO;
    public CircleCollider2D circleCollider;//范围
    private SpriteRenderer attackRangeSr;//攻击范围渲染
    public Animator animator;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        SpriteRenderer attackRangeSr = UITool.FindChild<SpriteRenderer>(gameObject, "AttackRange");
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}



