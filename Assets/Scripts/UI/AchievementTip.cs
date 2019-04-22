using Assets.Framework.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchievementTip : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image mImg;
    public Image imag
    {
        get
        {
            if (mImg == null)
            {
                mImg = UITool.FindChild<Image>(gameObject, "Img_Picture");
            }
            return mImg;
        }

        set
        {
            mImg = value;
        }
    }
    private Text macName;
    public Text acName
    {
        get
        {
            if (macName == null)
            {
                macName = UITool.FindChild<Text>(gameObject, "Txt_Name");
            }
            return macName;
        }

        set
        {
            macName = value;
        }

    }
    private Text macIntroduce;

    public Text acIntroduce
    {
        get
        {
            if (macIntroduce == null)
            {
                macIntroduce = UITool.FindChild<Text>(gameObject, "Txt_Introduce");
            }
            return macIntroduce;
        }

        set
        {
            macIntroduce = value;
        }
    }
    float showTimer = 0;
    float showTime = 2;
    float pressTime = 0.2f;
    float pressTimer = 0;

    bool isPress = false;
    bool isShowCount = true;
    Vector3 pos;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        transform.position = new Vector3(512, 700, 0);
        isPress = false;
        isShowCount = true;
        pressTimer = 0;
        showTimer = 0;
    }
    private void Update()
    {
        if (isPress)
        {
            if (pressTimer < pressTime)
            {
                pressTimer += Time.deltaTime;
            }
            else
            {
                isPress = false;
                isShowCount = false;//确认长按就不再计算
            }

        }
        if (gameObject.activeSelf && isShowCount)
        {
            if (showTimer < showTime)
            {
                showTimer += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                showTimer = 0;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
    }

    public void OnPointerUp(PointerEventData eventData)//抬起
    {
        isPress = false;
        gameObject.SetActive(false);
    }
}
