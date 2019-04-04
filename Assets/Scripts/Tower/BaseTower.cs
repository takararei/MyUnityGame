using Assets.Framework.Factory;
using Assets.Framework.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour, IBaseTower
{
    public TowerInfo towerInfo;
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
    //public Animator animator;
    //public Transform target;
    TowerProperty towerProperty;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        //animator = GetComponent<Animator>();
        towerProperty = GetComponent<TowerProperty>();
        enemyTargetList = new List<Transform>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        if(towerProperty.target == null&&enemyTargetList.Count!=0)
        {
            towerProperty.target = enemyTargetList[0];
        }
        if (towerProperty.target != null && towerProperty.target.gameObject.activeSelf == false)
        {
            enemyTargetList.Remove(towerProperty.target);
            towerProperty.target = null;
        }
    }
    List<Transform> enemyTargetList;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="enemy")
        {
            enemyTargetList.Add(other.transform);
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="enemy")
        {
            enemyTargetList.Remove(other.transform);
            if(towerProperty.target==other.transform)
            {
                towerProperty.target = null;
            }
        }
    }

    public void Recycle()
    {
        towerProperty.Recycle();
        FactoryManager.Instance.PushGame(towerInfo.path, gameObject);
    }
}



