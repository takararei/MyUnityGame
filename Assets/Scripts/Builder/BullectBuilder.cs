using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BullectBuilder : IBuilder<Bullect>
{
    public BaseTower baseTower;
    public void GetData(Bullect productClassGo)
    {
        productClassGo.bsTower = baseTower;
        productClassGo.targetTrans = baseTower.towerProperty.target;
        //productClassGo.towerInfo = baseTower.towerInfo;
        //productClassGo.towerTrans = baseTower.transform;
        productClassGo.isSetData = true;
    }

    public void GetOtherResource(Bullect productClassGo)
    {
        baseTower.towerProperty.GetBullectProperty(productClassGo);
    }

    public GameObject GetProduct()
    {
        GameObject go = FactoryManager.Instance.GetGame(baseTower.towerInfo.bullectPath);
        Bullect bu = GetProductClass(go);
        go.transform.SetParent(GameController.Instance.gameTrans);
        //go.transform.position = baseTower.towerProperty.bullectBornTrans.position - new Vector3(0, 0, 2);
        go.transform.position = baseTower.transform.position - new Vector3(0, 0, 2);
        go.transform.right = Vector3.right;
        go.transform.up = Vector3.up;
        GetOtherResource(bu);
        GetData(bu);
        return go;
    }

    public Bullect GetProductClass(GameObject gameObject)
    {
        return gameObject.GetComponent<Bullect>();
    }
}