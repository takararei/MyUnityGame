﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Framework.UI;
using UnityEngine.UI;
using Assets.Framework.Extension;
using System;
using UnityEngine.EventSystems;
using Assets.Framework.SceneState;
using Assets.Framework;

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
    private Image mImg_EffectsOff;
    private Image mImg_MusicOff;
    Image mSetPanel;
    bool isSetActive = false;
    //地图关卡按钮

    float MapPosXLeft = -960;
    float MapPosXRight = -1356.5f;

    Image mImg_Map;
    float dragLastPosX;
    EventTrigger mImg_MapEventTrigger;
    EventTrigger.Entry onDragEntry;
    EventTrigger.Entry onPointerDownEntry;

    PlayerStatics pStatics;
    public override void Init()
    {
        base.Init();
        pStatics = PlayerStatics.Instance;
        mBtn_Shop = Find<Button>("Btn_Shop");
        mBtn_Achievement = Find<Button>("Btn_Achievement");
        mBtn_Set = Find<Button>("Btn_Set");
        mBtn_Help = Find<Button>("Btn_Help");
        mTxt_Count = Find<Text>("Txt_Count");
        mBtn_SoundEffects = Find<Button>("Btn_SoundEffects");
        mBtn_Music = Find<Button>("Btn_Music");
        mBtn_Home = Find<Button>("Btn_Home");
        mSetPanel = Find<Image>("SetPanel");
        mImg_EffectsOff = Find<Image>("Img_EffectsOff");
        mImg_MusicOff = Find<Image>("Img_MusicOff");

        mImg_Map = Find<Image>("Img_Map");
        mImg_MapEventTrigger = mImg_Map.GetComponent<EventTrigger>();

        onDragEntry = new EventTrigger.Entry();
        onDragEntry.eventID = EventTriggerType.Drag;
        onPointerDownEntry = new EventTrigger.Entry();
        onPointerDownEntry.eventID = EventTriggerType.PointerDown;
        //获取一下当前完成的关卡数  假设两个 +1还未完成的下一关
        //实例化三个关卡按钮到对应位置 读取levelinfo 添加到List里
        //为每个button 注册事件
        //获取关卡要更换的ui
    }

    public class LevelButton : BaseUIListItem
    {
        //获取level id
        //更新level是否通关，更换UI和星星
        Button levelButton;//被点击时 显示关卡介绍面板 
        Sprite finishSprite;//完成时更换ui资源
        Image star1;
        Image star2;
        Image star3;

        public LevelButton(int index)
        {
            id = index;
            //获取预制体并生成在指定位置
            levelButton = Find<Button>("");
            star1 = Find<Image>("");
            star2 = Find<Image>("");
            star3 = Find<Image>("");
            levelButton.onClick.AddListener(OnButtonClick);
        }

        public void OnButtonClick()
        {
            GameRoot.Instance.pickLevel = id;
            UIManager.Instance.Show(UIPanelName.LevelIntroducePanel);
        }
        public void ShowStar(int num)
        {
            if(num>=1)
            {
                star1.gameObject.Show();
            }
            else
            {
                Debug.Log("星级参数有误 关卡" + id);
                return;
            }
            if(num>=2)
            {
                star2.gameObject.Show();
            }
            if(num==3)
            {
                star3.gameObject.Show();
            }
        }

        public override void Clear()
        {
            base.Clear();
            levelButton.onClick.RemoveAllListeners();
        }
    }

    public override void OnShow()
    {
        base.OnShow();
        mSetPanel.gameObject.Hide();
        isSetActive = false;

        mImg_Map.transform.SetLocalPosX(MapPosXLeft);
        //子面板显示
        mBtn_Shop.onClick.AddListener(() => UIManager.Instance.Show(UIPanelName.ShopPanel));
        mBtn_Achievement.onClick.AddListener(() => UIManager.Instance.Show(UIPanelName.AchievementPanel));
        mBtn_Help.onClick.AddListener(() => UIManager.Instance.Show(UIPanelName.HelpPanel));
        mBtn_Set.onClick.AddListener(OnButtonSetClick);
        //设置面板的部分
        mBtn_SoundEffects.onClick.AddListener(OnSoundEffect);
        mBtn_Music.onClick.AddListener(OnMusic);
        mBtn_Home.onClick.AddListener(OnButtonHomeClick);

        //地图部分
        onDragEntry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        onPointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        mImg_MapEventTrigger.triggers.Add(onDragEntry);
        mImg_MapEventTrigger.triggers.Add(onPointerDownEntry);

        //其他
        EventCenter.AddListener<int>(EventType.DoNumChange, SetDONum);

        mTxt_Count.text = pStatics.DO.ToString();
        mImg_EffectsOff.gameObject.SetActive(pStatics.isEffectOff);
        mImg_MusicOff.gameObject.SetActive(pStatics.isMusicOff);
    }

    public override void OnHide()//TODO
    {
        base.OnHide();
        mSetPanel.gameObject.Show();
        //isSetActive = true;
        //其他

        EventCenter.RemoveListener<int>(EventType.DoNumChange,SetDONum);
        //地图相关
        mImg_MapEventTrigger.triggers.Remove(onDragEntry);
        mImg_MapEventTrigger.triggers.Remove(onPointerDownEntry);
        onDragEntry.callback.RemoveAllListeners();
        onPointerDownEntry.callback.RemoveAllListeners();
        //设置面板
        mBtn_Home.onClick.RemoveAllListeners();
        mBtn_Music.onClick.RemoveAllListeners();
        mBtn_SoundEffects.onClick.RemoveAllListeners();
        mBtn_Set.onClick.RemoveAllListeners();
        //子面板
        mBtn_Help.onClick.RemoveAllListeners();
        mBtn_Achievement.onClick.RemoveAllListeners();
        mBtn_Shop.onClick.RemoveAllListeners();
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
        SceneStateManager.Instance.ChangeSceneState(new BeginSceneState());
    }
    void OnMusic()
    {
        pStatics.isEffectOff = !pStatics.isEffectOff;
        mImg_EffectsOff.gameObject.SetActive(pStatics.isEffectOff);
    }
    void OnSoundEffect()
    {
        pStatics.isMusicOff = !pStatics.isMusicOff;
        mImg_MusicOff.gameObject.SetActive(pStatics.isMusicOff);
    }
    void OnPointerDown(PointerEventData eventData)
    {
        mSetPanel.gameObject.Hide();
        isSetActive = false;
    }
    void OnDrag(PointerEventData eventData)
    {
        float dragPosXoffset = dragLastPosX - eventData.position.x;
        float localPosX = mImg_Map.transform.localPosition.x - dragPosXoffset;
        if (localPosX < MapPosXLeft && localPosX > MapPosXRight)
        {
            mImg_Map.transform.SetLocalPosX(localPosX);
        }
        dragLastPosX = eventData.position.x;
    }

    void SetDONum(int num)
    {
        mTxt_Count.text = num.ToString();
    }
}
