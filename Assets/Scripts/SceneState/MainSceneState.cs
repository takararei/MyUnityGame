using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneState : BaseSceneState {

    public override void EnterScene()
    {
        base.EnterScene();
        UIManager.Instance.Show(UIPanelName.MainPanel);
    }

    public override void ExitScene()
    {
        base.ExitScene();
        if(SceneStateManager.Instance.currentSceneState.GetType()==typeof(BeginSceneState))
        {
            SceneManager.LoadScene(1);
        }
        else if(SceneStateManager.Instance.currentSceneState.GetType() ==typeof(GameLoadSceneState))
        {
            SceneManager.LoadScene(3);
        }
    }
}
