using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel:BasePanel
{
    Button pauseBtn;
    Text Txt_TotalRound;
    Text Txt_NowRound;
    Text Txt_Life;
    Text Txt_Coin;
    PlayerStatics pStatics;
    LevelInfoMgr lvInfoMgr;
    Button btn_Restart;
    Button btn_ExitGame;
    Image GameOver;
    public override void Init()
    {
        base.Init();
        pStatics = PlayerStatics.Instance;
        lvInfoMgr = LevelInfoMgr.Instance;
        pauseBtn = Find<Button>("Btn_Pause");
        Txt_TotalRound = Find<Text>("Txt_TotalRound");
        Txt_NowRound = Find<Text>("Txt_NowRound");
        Txt_Life = Find<Text>("Txt_Life");
        Txt_Coin = Find<Text>("Txt_Coin");
        btn_Restart = Find<Button>("Btn_Restart");
        btn_ExitGame = Find<Button>("Btn_ExitGame");
        GameOver = Find<Image>("GameOver");
        GameOver.gameObject.SetActive(false);
    }

    public override void OnShow()
    {
        base.OnShow();
        LevelInfo info = lvInfoMgr.levelInfoList[pStatics.nowLevel];
        Txt_Coin.text = info.beginCoin.ToString();
        Txt_Life.text = info.life.ToString();
        Txt_NowRound.text = "1";
        Txt_TotalRound.text = info.totalRound.ToString();
        EventCenter.AddListener<int>(EventType.Play_CoinUpdate, SetCoinNum);
        EventCenter.AddListener<int>(EventType.Play_LifeUpdate, SetLifeNum);
        EventCenter.AddListener<int>(EventType.Play_NowRoundUpdate, SetNowRound);
        EventCenter.AddListener(EventType.GameOver, OnGameOver);
        pauseBtn.onClick.AddListener(OnPauseClick);
        btn_Restart.onClick.AddListener(OnRestart);
        btn_ExitGame.onClick.AddListener(OnExitGame);
    }

    

    public override void OnHide()
    {
        base.OnHide();
        btn_ExitGame.onClick.RemoveAllListeners();
        btn_Restart.onClick.RemoveAllListeners();
        pauseBtn.onClick.RemoveAllListeners();
        EventCenter.RemoveListener(EventType.GameOver, OnGameOver);
        EventCenter.RemoveListener<int>(EventType.Play_NowRoundUpdate, SetNowRound);
        EventCenter.RemoveListener<int>(EventType.Play_LifeUpdate, SetLifeNum);
        EventCenter.RemoveListener<int>(EventType.Play_CoinUpdate, SetCoinNum);
        GameOver.gameObject.SetActive(true);
    }


    void SetCoinNum(int num)
    {
        Txt_Coin.text = num.ToString();
    }

    void SetNowRound(int num)
    {
        Txt_NowRound.text = num.ToString();
    }

    void SetLifeNum(int num)
    {
        Txt_Life.text = num.ToString();
    }

    private void OnPauseClick()
    {
        GameController.Instance.isPause = true;
        UIManager.Instance.Show(UIPanelName.GamePausePanel);
    }

    private void OnExitGame()
    {
        //回到主场景，重置GameController
        SceneStateManager.Instance.ChangeSceneState(new MainSceneState());
    }

    private void OnRestart()
    {
        GameController.Instance.RestartGame();
        GameOver.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        GameOver.gameObject.SetActive(true);
    }
  
}