using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginSceneState : BaseSceneState {
    public override void EnterScene()
    {
        base.EnterScene();
        UIManager.Instance.Show(UIPanelName.BeginPanel);
        //添加背景音乐
    }

    public override void ExitScene()
    {
        base.ExitScene();
        SceneManager.LoadScene(2);
    }
}
