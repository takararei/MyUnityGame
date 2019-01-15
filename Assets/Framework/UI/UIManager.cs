using Assets.Framework.Extension;
using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Framework.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private const string UIRootName = "Canvas";
        /// <summary>
        /// 面板实例的位置
        /// </summary>
        #region Transform
        private Transform _canvasTransform;
        private Transform _bgTransform;
        private Transform _commonTransform;
        private Transform _topTransform;
        public Transform CanvasTransform
        {
            get
            {
                if (_canvasTransform == null)
                {
                    _canvasTransform = GameObject.Find(UIRootName).transform;
                }
                return _canvasTransform;
            }
        }

        public Transform BGTransform
        {
            get
            {
                if (_bgTransform == null)
                {
                    _bgTransform = CanvasTransform.Find(UILayer.Background);
                }
                return _bgTransform;
            }
        }

        public Transform CommonTransform
        {

            get
            {
                if (_commonTransform == null)
                {
                    _commonTransform = CanvasTransform.Find(UILayer.Common);
                }
                return _commonTransform;

            }
        }

        public Transform TopTransform
        {
            get
            {
                if(_topTransform==null)
                {
                    _topTransform = CanvasTransform.Find(UILayer.Top);
                }
                return _topTransform;
            }
        }
        #endregion Transform

        public void Reset()
        {
            _canvasTransform = null;
            _bgTransform = null;
            _commonTransform = null;
            _topTransform = null;
            panelDict.Clear();
        }

        /// <summary>
        /// 存储所有面板信息，名称，地址，层级
        /// </summary>
        private Dictionary<string, UIPanelInfo> panelInfoDict;

        /// <summary>
        /// 保存所有被实例化的BasePanel组件,BasePanel
        /// </summary>
        private Dictionary<string, BasePanel> panelDict;

        /// <summary>
        /// 管理所有显示的面板 用栈
        /// </summary>
        private Stack<BasePanel> panelStack;
        private Dictionary<string, BasePanel> panelShowDict;

        /// <summary>
        /// 根据面板类型得到实例化面板
        /// </summary>
        private BasePanel GetPanel(string panelName)
        {
            if (panelDict == null)
            {
                panelDict = new Dictionary<string, BasePanel>();
            }

            BasePanel panel = panelDict.TryGet(panelName);

            if (panel == null)
            {
                //如果找不到 就实例
                UIPanelInfo pInfo = panelInfoDict.TryGet(panelName);
                string path = pInfo.path;
                string layer = pInfo.layer;
                GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                instPanel.name = panelName;
                
                switch (layer)
                {
                    case UILayer.Background:
                        instPanel.transform.SetParent(BGTransform, false );
                        break;
                    case UILayer.Common:
                        instPanel.transform.SetParent(CommonTransform, false);
                        break;
                    case UILayer.Top:
                        instPanel.transform.SetParent(TopTransform, false);
                        break;
                    default:
                        Debug.LogError(pInfo.panelName+"没有设置层级");
                        break;
                }

                //instPanel.transform.SetParent(CanvasTransform, false);

                //TODO
                //panel = instPanel.GetComponent<BasePanel>();
                panel = UIBusiness.GetPanelBusiness(panelName);
                panel.RootUI = instPanel;
                //panel.SetRootUI(instPanel);
                panelDict.Add(panelName, panel);
                panel.Init();

                return panel;

                //panelDict.Add(panelName, instPanel.GetComponent<BasePanel>());
                //return instPanel.GetComponent<BasePanel>();
            }
            else
            {
                return panel;
            }

        }

        /// <summary>
        /// JSON解析对象
        /// </summary>
        [Serializable]
        class UIPanelTypeJson
        {
            public List<UIPanelInfo> infoList;
        }

        /// <summary>
        /// 解析JSON文件
        /// </summary>
        public void ParseUIPanelTypeJson()
        {
            if (panelInfoDict == null)
                panelInfoDict = new Dictionary<string, UIPanelInfo>();

            TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

            UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

            foreach (UIPanelInfo info in jsonObject.infoList)
            {
                panelInfoDict.Add(info.panelName, info);
            }
        }

        public void ParseUIpanelTypeAsset()
        {

        }

        public void Show(string panelName)
        {
            //if (panelStack == null)
            //{
            //    panelStack = new Stack<BasePanel>();
            //}
            ////判断一下是否有正在显示的页面,有就暂停
            //if (panelStack.Count > 0)
            //{
            //    BasePanel topPanel = panelStack.Peek();
            //    topPanel.OnPause();
            //}

            if(panelShowDict==null)
            {
                panelShowDict = new Dictionary<string, BasePanel>();
            }
            BasePanel panel = GetPanel(panelName);
            panel.RootUI.transform.SetAsLastSibling();
            panel.OnShow();
            //panelStack.Push(panel);
            panelShowDict.Add(panelName, panel);
        }

        public void Hide(string panelName)
        {
            //if (panelStack == null)
            //{
            //    panelStack = new Stack<BasePanel>();
            //}

            //if (panelStack.Count <= 0)
            //{
            //    return;
            //}

            //BasePanel topPanel = panelStack.Pop();
            //topPanel.OnHide();

            //if (panelStack.Count <= 0)
            //{
            //    return;
            //}

            //BasePanel topPanelNow = panelStack.Peek();
            //topPanelNow.OnResume();

            if (panelShowDict == null)
            {
                panelShowDict = new Dictionary<string, BasePanel>();
            }

            if (panelShowDict.Count <= 0) return;

            BasePanel panel = panelShowDict.TryGet(panelName);

            if (panel == null) return;

            panel.OnHide();
            panelShowDict.Remove(panelName);
            
        }

        public void Update()
        {
            //if (panelStack.Count == 0) return;
            foreach (KeyValuePair<string,BasePanel> panel in panelShowDict)
            {
                panel.Value.Update();
            }
        }
    }
}
