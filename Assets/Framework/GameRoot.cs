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
            UIManager.GetInstance().ParseUIPanelTypeJson();
            UIManager.GetInstance().Show(UIPanelName.TestUIPanel);
        }

        private void Update()
        {
            UIManager.GetInstance().Update();
        }
    }
}
