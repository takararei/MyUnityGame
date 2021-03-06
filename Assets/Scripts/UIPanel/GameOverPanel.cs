﻿using Assets.Framework.Audio;
using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class GameOverPanel:BasePanel
{
    Button btn_Restart;
    Button btn_ExitGame;
    Text txt_DO;
    public override void Init()
    {
        base.Init();
        btn_Restart = Find<Button>("Btn_Restart");
        btn_ExitGame = Find<Button>("Btn_ExitGame");
        txt_DO = Find<Text>("Txt_DO");
    }

    public override void OnShow()
    {
        base.OnShow();
        btn_Restart.onClick.AddListener(OnRestart);
        btn_ExitGame.onClick.AddListener(OnExitGame);
        txt_DO.text = GameController.Instance.DO.ToString();
    }

    public override void OnHide()
    {
        base.OnHide();
        btn_ExitGame.onClick.RemoveAllListeners();
        btn_Restart.onClick.RemoveAllListeners();
    }

    private void OnExitGame()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        //回到主场景，重置GameController
        GameController.Instance.RecycleAll();
        EventCenter.Broadcast(EventType.LeaveGameScene);
        SceneStateMgr.Instance.ChangeSceneState(new MainSceneState());
    }

    private void OnRestart()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        GameController.Instance.RestartGame();
        UIMgr.Instance.Hide(UIPanelName.GameOverPanel);
    }

}