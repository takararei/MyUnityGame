using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.UI
{
    public class BasePanel:MonoBehaviour
    {
        protected CanvasGroup canvasGroup;
        /// <summary>
        /// 处理一些初始化,如按钮注册
        /// </summary>
        public virtual void Init()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        public virtual void Start()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        public virtual void Update()
        {

        }

        /// <summary>
        /// 界面显示
        /// </summary>
        public virtual void OnShow()
        {
            //if (canvasGroup == null)
            //{
            //    canvasGroup = GetComponent<CanvasGroup>();
            //}
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// 界面暂停交互
        /// </summary>
        public virtual void OnPause()
        {
            canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// 界面重启交互
        /// </summary>
        public virtual void OnResume()
        {
            canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// 界面关闭
        /// </summary>
        public virtual void OnHide()
        {
            
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
