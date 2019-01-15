using Assets.Framework.Extension;
using Assets.Framework.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollViewSet : MonoBehaviour {

    private GridLayoutGroup glg;
    private RectTransform contentRect;
    private ScrollRect scrollRect;
    public bool isColumn = false;
    // Use this for initialization
    void Start() {
        scrollRect = GetComponent<ScrollRect>();
        glg = GetComponentInChildren<GridLayoutGroup>();
        contentRect = GameObject.Find("Content").transform as RectTransform;
        //contentRect.offsetMax = new Vector2((contentRect.childCount - 1) * (glg.cellSize.x + glg.spacing.x), 0);
        if(isColumn)
        {
            //SetContentY(scrollRect, glg);
            scrollRect.ContentAdaptiveY(glg);
        }
        else
        {
            //SetContentX(scrollRect, glg);
            scrollRect.ContentAdaptiveX(glg);
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
