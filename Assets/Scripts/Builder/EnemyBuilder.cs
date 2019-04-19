using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EnemyBuilder : IBuilder<BaseEnemy>
{//敌人只需要知道自己的信息 和该回合他的路径点
    public int EnemyId;//敌人编号
    public List<Vector3> enemyPathList;//敌人路径
    //private GameObject enemyGo;

    public void GetData(BaseEnemy productClassGo)
    {
        //EnemyInfoMgr 对敌人类中的参数进行赋值
        productClassGo.enemyInfo = EnemyInfoMgr.Instance.enemyInfoList[EnemyId - 1];
        productClassGo.pathPointList = enemyPathList;
        productClassGo.currentLife = productClassGo.enemyInfo.life;
        productClassGo.CorrectRotate(0);
        productClassGo.Sign.enabled = false;
        //productClassGo.isSetData = true;
    } 

    public void GetOtherResource(BaseEnemy productClassGo)
    {
        throw new NotImplementedException();
    }

    public GameObject GetProduct()
    {
        //获取游戏预制体，GetData,getotherResource
        GameObject go = FactoryMgr.Instance.GetGame(EnemyInfoMgr.Instance.enemyInfoList[EnemyId-1].path);
        BaseEnemy enemy = GetProductClass(go);
        GetData(enemy);
        go.transform.SetParent(GameController.Instance.gameTrans);
        go.transform.position = enemyPathList[0];
        enemy.isSetData = true;
        GameController.Instance.enemyAliveList.Add(go);
        return go;
    }

    public BaseEnemy GetProductClass(GameObject gameObject)
    {
        return gameObject.GetComponent<BaseEnemy>();
    }
}
