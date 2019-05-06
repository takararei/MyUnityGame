using Assets.Framework.Extension;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Framework.SceneState;
using Assets.Framework.Audio;

public class BeginPanel : BasePanel
{
    private Button mBtn_SoundEffects;
    private Button mBtn_Music;
    private Button mBtn_StartGame;
    private Button mBtn_ExitGame;
    private Button mBtn_About;
    private Button mBtn_Yes;
    private Button mBtn_No;
    private Image mImg_Exit;
    private Button mBtn_CloseAbout;
    private Image mImg_About;
    private Image mImg_EffectsOff;
    private Image mImg_MusicOff;
    private float topUp = 772;
    private float topDown = 606;
    private float topMoveTime = 0.5f;
    public override void Init()
    {
        base.Init();
        mBtn_SoundEffects = Find<Button>("Btn_SoundEffects");
        mBtn_Music = Find<Button>("Btn_Music");
        mBtn_StartGame = Find<Button>("Btn_StartGame");
        mBtn_ExitGame = Find<Button>("Btn_ExitGame");
        mBtn_About = Find<Button>("Btn_About");
        mBtn_Yes = Find<Button>("Btn_Yes");
        mBtn_No = Find<Button>("Btn_No");
        mImg_Exit = Find<Image>("Img_Exit");
        mImg_About = Find<Image>("Img_About");
        mBtn_CloseAbout = Find<Button>("Btn_CloseAbout");
        mImg_EffectsOff = Find<Image>("Img_EffectsOff");
        mImg_MusicOff = Find<Image>("Img_MusicOff");
    }

    public override void OnShow()
    {
        base.OnShow();
        mImg_Exit.gameObject.Hide();
        mImg_About.gameObject.Hide();
        mBtn_SoundEffects.onClick.AddListener(OnButtonSoundEffectClick);
        mBtn_Music.onClick.AddListener(OnButtonMusicClick);
        mBtn_StartGame.onClick.AddListener(OnButtonStartGameClick);
        mBtn_ExitGame.onClick.AddListener(OnBtnExit);
        mBtn_About.onClick.AddListener(OnBtnAbout);
        mBtn_Yes.onClick.AddListener(OnButtonYesClick);
        mBtn_No.onClick.AddListener(OnBtnNoClick);
        mBtn_CloseAbout.onClick.AddListener(OnBtnCloseAbout);
        
        mImg_EffectsOff.gameObject.SetActive(!AudioMgr.Instance.isPlayEffectMusic);
        mImg_MusicOff.gameObject.SetActive(!AudioMgr.Instance.isPlayBGMusic);
    }
    private void MoveTopButtonUp()
    {
        mBtn_SoundEffects.transform.DOLocalMoveY(topUp, topMoveTime);
        mBtn_Music.transform.DOLocalMoveY(topUp, topMoveTime);
        mBtn_About.transform.DOLocalMoveY(topUp, topMoveTime);
    }

    private void MoveTopButtonDown()
    {
        mBtn_SoundEffects.transform.DOLocalMoveY(topDown, topMoveTime);
        mBtn_Music.transform.DOLocalMoveY(topDown, topMoveTime);
        mBtn_About.transform.DOLocalMoveY(topDown, topMoveTime);
    }

    public override void OnHide()
    {
        base.OnHide();
        mBtn_CloseAbout.onClick.RemoveListener(OnBtnCloseAbout);
        mBtn_No.onClick.RemoveListener(OnBtnNoClick);
        mBtn_Yes.onClick.RemoveListener(OnButtonYesClick);
        mBtn_About.onClick.RemoveListener(OnBtnAbout);
        mBtn_ExitGame.onClick.RemoveListener(OnBtnExit);
        mBtn_StartGame.onClick.RemoveListener(OnButtonStartGameClick);
        mBtn_Music.onClick.RemoveListener(OnButtonMusicClick);
        mBtn_SoundEffects.onClick.RemoveListener(OnButtonSoundEffectClick);
        mImg_Exit.gameObject.Show();
        mImg_About.gameObject.Show();
        mImg_EffectsOff.gameObject.SetActive(true);
        mImg_MusicOff.gameObject.SetActive(true);
    }

    void OnBtnAbout()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        MoveTopButtonUp();
        mImg_About.gameObject.Show();
    }
    
    void OnBtnCloseAbout()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        mImg_About.gameObject.Hide();
        MoveTopButtonDown();
    }

    void OnBtnExit()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        MoveTopButtonUp();
        mImg_Exit.gameObject.Show();
    }

    void OnBtnNoClick()//取消退出
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        mImg_Exit.gameObject.Hide();
        MoveTopButtonDown();
    }

    private void OnButtonYesClick()//确认退出游戏
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        PlayerDataOperator.Instance.SavePlayerData();
        Application.Quit();
        //保存数据之类的操作 
    }

    

    private void OnButtonSoundEffectClick()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        AudioMgr.Instance.CloseOrOpenEffectMusic();
        mImg_EffectsOff.gameObject.SetActive(!AudioMgr.Instance.isPlayEffectMusic);
    }

    private void OnButtonMusicClick()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        AudioMgr.Instance.CloseOrOpenBGMusic();
        mImg_MusicOff.gameObject.SetActive(!AudioMgr.Instance.isPlayBGMusic);
    }

    private void OnButtonStartGameClick()//切换场景
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        SceneStateMgr.Instance.ChangeSceneState(new MainSceneState());
    }
    
}
