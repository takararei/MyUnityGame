using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EnemyBuilder : IBuilder<BaseEnemy>
{
    public int EnemyId;
    private GameObject enemyGo;

    public void GetData(BaseEnemy productClassGo)
    {
        //EnemyInfoMgr 对敌人类中的参数进行赋值
    }

    public void GetOtherResource(BaseEnemy productClassGo)
    {
        throw new NotImplementedException();
    }

    public GameObject GetProduct()
    {
        //获取游戏预制体，GetData,getotherResource
        //GameObject go=FactoryManager.Instance.GetGame();

        //return go;
        throw new NotImplementedException();
    }

    public BaseEnemy GetProductClass(GameObject gameObject)
    {
        throw new NotImplementedException();
    }
}
