using Assets.Framework.Audio;
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
        private void Awake()
        {
            DontDestroyOnLoad(this);
            UIManager.GetInstance().ParseUIpanelTypeAsset();
            UIManager.GetInstance().Show(UIPanelName.TestUIPanel);

            AudioManager.GetInstance().Root = this.gameObject;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            UIManager.GetInstance().Update();
        }
    }
}
