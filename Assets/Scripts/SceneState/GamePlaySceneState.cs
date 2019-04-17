﻿using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

public class GamePlaySceneState:BaseSceneState
{
    public override void EnterScene()
    {
        base.EnterScene();
        UIMgr.Instance.Show(UIPanelName.GamePlayPanel);
        UIMgr.Instance.Show(UIPanelName.TowerSetPanel);
    }

    public override void ExitScene()
    {
        base.ExitScene();
        SceneManager.LoadScene(2);
    }
}