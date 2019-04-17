using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadSceneState : BaseSceneState {

    public override void EnterScene()
    {
        base.EnterScene();
        UIMgr.Instance.Show(UIPanelName.StartLoadPanel);
    }

    public override void ExitScene()
    {
        base.ExitScene();
        SceneManager.LoadScene(1);
    }
}
