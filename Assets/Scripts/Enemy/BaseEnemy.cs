using Assets.Framework.Factory;
using Assets.Framework.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class BaseEnemy : MonoBehaviour, IBaseEnemy
{
    public EnemyInfo enemyInfo;
    //public GameObject enemyGO;

    private Animator animator;
    private Slider slider;//血条
    private AudioSource audioSource;
    public List<Vector3> pathPointList;

    public int currentLife;

    public bool isSetData;
    //用于计数的属性或开关
    private int roadPointIndex = 1;
    private bool reachEnd;//到达终点
    private bool hasDecreasSpeed;//是否减速

    private float decreaseSpeedTimeVal;//减速计时器
    private float decreaseTime;//减速持续的具体时间
    private SpriteRenderer _Sign;
    public SpriteRenderer Sign
    {
        get
        {
            if(_Sign==null)
            {
                _Sign= UITool.FindChild<SpriteRenderer>(gameObject, "Sign");
            }
            return _Sign;
        }
    }
    //资源
    public AudioClip dieAudioClip;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        slider = UITool.FindChild<Slider>(gameObject, "HPSlider");
    }


    private void Update()
    {
        if (GameController.Instance.isPause)
        {
            animator.speed = 0;
            return;
        }
        if(!isSetData)
        {
            return;
        }
        animator.speed = 1;
        if (!reachEnd)
        {
            transform.position = Vector3.Lerp(
                    transform.position, //起点
                pathPointList[roadPointIndex],//终点
                1 / Vector3.Distance(transform.position, pathPointList[roadPointIndex]) * Time.deltaTime * enemyInfo.speed);

            if (Vector3.Distance(transform.position, pathPointList[roadPointIndex]) <= 0.01f)
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
        if(EventSystem.current.IsPointerOverGameObject())
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
            //rA = rAnimator1;
            animator.Play("Right");
        }
        else if (xOffset > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            //rA = rAnimator1;
            animator.Play("Right");
        }
        else if (yOffset > 0)
        {
            //正面走的动画
            //rA = rAnimator2;
            animator.Play("Front");
        }
        else if (yOffset < 0)
        {
            //背面走的动画
            //rA = rAnimator3;
            animator.Play("Back");
        }

        //if (animator.runtimeAnimatorController != rA)
        //{
        //    animator.runtimeAnimatorController = rA;
        //}
        slider.gameObject.transform.eulerAngles = Vector3.zero;
    }

    public void ResetEnemy()
    {
        reachEnd = false;
        isSetData = false;
        hasDecreasSpeed = false;
        roadPointIndex = 1;
        slider.value = 1;
        transform.eulerAngles = Vector3.zero;
        dieAudioClip = null;
        decreaseSpeedTimeVal = 0;
        decreaseTime = 0;
        _Sign.enabled = false;
        //CancelDecreaseDebuff();

        FactoryManager.Instance.PushGame(enemyInfo.path, gameObject);
    }

    public void DestroyEnemy()
    {
        if(!reachEnd)
        {
            //被玩家杀死 处理一些金币等
            GameController.Instance.ChangeCoin(enemyInfo.killCoin);
            //钻石
        }
        GameController.Instance.currRoundkillNum++;
        ResetEnemy();
    }

    public void TakeDamage(Bullect bullect)
    {
        if(bullect.towerInfo.damageType==1)
        {
            currentLife -= bullect.towerInfo.damage;//加上一些加成护甲之类的
        }
        else
        {
            //魔法攻击
            currentLife -= bullect.towerInfo.damage;
        }
        
        if(currentLife<=0)
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

    void Recycle()
    {
        ResetEnemy();
    }
}



