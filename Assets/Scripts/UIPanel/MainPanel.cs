using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Framework.UI;
using UnityEngine.UI;
using Assets.Framework.Extension;
using System;
using UnityEngine.EventSystems;
using Assets.Framework.SceneState;

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
    
    float MapPosXLeft = -960;
    float MapPosXRight = -1356.5f;

    Image mImg_Map;
    float dragLastPosX;
    EventTrigger mImg_MapEventTrigger;
    EventTrigger.Entry onDragEntry;
    EventTrigger.Entry onPointerDownEntry;
    

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
        mImg_MapEventTrigger = mImg_Map.GetComponent<EventTrigger>();

        onDragEntry = new EventTrigger.Entry();
        onDragEntry.eventID = EventTriggerType.Drag;
        onPointerDownEntry = new EventTrigger.Entry();
        onPointerDownEntry.eventID = EventTriggerType.PointerDown;
        
    }

    public class LevelButton:BaseUIListItem
    {
        //获取level id
        //更新level是否通关，更换UI和星星
        Button levelButton;//被点击时 显示关卡介绍面板 
        Sprite finishSprite;//完成时更换ui资源
        Image star1;
        Image star2;
        Image star3;

        
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
        mBtn_SoundEffects.onClick.AddListener(() => { } );
        mBtn_Music.onClick.AddListener(() => { });
        mBtn_Home.onClick.AddListener(OnButtonHomeClick);

        //地图部分
        onDragEntry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        onPointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        mImg_MapEventTrigger.triggers.Add(onDragEntry);
        mImg_MapEventTrigger.triggers.Add(onPointerDownEntry);

        mTxt_Count.text = "";//


        //获取一下当前完成的关卡数  假设两个 +1还未完成的下一关
        //实例化三个关卡按钮到对应位置 读取levelinfo 添加到List里
        //为每个button 注册事件
    }

    public override void OnHide()//TODO
    {
        base.OnHide();
        mSetPanel.gameObject.Show();
        isSetActive = true;


        mImg_MapEventTrigger.triggers.Remove(onDragEntry);
        mImg_MapEventTrigger.triggers.Remove(onPointerDownEntry);
        onDragEntry.callback.RemoveAllListeners();
        onPointerDownEntry.callback.RemoveAllListeners();
        
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

}
