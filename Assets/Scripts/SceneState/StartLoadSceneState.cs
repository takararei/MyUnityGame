using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoadSceneState : BaseSceneState {

    public override void EnterScene()
    {
        base.EnterScene();
        UIManager.Instance.Show(UIPanelName.StartLoadPanel);
    }
}
