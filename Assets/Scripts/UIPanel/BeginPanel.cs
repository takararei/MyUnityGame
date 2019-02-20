﻿using Assets.Framework.Extension;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
        
    }

    public override void OnShow()
    {
        base.OnShow();
        mImg_Exit.gameObject.Hide();
        mImg_About.gameObject.Hide();
        mBtn_SoundEffects.onClick.AddListener(OnButtonSoundEffectClick);
        mBtn_Music.onClick.AddListener(OnButtonMusicClick);
        mBtn_StartGame.onClick.AddListener(OnButtonStartGameClick);
        mBtn_ExitGame.onClick.AddListener(()=> { MoveTopButtonUp(); mImg_Exit.gameObject.Show(); });
        mBtn_About.onClick.AddListener(()=> { MoveTopButtonUp(); mImg_About.gameObject.Show(); });
        mBtn_Yes.onClick.AddListener(OnButtonYesClick);
        mBtn_No.onClick.AddListener(()=> { mImg_Exit.gameObject.Hide();MoveTopButtonDown(); });
        mBtn_CloseAbout.onClick.AddListener(() => { mImg_About.gameObject.Hide();MoveTopButtonDown(); });
        
    }
    public void MoveTopButtonUp()
    {
        mBtn_SoundEffects.transform.DOLocalMoveY(topUp, topMoveTime);
        mBtn_Music.transform.DOLocalMoveY(topUp, topMoveTime);
        mBtn_About.transform.DOLocalMoveY(topUp, topMoveTime);
    }

    public void MoveTopButtonDown()
    {
        mBtn_SoundEffects.transform.DOLocalMoveY(topDown, topMoveTime);
        mBtn_Music.transform.DOLocalMoveY(topDown, topMoveTime);
        mBtn_About.transform.DOLocalMoveY(topDown, topMoveTime);
    }

    public override void OnHide()
    {
        base.OnHide();
        mBtn_CloseAbout.onClick.RemoveListener(() => { mImg_About.gameObject.Hide(); MoveTopButtonDown(); });
        mBtn_No.onClick.RemoveListener(() => { mImg_Exit.gameObject.Hide(); MoveTopButtonDown(); });
        mBtn_Yes.onClick.RemoveListener(OnButtonYesClick);
        mBtn_About.onClick.RemoveListener(() => { MoveTopButtonUp(); mImg_About.gameObject.Show(); });
        mBtn_ExitGame.onClick.RemoveListener(() => { MoveTopButtonUp(); mImg_Exit.gameObject.Show(); });
        mBtn_StartGame.onClick.RemoveListener(OnButtonStartGameClick);
        mBtn_Music.onClick.RemoveListener(OnButtonMusicClick);
        mBtn_SoundEffects.onClick.RemoveListener(OnButtonSoundEffectClick);
        mImg_Exit.gameObject.Show();
        mImg_About.gameObject.Show();
    }
    
    private void OnButtonYesClick()
    {
        Application.Quit();
    }

    

    public void OnButtonSoundEffectClick()
    {

    }

    public void OnButtonMusicClick()
    {

    }

    public void OnButtonStartGameClick()//切换场景
    {
        UIManager.Instance.uiFacade.ChangeSceneState(new MainSceneState());
    }
    
}