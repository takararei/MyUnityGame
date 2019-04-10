using Assets.Framework.Audio;
using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GamePausePanel:BasePanel
{
    Button btn_Close;
    Button btn_Music;
    Button btn_SoundEffect;
    Button btn_Restart;
    Button btn_ExitGame;
    Image mImg_MusicOff;
    Image mImg_EffectsOff;
    public override void Init()
    {
        base.Init();
        btn_Close = Find<Button>("Btn_Close");
        btn_Music = Find<Button>("Btn_Music");
        btn_SoundEffect = Find<Button>("Btn_SoundEffect");
        btn_Restart = Find<Button>("Btn_Restart");
        btn_ExitGame = Find<Button>("Btn_ExitGame");
        mImg_EffectsOff = Find<Image>("Img_EffectOff");
        mImg_MusicOff = Find<Image>("Img_MusicOff");
    }

    public override void OnShow()
    {
        base.OnShow();
        
        btn_Close.onClick.AddListener(OnClosePanel);
        btn_Music.onClick.AddListener(OnMusicClick);
        btn_SoundEffect.onClick.AddListener(OnEffectClick);
        btn_Restart.onClick.AddListener(OnRestart);
        btn_ExitGame.onClick.AddListener(OnExitGame);
        mImg_EffectsOff.gameObject.SetActive(!AudioManager.Instance.playEffectMusic);
        mImg_MusicOff.gameObject.SetActive(!AudioManager.Instance.playBGMusic);
        //mImg_EffectsOff.gameObject.SetActive(PlayerPrefs.GetInt(StringMgr.isEffectOff) == 1);
        //mImg_MusicOff.gameObject.SetActive(PlayerPrefs.GetInt(StringMgr.isMusicOff) == 1);
    }
    public override void OnHide()
    {
        base.OnHide();
        mImg_EffectsOff.gameObject.SetActive(true);
        mImg_MusicOff.gameObject.SetActive(true);
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
        UIManager.Instance.Hide(UIPanelName.GamePausePanel);
        GameController.Instance.RestartGame();
    }

    private void OnEffectClick()
    {
        //PlayerPrefs.SetInt(StringMgr.isEffectOff, PlayerPrefs.GetInt(StringMgr.isEffectOff) == 1 ? 0 : 1);
        //mImg_EffectsOff.gameObject.SetActive(PlayerPrefs.GetInt(StringMgr.isEffectOff) == 1);
        AudioManager.Instance.CloseOrOpenEffectMusic();
        mImg_EffectsOff.gameObject.SetActive(!AudioManager.Instance.playEffectMusic);
        //pStatics.isEffectOff = !pStatics.isEffectOff;
        //mImg_EffectsOff.gameObject.SetActive(pStatics.isEffectOff);
    }

    private void OnMusicClick()
    {
        //PlayerPrefs.SetInt(StringMgr.isMusicOff, PlayerPrefs.GetInt(StringMgr.isMusicOff) == 1 ? 0 : 1);
        //mImg_MusicOff.gameObject.SetActive(PlayerPrefs.GetInt(StringMgr.isMusicOff) == 1);
        AudioManager.Instance.CloseOrOpenBGMusic();
        mImg_MusicOff.gameObject.SetActive(!AudioManager.Instance.playEffectMusic);
        //pStatics.isMusicOff = !pStatics.isMusicOff;
        //mImg_MusicOff.gameObject.SetActive(pStatics.isMusicOff);
    }

    
}