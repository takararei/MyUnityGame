using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TowerBuilder : IBuilder<BaseTower>
{
    private int TowerId;
    private Vector3 pos;
    public GridPoint selectGrid;
    public void GetData(BaseTower productClassGo)
    {
        productClassGo.towerInfo = TowerInfoMgr.Instance.towerInfoList[TowerId - 1];
        productClassGo.AttackRangeSr.enabled = false;
    }

    public void GetOtherResource(BaseTower productClassGo)
    {

    }

    public GameObject GetProduct()
    {
        TowerId = selectGrid.gridState.towerID;
        pos = selectGrid.transform.position;
        GameObject go = FactoryMgr.Instance.GetGame(TowerInfoMgr.Instance.towerInfoList[TowerId - 1].path);
        BaseTower tower = GetProductClass(go);
        GetData(tower);
        go.transform.SetParent(selectGrid.transform);
        go.transform.position = new Vector3(pos.x,pos.y,2);//此处后期转向需要矫正
        go.transform.up = Vector3.up;
        selectGrid.currentTower = go;//格子要持有这个塔
        selectGrid.baseTower = go.GetComponent<BaseTower>();
        return go;
    }

    public BaseTower GetProductClass(GameObject gameObject)
    {
        return gameObject.GetComponent<BaseTower>();
    }
}