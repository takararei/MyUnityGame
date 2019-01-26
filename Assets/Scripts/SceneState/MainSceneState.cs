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
        UIManager.Instance.Show("MainPanel");
    }

    public override void ExitScene()
    {
        base.ExitScene();
        if(UIManager.Instance.uiFacade.currentSceneState.GetType()==typeof(BeginPanel))
        {
            SceneManager.LoadScene(1);
        }
    }
}
