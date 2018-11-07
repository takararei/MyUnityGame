using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.UI
{
    public class GameRoot:MonoBehaviour
    {
        private void Awake()
        {
            UIManager.GetInstance().ParseUIPanelTypeJson();
            UIManager.GetInstance().PushPanel(UIPanelName.TestUIPanel);
        }
    }
}
