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

    public override void Init()
    {
        base.Init();
        btn_Restart = Find<Button>("Btn_Restart");
        btn_ExitGame = Find<Button>("Btn_ExitGame");
    }

    public override void OnShow()
    {
        base.OnShow();
        btn_Restart.onClick.AddListener(OnRestart);
        btn_ExitGame.onClick.AddListener(OnExitGame);
    }

    public override void OnHide()
    {
        base.OnHide();
        btn_ExitGame.onClick.RemoveAllListeners();
        btn_Restart.onClick.RemoveAllListeners();
    }

    private void OnExitGame()
    {
        //回到主场景，重置GameController
        SceneStateManager.Instance.ChangeSceneState(new MainSceneState());
    }

    private void OnRestart()
    {
        GameController.Instance.RestartGame();
        UIManager.Instance.Hide(UIPanelName.GameOverPanel);
    }

}