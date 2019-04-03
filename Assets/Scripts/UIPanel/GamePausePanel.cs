using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class GamePausePanel:BasePanel
{

    Button btn_Close;
    Button btn_Music;
    Button btn_SoundEffect;
    Button btn_Restart;
    Button btn_ExitGame;
    Image img_MusicOff;
    Image img_EffectOff;
    PlayerStatics pStatics;
    public override void Init()
    {
        base.Init();
        pStatics = PlayerStatics.Instance;
        btn_Close = Find<Button>("Btn_Close");
        btn_Music = Find<Button>("Btn_Music");
        btn_SoundEffect = Find<Button>("Btn_SoundEffect");
        btn_Restart = Find<Button>("Btn_Restart");
        btn_ExitGame = Find<Button>("Btn_ExitGame");
        img_EffectOff = Find<Image>("Img_EffectOff");
        img_MusicOff = Find<Image>("Img_MusicOff");
    }

    public override void OnShow()
    {
        base.OnShow();
        btn_Close.onClick.AddListener(OnClosePanel);
        btn_Music.onClick.AddListener(OnMusicClick);
        btn_SoundEffect.onClick.AddListener(OnEffectClick);
        btn_Restart.onClick.AddListener(OnRestart);
        btn_ExitGame.onClick.AddListener(OnExitGame);
    }
    public override void OnHide()
    {
        base.OnHide();
        btn_ExitGame.onClick.RemoveAllListeners();
        btn_Restart.onClick.RemoveAllListeners();
        btn_SoundEffect.onClick.RemoveAllListeners();
        btn_Music.onClick.RemoveAllListeners();
        btn_Close.onClick.RemoveAllListeners();
    }

    private void OnClosePanel()
    {
        UIManager.Instance.Hide(UIPanelName.GamePausePanel);
        GameController.Instance.isPause = false;
    }

    private void OnExitGame()
    {
        //回到主场景，重置GameController
        SceneStateManager.Instance.ChangeSceneState(new MainSceneState());
    }

    private void OnRestart()
    {
        //重置GameController
        GameController.Instance.RestartGame();
    }

    private void OnEffectClick()
    {
        pStatics.isEffectOff = !pStatics.isEffectOff;
        img_EffectOff.gameObject.SetActive(pStatics.isEffectOff);
    }

    private void OnMusicClick()
    {
        pStatics.isMusicOff = !pStatics.isMusicOff;
        img_MusicOff.gameObject.SetActive(pStatics.isMusicOff);
    }

    
}