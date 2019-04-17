﻿using Assets.Framework.Extension;
using Assets.Framework.Factory;
using Assets.Framework.SceneState;
using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Framework.UI
{
    public class UIMgr : Singleton<UIMgr>
    {
        public UIFacade uiFacade;
        
        public override void Init()
        {
            base.Init();
            uiFacade = new UIFacade();
            ParseUIpanelTypeAsset();
        }
        
        /// <summary>
        /// 解析JSON文件
        /// </summary>
        //public void ParseUIPanelTypeJson()
        //{
        //    uiFacade.ParseUIPanelTypeJson();
        //}

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
        
    }
}
