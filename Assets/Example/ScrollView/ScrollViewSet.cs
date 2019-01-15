using Assets.Framework.Extension;
using Assets.Framework.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollViewSet : MonoBehaviour {
    public Button left;
    public Button Right;
    private GridLayoutGroup glg;
    private RectTransform contentRect;
    private ScrollRect scrollRect;
    public bool isColumn = false;
    // Use this for initialization
    void Start() {
        scrollRect = GetComponent<ScrollRect>();
        glg = GetComponentInChildren<GridLayoutGroup>();
        
        //contentRect = GameObject.Find("Content").transform as RectTransform;
        //contentRect.offsetMax = new Vector2((contentRect.childCount - 1) * (glg.cellSize.x + glg.spacing.x), 0);
        if(isColumn)
        {
            //SetContentY(scrollRect, glg);
            scrollRect.ContentAdaptiveY(glg);
            left.onClick.AddListener(OnUp);
            Right.onClick.AddListener(OnDown);
        }
        else
        {
            //SetContentX(scrollRect, glg);
            scrollRect.ContentAdaptiveX(glg);
            left.onClick.AddListener(OnLeft);
            Right.onClick.AddListener(OnRight);
        }

    }
    public void OnDown()
    {
  
        //float contentLength = contentRect.rect.height - 2*glg.padding.top-glg.cellSize.y;
        //float move = scrollRect.verticalNormalizedPosition - (glg.cellSize.y + glg.spacing.y) / contentLength;
        float move = scrollRect.verticalNormalizedPosition - scrollRect.RatioUp(glg);

        if (move <= 0.01)
        {
            scrollRect.verticalNormalizedPosition = 0;
        }
        else
        {

            scrollRect.verticalNormalizedPosition = move;
        }
    }

    public void OnUp()
    {
        //float contentLength = contentRect.rect.height - 2 * glg.padding.top - glg.cellSize.y;
        float move = scrollRect.verticalNormalizedPosition + scrollRect.RatioUp(glg);

        if (move>= 0.99)
        {
            scrollRect.verticalNormalizedPosition = 1;
        }
        else
        {

            scrollRect.verticalNormalizedPosition = move;
        }
    }
    public void OnLeft()
    {
        float contentLength =
            scrollRect.content.rect.xMax - 2 * glg.padding.left - glg.cellSize.x;
        float move= scrollRect.horizontalNormalizedPosition - (glg.cellSize.x + glg.spacing.x) / contentLength;
        if(move<=0.01)
        {
            scrollRect.horizontalNormalizedPosition = 0;
        }
        else
        {

            scrollRect.horizontalNormalizedPosition = move;
        }
    }

    public void OnRight()
    {
        
        float contentLength =
           scrollRect.content.rect.xMax - 2 * glg.padding.left - glg.cellSize.x;
        float move= scrollRect.horizontalNormalizedPosition + (glg.cellSize.x + glg.spacing.x) / contentLength;
        if(move>=0.99)
        {
            scrollRect.horizontalNormalizedPosition = 1;
        }
        else
        {
            scrollRect.horizontalNormalizedPosition = move;
        }
        
    }


    /// <summary>
    /// 设置ScrollView中Content的Right值，仅宽度
    /// </summary>
    /// <param name="scrollRect"></param>
    /// <param name="gridLayoutGroup"></param>
    void SetContentX(ScrollRect scrollRect, GridLayoutGroup gridLayoutGroup)
    {
        float rightLength = (scrollRect.content.childCount-1) * (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x);
        scrollRect.content.sizeDelta = new Vector2(rightLength, scrollRect.content.sizeDelta.y);
    }
    /// <summary>
    /// 设置ScrollView中Content的Height值，仅高度
    /// </summary>
    /// <param name="scrollRect"></param>
    /// <param name="gridLayoutGroup"></param>
    void SetContentY(ScrollRect scrollRect, GridLayoutGroup gridLayoutGroup)
    {
        float offset = (scrollRect.transform as RectTransform).sizeDelta.y-300;
        float heightLength = (scrollRect.content.childCount) * (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);
        scrollRect.content.sizeDelta = new Vector2(0, heightLength+offset);
    }

    void SetContent(ScrollRect scrollRect, GridLayoutGroup gridLayoutGroup)
    {

    }
	// Update is called once per frame
	void Update () {
		
	}
}
