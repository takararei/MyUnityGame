using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AchievementTip : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    float showTimer = 0;
    float showTime = 2;
    float pressTime = 0.2f;
    float pressTimer = 0;

    bool isPress = false;
    bool isShowCount = true;
    Transform canvasTop;
    Vector3 pos;
    private void Awake()
    {
        canvasTop = GameObject.Find("Canvas/Top").transform;
        pos = new Vector3(0, 570, 0);
    }
    private void OnEnable()
    {
        transform.SetParent(canvasTop);
        transform.position = pos;
        isPress = false;
        isShowCount = true;
        pressTimer = 0;
        showTimer = 0;
    }
    private void Update()
    {
        if (isPress)
        {
            if(pressTimer<pressTime)
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
