using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Framework.UI;
using UnityEngine.UI;
using Assets.Framework.Extension;
using System;

public class MainPanel : BasePanel {

    Button mBtn_Shop;
    Button mBtn_Achievement;
    Button mBtn_Set;
    Button mBtn_Help;
    Text mTxt_Count;
    Button mBtn_SoundEffects;
    Button mBtn_Music;
    Button mBtn_Home;
    Image mSetPanel;
    bool isSetActive = false;
    //地图关卡按钮

    public override void Init()
    {
        base.Init();

        mBtn_Shop = Find<Button>("Btn_Shop");
        mBtn_Achievement = Find<Button>("Btn_Achievement");
        mBtn_Set = Find<Button>("Btn_Set");
        mBtn_Help = Find<Button>("Btn_Help");
        mTxt_Count = Find<Text>("Txt_Count");
        mBtn_SoundEffects = Find<Button>("Btn_SoundEffects");
        mBtn_Music = Find<Button>("Btn_Music");
        mBtn_Home = Find<Button>("Btn_Home");
        mSetPanel = Find<Image>("SetPanel");
       
    }

    public override void OnShow()
    {
        base.OnShow();
        mSetPanel.gameObject.Hide();
        isSetActive = false;
        mBtn_Shop.onClick.AddListener(()=>UIManager.Instance.Show(UIPanelName.ShopPanel));
        mBtn_Achievement.onClick.AddListener(() => UIManager.Instance.Show(UIPanelName.AchievementPanel));
        mBtn_Set.onClick.AddListener(OnButtonSetClick);
        mBtn_Help.onClick.AddListener(() => { });//helpPanel
        mTxt_Count.text = "";//
        mBtn_SoundEffects.onClick.AddListener(()=> { });//
        mBtn_Music.onClick.AddListener(() => { });//
        mBtn_Home.onClick.AddListener(OnButtonHomeClick);

    }

    public override void OnHide()//TODO
    {
        base.OnHide();
        mSetPanel.gameObject.Show();
        isSetActive = true;
    }


    private void OnButtonSetClick()
    {
        if(isSetActive)
        {
            mSetPanel.gameObject.Hide();
            isSetActive = false;
        }
        else
        {
            mSetPanel.gameObject.Show();
            isSetActive = true;
        }
    }

    

    private void OnButtonHomeClick()
    {
        UIManager.Instance.uiFacade.ChangeSceneState(new BeginSceneState());
    }

    
}
