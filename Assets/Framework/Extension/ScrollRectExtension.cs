using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Framework.Extension
{
    public static class ScrollRectExtension
    {
        /// <summary>
        /// Conten宽自适应
        /// </summary>
        /// <param name="scrollRect"></param>
        /// <param name="grid"></param>
        public static void ContentAdaptiveX(this ScrollRect scrollRect,GridLayoutGroup grid)
        {
            float rightLength = (scrollRect.content.childCount - 1) * (grid.cellSize.x + grid.spacing.x);
            scrollRect.content.sizeDelta = new Vector2(rightLength, scrollRect.content.sizeDelta.y);
        }
        /// <summary>
        /// Conten高自适应
        /// </summary>
        /// <param name="scrollRect"></param>
        /// <param name="grid"></param>
        public static void ContentAdaptiveY(this ScrollRect scrollRect, GridLayoutGroup grid)
        {
            float offset = (scrollRect.transform as RectTransform).sizeDelta.y - 300;
            float heightLength = scrollRect.content.childCount * (grid.cellSize.y + grid.spacing.y);
            scrollRect.content.sizeDelta = new Vector2(0, heightLength + offset);
        }
        /// <summary>
        /// Conten宽高自适应
        /// </summary>
        /// <param name="scrollRect"></param>
        /// <param name="grid"></param>
        /// <param name="Count">一页单元格数量</param>
        public static void ContentAdaptive(this ScrollRect scrollRect, GridLayoutGroup grid,int Count)
        {
            int pageNum=scrollRect.content.childCount/Count;
            if(scrollRect.content.childCount % Count != 0)
            {
                pageNum++;
            }
            float rightLength = (pageNum - 1) * (grid.cellSize.x + grid.spacing.x);
            float offset = (scrollRect.transform as RectTransform).sizeDelta.y - 300;
            float heightLength = pageNum * (grid.cellSize.y + grid.spacing.y);
            scrollRect.content.sizeDelta = new Vector2(rightLength, heightLength + offset);
        }
    }
}
