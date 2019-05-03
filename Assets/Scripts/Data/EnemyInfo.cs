using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class EnemyInfo
{
    public int enemyId;
    public int killCoin;
    public int killDO;//钻石数
    public int life;
    public float speed;
    public int Def;//物理防御
    public int Mdef;//魔法防御
    public string Name;
    public string Introduce;
    public int heart;
    public string path;
    public string audio;
    public string helpSprite;

    public void Init()
    {
        enemyId = 0;
        killCoin = 0;
        killDO = 0;
        life = 0;
        speed = 0;
        Def = 0;
        Mdef = 0;
        Name = null;
        Introduce = null;
        heart = 0;
        path = null;
    }
}