using Assets.Framework.Factory;
using Assets.Framework.Tools;
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
            slowSpeed = 1;
            slowTime = 0;
            slowSpeedTimeVal = 0;
            hasSlowSpeed = false;
        }

    }

    public void EnemyMove()
    {
        if (!reachEnd)
        {
            Vector3 pathPoint = pathPointList[roadPointIndex] + new Vector3(pathRangeX, pathRangeY)*0.01f;
            //transform.position = Vector3.Lerp(
            //        transform.position, //起点
            //    pathPointList[roadPointIndex],//终点
            //    1 / Vector3.Distance(transform.position, pathPointList[roadPointIndex]) * Time.deltaTime * enemyInfo.speed * slowSpeed);

            transform.position = Vector3.Lerp(
                    transform.position, //起点
                pathPoint,//终点
                1 / Vector3.Distance(transform.position, pathPoint) * Time.deltaTime * enemyInfo.speed * slowSpeed);

            if(Vector3.Distance(transform.position,pathPoint)<=0.01f)
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
        dieAudioClip = null;
        slowSpeedTimeVal = 0;
        slowTime = 0;
        slowSpeed = 1;
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
        }
        GameController.Instance.currRoundkillNum++;
        ResetEnemy();
    }

    void SlowDebuf(float time)
    {
        slowTime += time;
        hasSlowSpeed = true;
        
    }

    public virtual void TakeDamage(Bullect bullect)
    {
        if (bullect.bsTower.towerInfo.damageType == 1)
        {
            currentLife -= bullect.bsTower.towerInfo.damage;//加上一些加成护甲之类的
        }
        else
        {
            //魔法攻击
            currentLife -= bullect.bsTower.towerInfo.damage;
        }

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
        //switch(itemType)
        //{

        //}
    }
}



