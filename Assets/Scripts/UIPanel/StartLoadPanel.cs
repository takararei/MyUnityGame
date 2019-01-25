using Assets.Framework;
using Assets.Framework.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoadPanel : BasePanel {
    float Timer = 2f;
    float time = 0f;
    bool isTime = false;
    public override void Init()
    {
        base.Init();
        //DOTween.To(() => i,
        //        toColor => i = toColor,
        //        10f,
        //        2f).OnComplete(LoadNextScene);
    }
    
    void LoadNextScene()
    {
        UIManager.Instance.uiFacade.ChangeSceneState(new BeginSceneState());
    }

    public override void Update()
    {
        base.Update();
        if(isTime==false)
        {
            if(time<Timer)
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
