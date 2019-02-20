using Assets.Framework.Extension;
using Assets.Framework.Factory;
using Assets.Framework.SceneState;
using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Framework.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public UIFacade uiFacade;

        //当前场景所有的UIPanel预制体
        public Dictionary<string, GameObject> currentScenePanelDict;

        public void Init()
        {
            uiFacade = new UIFacade();
            currentScenePanelDict = new Dictionary<string, GameObject>();
            ParseUIpanelTypeAsset();
            uiFacade.currentSceneState = new StartLoadSceneState();
        }
        
        /// <summary>
        /// 解析JSON文件
        /// </summary>
        public void ParseUIPanelTypeJson()
        {
            uiFacade.ParseUIPanelTypeJson();
        }

        public void ParseUIpanelTypeAsset()
        {
            uiFacade.ParseUIpanelTypeAsset();
        }

        public void Show(string panelName)
        {
            uiFacade.Show(panelName);
        }

        public void Hide(string panelName)
        {
            uiFacade.Hide(panelName);
        }

        public void Update()
        {
            uiFacade.Update();
        }

        //public void ClearDict()
        //{
        //    foreach (var item in currentScenePanelDict)
        //    {
        //        item.Value.transform.SetParent(GameRoot.Instance.transform);
        //        UIManager.Instance.Hide(item.Value.name);
        //    }

        //    currentScenePanelDict.Clear();
        //}
    }
}
