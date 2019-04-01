using Assets.Framework.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EnemyBuilder : IBuilder<BaseEnemy>
{
    public int EnemyId;
    public List<Vector3> enemyPathList;
    private GameObject enemyGo;

    public void GetData(BaseEnemy productClassGo)
    {
        //EnemyInfoMgr 对敌人类中的参数进行赋值
        productClassGo.enemyInfo = EnemyInfoMgr.Instance.enemyInfoList[EnemyId - 1];
        productClassGo.pathPointList = enemyPathList;
        productClassGo.rAnimator1= FactoryManager.Instance.GetRuntimeAnimatorController("diren" + EnemyId + "_" + 1);
        productClassGo.rAnimator2 = FactoryManager.Instance.GetRuntimeAnimatorController("diren" + EnemyId + "_" + 2);
        productClassGo.rAnimator3 = FactoryManager.Instance.GetRuntimeAnimatorController("diren" + EnemyId + "_" + 3);
        productClassGo.CorrectRotate(0);
        //productClassGo.isSetData = true;
    } 

    public void GetOtherResource(BaseEnemy productClassGo)
    {
        throw new NotImplementedException();
    }

    public GameObject GetProduct()
    {
        //获取游戏预制体，GetData,getotherResource
        GameObject go = FactoryManager.Instance.GetGame(EnemyInfoMgr.Instance.enemyInfoList[EnemyId-1].path);
        BaseEnemy enemy = GetProductClass(go);
        GetData(enemy);
        go.transform.SetParent(GameController.Instance.transform);
        go.transform.position = enemyPathList[0];
        enemy.isSetData = true;
        return go;
    }

    public BaseEnemy GetProductClass(GameObject gameObject)
    {
        return gameObject.GetComponent<BaseEnemy>();
    }
}
