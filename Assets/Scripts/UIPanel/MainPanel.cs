using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Framework.UI;
using UnityEngine.UI;
using Assets.Framework.Extension;
using System;
using UnityEngine.EventSystems;

public class MainPanel : BasePanel
{

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

    float dragPosXoffset;
    float dragBeginPosX;
    float dragEndPosX;
    Image mImg_Map;
    DragMap mDargMap;
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

        mImg_Map = Find<Image>("Img_Map");
        mDargMap = mImg_Map.GetComponent<DragMap>();
    }

    public override void OnShow()
    {
        base.OnShow();
        mSetPanel.gameObject.Hide();
        isSetActive = false;
        mImg_Map.transform.SetLocalPosX(mDargMap.MapPosXLeft);

        mBtn_Shop.onClick.AddListener
            (
                () => UIManager.Instance.Show(UIPanelName.ShopPanel)
            );
        mBtn_Achievement.onClick.AddListener
            (
                () => UIManager.Instance.Show(UIPanelName.AchievementPanel)
            );
        
        mBtn_Help.onClick.AddListener
            (
                () => UIManager.Instance.Show(UIPanelName.HelpPanel)
            );
        
        mBtn_SoundEffects.onClick.AddListener
            (
                () => { }
            );//
        mBtn_Music.onClick.AddListener
            (
                () => { }
            );//
        mBtn_Home.onClick.AddListener(OnButtonHomeClick);
        mBtn_Set.onClick.AddListener(OnButtonSetClick);
        mTxt_Count.text = "";//
    }

    public override void OnHide()//TODO
    {
        base.OnHide();
        mSetPanel.gameObject.Show();
        isSetActive = true;
    }


    private void OnButtonSetClick()
    {
        if (isSetActive)
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
