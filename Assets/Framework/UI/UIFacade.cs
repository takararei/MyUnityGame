using Assets.Framework.Extension;
using Assets.Framework.Factory;
using Assets.Framework.SceneState;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Framework.UI
{
    public class UIFacade
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
                if (_topTransform == null)
                {
                    _topTransform = CanvasTransform.Find(UILayer.Top);
                }
                return _topTransform;
            }
        }
        #endregion Transform

        //其他成员变量
        private GameObject mask;
        private Image maskImage;

        /// <summary>
        /// 存储所有面板信息，名称，地址，层级
        /// </summary>
        private Dictionary<string, UIPanelInfo> panelInfoDict;

        /// <summary>
        /// 保存所有被实例化的BasePanel组件,BasePanel
        /// </summary>
        private Dictionary<string, BasePanel> panelDict;

        /// <summary>
        /// 管理所有显示的面板
        /// </summary>
        private Dictionary<string, BasePanel> panelShowDict;

        public UIFacade()
        {
            InitMask();
        }

        public void ClearPanelDict()
        {
            panelShowDict.Clear();
            panelDict.Clear();
            UIManager.Instance.ClearDict();
        }

        #region 解析UIPanelData
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
            if (panelInfoDict == null)
            {
                panelInfoDict = new Dictionary<string, UIPanelInfo>();
            }

            UIDataMgr uiMgr = Resources.Load<UIDataMgr>("AssetData/UIPanelData");
            foreach (UIPanelInfo info in uiMgr.PanelInfoList)
            {
                panelInfoDict.Add(info.panelName, info);
            }
        }
        #endregion

        /// <summary>
        /// 根据面板类型得到实例化面板
        /// </summary>
        public BasePanel GetPanel(string panelName)
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
                if (pInfo == null)
                {
                    Debug.LogError("没有该面板的信息" + panelName);
                    return null;
                }
                GameObject instPanel=UIManager.Instance.currentScenePanelDict.TryGet(panelName);    
                
                if (instPanel == null)
                {
                    instPanel = GameObject.Instantiate(Resources.Load(pInfo.path)) as GameObject;
                    instPanel.name = panelName;
                    UIManager.Instance.currentScenePanelDict.Add(panelName, instPanel);
                }
                
                switch (pInfo.layer)
                {
                    case UILayer.Background:
                        instPanel.transform.SetParent(BGTransform, false);
                        break;
                    case UILayer.Common:
                        instPanel.transform.SetParent(CommonTransform, false);
                        break;
                    case UILayer.Top:
                        instPanel.transform.SetParent(TopTransform, false);
                        break;
                    default:
                        Debug.LogError(pInfo.panelName + "没有设置层级");
                        break;
                }
                instPanel.transform.ResetLocal();
                
                panel = UIBusiness.GetPanelBusiness(panelName);
                panel.RootUI = instPanel;

                panelDict.Add(panelName, panel);
                panel.Init();
            }
            return panel;
        }

        public void Show(string panelName)
        {
            if (panelShowDict == null)
            {
                panelShowDict = new Dictionary<string, BasePanel>();
            }

            BasePanel panel = GetPanel(panelName);
            panel.RootUI.transform.SetAsLastSibling();
            panel.OnShow();

            if (!panelShowDict.ContainsKey(panelName))
                panelShowDict.Add(panelName, panel);
        }

        public void Hide(string panelName)
        {
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
            if (panelShowDict.Count == 0) return;
            foreach (KeyValuePair<string, BasePanel> panel in panelShowDict)
            {
                panel.Value.Update();
            }
        }

        public IBaseSceneState lastSceneState;
        public IBaseSceneState currentSceneState;

        #region 遮罩Mask
        //显示遮罩
        private void ShowMask()
        {
            mask.transform.SetSiblingIndex(10);
            Tween t =
                DOTween.To(() => maskImage.color,
                toColor => maskImage.color = toColor,
                new Color(0, 0, 0, 1),
                2f);
            t.OnComplete(ExitSceneComplete);
        }

        private void HideMask()
        {
            DOTween.To(() => maskImage.color,
                toColor => maskImage.color = toColor,
                new Color(0, 0, 0, 0),
                2f);
        }

        public void InitMask()
        {
            mask = CreateUIAndSetUIPosition("Img_Mask");
            maskImage = mask.GetComponent<Image>();
        }
        #endregion 遮罩Mask

        //UI部分
        public GameObject CreateUIAndSetUIPosition(string uiName)
        {
            GameObject itemGo = FactoryManager.Instance.GetUI(uiName);
            itemGo.transform.SetParent(CanvasTransform);
            itemGo.transform.ResetLocal();
            return itemGo;
        }

        public void ChangeSceneState(IBaseSceneState baseSceneState)
        {
            lastSceneState = currentSceneState;
            ShowMask();
            currentSceneState = baseSceneState;
        }

        private void ExitSceneComplete()
        {
            lastSceneState.ExitScene();
            currentSceneState.EnterScene();
            HideMask();
        }

    }
}
