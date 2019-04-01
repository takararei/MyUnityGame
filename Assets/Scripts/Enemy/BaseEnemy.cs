using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
}
[System.Serializable]
public class EnemyInfoMgr : ScriptableObject
{
    public List<EnemyInfo> enemyInfoList;
    private static EnemyInfoMgr _instance;
    public static EnemyInfoMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<EnemyInfoMgr>("AssetData/EnemyInfo");
            }
            return _instance;
        }
    }
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class BaseEnemy : MonoBehaviour,IBaseEnemy
{
    public EnemyInfo enemyInfo;
    //public GameObject enemyGO;

    private Animator animator;
    private Slider slider;//血条
    private AudioSource audioSource;
    public List<Vector3> pathPointList;

    int currentLife;

    //用于计数的属性或开关
    private int roadPointIndex = 1;
    private bool reachEnd;//到达终点
    private bool hasDecreasSpeed;//是否减速

    private float decreaseSpeedTimeVal;//减速计时器
    private float decreaseTime;//减速持续的具体时间

    //资源
    public AudioClip dieAudioClip;
    public RuntimeAnimatorController runtimeAnimatorController;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if(GameController.Instance.isPause)
        {
            return;
        }
    }
}



