﻿using Assets.Framework.Audio;
using Assets.Framework.Factory;
using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework
{
    public class GameRoot:MonoBehaviour
    {
        private static GameRoot _instance;
        public static GameRoot Instance
        {
            get
            {
                return _instance;
            }
        }
        public bool toMainScene;
        public int pickLevel;
        public PlayerData playerData;
        PlayerDataOperator pdOperator;
        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
            pdOperator = new PlayerDataOperator();
            playerData = pdOperator.LoadPlayerData();
            FactoryMgr.Instance.Init();
            AudioMgr.Instance.Init();
            UIMgr.Instance.Init();
            SceneStateMgr.Instance.Init();

        }

        private void Start()
        {
            
        }

        private void Update()
        {
            UIMgr.Instance.Update();
        }

        public GameObject CreateItem(GameObject itemGo)
        {
            GameObject go = Instantiate(itemGo);
            return go;
        }

    }
}
