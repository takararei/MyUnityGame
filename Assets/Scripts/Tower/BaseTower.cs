using Assets.Framework.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour, IBaseTower
{
    public TowerInfo towerInfo;
    public GameObject bullectGO;
    private CircleCollider2D circleCollider;//范围
    private SpriteRenderer _attackRender;
    public SpriteRenderer attackRangeSr
    {
        get
        {
            if (_attackRender == null)
            {
                _attackRender = UITool.FindChild<SpriteRenderer>(gameObject, "AttackRange");
            }
            return _attackRender;
        }
    }
    public Animator animator;
    public Transform target;
    TowerProperty towerProperty;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        towerProperty = GetComponent<TowerProperty>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (target != null && target.gameObject.activeSelf == false)
        {
            target = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy" && towerProperty.target == null)
        {
            towerProperty.target = other.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "enemy" && towerProperty.target == null)
        {
            towerProperty.target = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (towerProperty.target == other.transform)
        {
            towerProperty.target = null;
        }
    }
}



