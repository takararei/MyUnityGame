using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class GameWinPanel:BasePanel
{
    Button btn_Restart;
    Button btn_Continue;
    Image star1;
    Image star2;
    Image star3;
    public override void Init()
    {
        base.Init();
        btn_Restart = Find<Button>("Btn_Restart");
        btn_Continue = Find<Button>("Btn_Continue");
        star1 = Find<Image>("star1");
        star2 = Find<Image>("star2");
        star3 = Find<Image>("star3");
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);
    }

    public override void OnShow()
    {
        base.OnShow();
        btn_Restart.onClick.AddListener(OnRestart);
        btn_Continue.onClick.AddListener(OnExitGame);
        ShowStar(GameController.Instance.Life);
    }

    public override void OnHide()
    {
        base.OnHide();
        btn_Continue.onClick.RemoveAllListeners();
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
        UIManager.Instance.Hide(UIPanelName.GameWinPanel);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        star1.gameObject.SetActive(true);
        star2.gameObject.SetActive(true);
        star3.gameObject.SetActive(true);
    }

    public void ShowStar(int num)
    {
        if (num >= 9)
        {
            //三星处理
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(true);
            star3.gameObject.SetActive(true);
        }
        else if (num >= 5)
        {
            //两星处理
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(true);
            star3.gameObject.SetActive(false);
        }
        else if (num >= 1)
        {
            //一星处理
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
        }
    }
}