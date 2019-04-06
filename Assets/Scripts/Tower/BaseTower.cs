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

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        towerProperty = GetComponent<TowerProperty>();
        enemyTargetList = new List<Transform>();
    }
    private void Start()
    {

    }
    private void Update()
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
    List<Transform> enemyTargetList;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            if(!enemyTargetList.Contains(other.transform))
            {
                enemyTargetList.Add(other.transform);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            if (!enemyTargetList.Contains(other.transform))
            {
                enemyTargetList.Add(other.transform);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
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
        FactoryManager.Instance.PushGame(towerInfo.path, gameObject);
    }
}



