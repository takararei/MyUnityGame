using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

public class GameLoadSceneState:BaseSceneState
{
    public override void EnterScene()
    {
        base.EnterScene();
        UIMgr.Instance.Show(UIPanelName.GameLoadPanel);
    }

    public override void ExitScene()
    {
        base.ExitScene();
        SceneManager.LoadScene(4);
    }
}