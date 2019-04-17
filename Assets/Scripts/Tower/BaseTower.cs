using Assets.Framework.Factory;
using Assets.Framework.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour, IBaseTower
{
    public TowerInfo towerInfo;
    protected CircleCollider2D _circleCollider;//范围
    public CircleCollider2D CirecleCollider
    {
        get
        {
            if (_circleCollider == null)
            {
                _circleCollider = gameObject.GetComponent<CircleCollider2D>();
            }
            return _circleCollider;
        }
    }

    protected SpriteRenderer _attackRender;
    public SpriteRenderer AttackRangeSr
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
    [HideInInspector]
    public TowerProperty towerProperty;

    protected void Awake()
    {
        towerProperty = GetComponent<TowerProperty>();
        enemyTargetList = new List<Transform>();
    }
    protected void Start()
    {

    }
    protected void Update()
    {
        if (towerProperty.target == null && enemyTargetList.Count != 0)
        {
            towerProperty.target = enemyTargetList[0];
        }
        if (towerProperty.target != null && towerProperty.target.gameObject.activeSelf == false)
        {
            enemyTargetList.Remove(towerProperty.target);
            towerProperty.target = null;
        }
    }
    public List<Transform> enemyTargetList;
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            if(!enemyTargetList.Contains(other.transform))
            {
                enemyTargetList.Add(other.transform);
            }
        }

    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            if (!enemyTargetList.Contains(other.transform))
            {
                enemyTargetList.Add(other.transform);
            }
        }
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            enemyTargetList.Remove(other.transform);
            if (towerProperty.target == other.transform)
            {
                towerProperty.target = null;
            }
        }
    }

    public void Recycle()
    {
        towerProperty.Recycle();
        enemyTargetList.Clear();
        FactoryMgr.Instance.PushGame(towerInfo.path, gameObject);
    }

}



