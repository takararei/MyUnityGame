using Assets.Framework.Audio;
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
        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;

            FactoryManager.Instance.Init();
            AudioManager.Instance.Init();
            UIManager.Instance.Init();
            SceneStateManager.Instance.Init();

        }

        private void Start()
        {
            
        }

        private void Update()
        {
            UIManager.Instance.Update();
        }

        public GameObject CreateItem(GameObject itemGo)
        {
            GameObject go = Instantiate(itemGo);
            return go;
        }

    }
}
