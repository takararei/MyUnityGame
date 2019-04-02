using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TowerBuilder : IBuilder<BaseTower>
{
    public int TowerId;
    public Vector3 pos;
    public void GetData(BaseTower productClassGo)
    {

    }

    public void GetOtherResource(BaseTower productClassGo)
    {

    }

    public GameObject GetProduct()
    {
        GameObject go = FactoryManager.Instance.GetGame(TowerInfoMgr.Instance.towerInfoList[TowerId - 1].path);
        BaseTower tower = GetProductClass(go);
        GetData(tower);
        go.transform.SetParent(GameController.Instance.selectGrid.transform);
        go.transform.position = new Vector3(pos.x,pos.y,2);//此处后期转向需要矫正
        return go;
    }

    public BaseTower GetProductClass(GameObject gameObject)
    {
        return gameObject.GetComponent<BaseTower>();
    }
}