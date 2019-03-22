using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour,IBaseEnemy
{
    public int enemyId;
    public int killCoin;
    public int killDO;//钻石数
    public int life;
    public float Speed;
    public int Def;//物理防御
    public int Mdef;//魔法防御
    public GameObject enemyGO;
}
