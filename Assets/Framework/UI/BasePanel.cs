using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.UI
{
    public class BasePanel: IBasePanel
    {
        protected GameObject mRootUI;
        protected CanvasGroup canvasGroup;
        protected GameObject UICanvas;
        
        public virtual void Init()
        {
            UICanvas = UIManager.GetInstance().CanvasTransform.gameObject;
        }

        protected CanvasGroup GetCanvasGroup()
        {
            canvasGroup = mRootUI.transform.GetComponent<CanvasGroup>();
            if(canvasGroup==null)
            {
                Debug.Log(mRootUI.name+"没有canvasGroup组件");
            }
            return canvasGroup;
        }
        

        public virtual void Update()
        {

        }

        
        public virtual void OnShow()
        {
            mRootUI.SetActive(true);
        }
        
        public virtual void OnPause()
        {
        }
        
        public virtual void OnResume()
        {
        }
        
        public virtual void OnHide()
        {
            mRootUI.SetActive(false);
        }
    }
}
