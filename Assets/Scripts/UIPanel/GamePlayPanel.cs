using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        pauseBtn.onClick.AddListener(OnPauseClick);
    }

    

    public override void OnHide()
    {
        base.OnHide();

        pauseBtn.onClick.RemoveAllListeners();
        EventCenter.RemoveListener<int>(EventType.Play_NowRoundUpdate, SetNowRound);
        EventCenter.RemoveListener<int>(EventType.Play_LifeUpdate, SetLifeNum);
        EventCenter.RemoveListener<int>(EventType.Play_CoinUpdate, SetCoinNum);
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
}