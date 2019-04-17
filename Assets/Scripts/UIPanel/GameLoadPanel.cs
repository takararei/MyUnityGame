using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameLoadPanel:BasePanel
{
    float Timer = 2f;
    float time = 0f;
    bool isTime = false;
    public override void Init()
    {
        base.Init();
    }

    void LoadNextScene()
    {
        SceneStateMgr.Instance.ChangeSceneState(new GamePlaySceneState());
    }

    public override void Update()
    {
        base.Update();
        if (isTime == false)
        {
            if (time < Timer)
            {
                time += Time.deltaTime;
            }
            else
            {
                isTime = true;
                LoadNextScene();
            }
        }
    }
}