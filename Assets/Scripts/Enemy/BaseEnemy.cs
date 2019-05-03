using Assets.Framework.Audio;
using Assets.Framework.Factory;
using Assets.Framework.Tools;
using Assets.Framework.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class BaseEnemy : MonoBehaviour, IBaseEnemy
{
    public EnemyInfo enemyInfo;
    //public GameObject enemyGO;

    private Animator animator;
    private Slider slider;//血条
    //private AudioSource audioSource;
    public List<Vector3> pathPointList;

    public int currentLife;

    public bool isSetData;
    //用于计数的属性或开关
    private int roadPointIndex = 1;
    private bool reachEnd;//到达终点

    private bool hasSlowSpeed;//是否减速
    private float slowSpeedTimeVal;//减速计时器
    private float slowTime;//减速持续的具体时间
    private bool hasFreeze;
    private float freezeTimeVal;
    private float freezeTime;

    private float slowSpeed = 1;
    private SpriteRenderer _Sign;
    public SpriteRenderer Sign
    {
        get
        {
            if (_Sign == null)
            {
                _Sign = UITool.FindChild<SpriteRenderer>(gameObject, "Sign");
            }
            return _Sign;
        }
    }
    //资源
    protected AudioClip dieAudioClip;
    protected int pathRangeX;
    protected int pathRangeY;

    float itemPhy = 1;
    float itemMagic = 1;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
        slider = UITool.FindChild<Slider>(gameObject, "HPSlider");
        pathRangeX = Random.Range(-20, 20);
        pathRangeY = Random.Range(-20, 20);
    }

    private void Update()
    {
        if (GameController.Instance.isPause)
        {
            animator.speed = 0;
            return;
        }
        if (!isSetData)
        {
            return;
        }
        animator.speed = 1;

        EnemyMove();

        if (hasSlowSpeed && slowSpeedTimeVal < slowTime)
        {
            slowSpeed = 0.5f;
            slowSpeedTimeVal += Time.deltaTime;
        }
        else
        {
            if (!hasFreeze)
            {
                slowSpeed = 1;
            }
            slowTime = 0;
            slowSpeedTimeVal = 0;
            hasSlowSpeed = false;
        }

        if (hasFreeze && freezeTimeVal < freezeTime)
        {
            slowSpeed = 0;
            freezeTimeVal += Time.deltaTime;
        }
        else
        {
            if (!hasSlowSpeed)
            {
                slowSpeed = 1;
            }
            freezeTime = 0;
            freezeTimeVal = 0;
            hasFreeze = false;
        }

    }

    public void EnemyMove()
    {
        if (!reachEnd)
        {
            Vector3 pathPoint = pathPointList[roadPointIndex] + new Vector3(pathRangeX, pathRangeY) * 0.01f;
            //transform.position = Vector3.Lerp(
            //        transform.position, //起点
            //    pathPointList[roadPointIndex],//终点
            //    1 / Vector3.Distance(transform.position, pathPointList[roadPointIndex]) * Time.deltaTime * enemyInfo.speed * slowSpeed);

            transform.position = Vector3.Lerp(
                    transform.position, //起点
                pathPoint,//终点
                1 / Vector3.Distance(transform.position, pathPoint) * Time.deltaTime * enemyInfo.speed * slowSpeed*0.4f);

            if (Vector3.Distance(transform.position, pathPoint) <= 0.01f)
            //if (Vector3.Distance(transform.position, pathPointList[roadPointIndex]) <= 0.01f)
            {

                //确定下一点存在
                if (roadPointIndex + 1 < pathPointList.Count)
                {
                    //通过正负值来决定是否转向
                    CorrectRotate(roadPointIndex);
                    roadPointIndex++;
                }
                else
                {
                    reachEnd = true;
                }
            }
        }
        else
        {
            DestroyEnemy();
            //玩家总生命值减少
            GameController.Instance.ChangeLife(-enemyInfo.heart);
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        EventCenter.Broadcast(EventType.HandleEnemy, this);

    }

    public void CorrectRotate(int index)
    {
        //通过正负值来决定是否转向
        float xOffset = pathPointList[index].x - pathPointList[index + 1].x;
        float yOffset = pathPointList[index].y - pathPointList[index + 1].y;

        if (xOffset < 0)//右走
        {//播放左右走的动画
            transform.eulerAngles = new Vector3(0, 0, 0);
            animator.Play("Right");
        }
        else if (xOffset > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            animator.Play("Right");
        }
        else if (yOffset > 0)
        {
            //正面走的动画
            animator.Play("Front");
        }
        else if (yOffset < 0)
        {
            //背面走的动画
            animator.Play("Back");
        }

        slider.gameObject.transform.eulerAngles = Vector3.zero;
    }
    //重置资源
    public virtual void ResetEnemy()
    {
        reachEnd = false;
        isSetData = false;
        hasSlowSpeed = false;
        roadPointIndex = 1;
        slider.value = 1;
        transform.eulerAngles = Vector3.zero;

        slowSpeedTimeVal = 0;
        slowTime = 0;
        slowSpeed = 1;
        hasFreeze = false;
        freezeTimeVal = 0;
        freezeTime = 0;

        _Sign.enabled = false;
        //CancelDecreaseDebuff();

        FactoryMgr.Instance.PushGame(enemyInfo.path, gameObject);
        GameController.Instance.enemyAliveList.Remove(gameObject);
    }
    //敌人被杀死时的处理
    protected void DestroyEnemy()
    {
        if (!reachEnd)
        {
            //被玩家杀死 处理一些金币等
            GameController.Instance.ChangeCoin(enemyInfo.killCoin);
            //钻石
            AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.FirstKill, 1);
            AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.Kill_100, 1);
            AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.Kill_1000, 1);
            AudioMgr.Instance.PlayEffectMusic(enemyInfo.audio);
        }
        else//到达终点
        {
            AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.Enemy_ReachEnd, 1);
            AudioMgr.Instance.PlayEffectMusic(StringMgr.ReachEnd);
        }
        EventCenter.Broadcast(EventType.RemoveEnemy, transform);
        GameController.Instance.currRoundkillNum++;
        ResetEnemy();
    }
    //减速处理
    void SlowDebuf(float time)
    {
        slowTime += time;
        slowSpeed = 0.5f;
        hasSlowSpeed = true;
    }
    //冻结处理
    void FreezeDebuf(float time)
    {
        freezeTime += time;
        slowSpeed = 0f;
        hasFreeze = true;
    }
    //处理伤害
    public virtual void TakeDamage(Bullect bullect)
    {
        int damageType = bullect.bsTower.towerInfo.damageType;
        int damage = bullect.bsTower.towerInfo.damage;
        if (damageType == 1)
        {
            damage -= (int)(damage * enemyInfo.Def * 0.1f);//加上一些加成护甲之类的
            damage = (int)(damage * itemPhy);
        }
        else if (damageType == 2)
        {
            damage -= (int)(damage * enemyInfo.Mdef * 0.1f);
            damage = (int)(damage * itemMagic);
        }
        currentLife -= damage;
        if (currentLife <= 0)
        {
            //死亡的一些效果
            DestroyEnemy();
            return;
        }
        slider.value = (float)currentLife / enemyInfo.life;
    }

    public void SetEnemySign(bool isEnabled)
    {
        _Sign.enabled = isEnabled;
    }
    //外部的，重新开始游戏时调用的
    void Recycle()
    {
        ResetEnemy();
    }

    //使用道具
    void OnItemEffect(int itemType)
    {
        float time = ItemInfoMgr.instance.itemInfoList[itemType].time;
        switch (itemType)
        {
            case 2:
                itemPhy = 1.5f;
                //计时
                GameTimer.Instance.AddTimeTask(time, () =>
                 {
                     itemPhy = 1;
                 });
                break;
            case 3:
                itemMagic = 1.5f;
                GameTimer.Instance.AddTimeTask(time, () =>
                {
                    itemMagic = 1;
                });
                //计时
                break; 
            case 4:
                FreezeDebuf(time);
                break; 
        }
    }
}



