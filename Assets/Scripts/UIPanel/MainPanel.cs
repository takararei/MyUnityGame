using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Framework.UI;
using UnityEngine.UI;
using Assets.Framework.Extension;
using System;
using UnityEngine.EventSystems;
using Assets.Framework.SceneState;
using Assets.Framework;
using Assets.Framework.Factory;

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
    float MapPosXRight = -3000;

    Image mImg_Map;
    float dragLastPosX;
    EventTrigger mImg_MapEventTrigger;
    EventTrigger.Entry onDragEntry;
    EventTrigger.Entry onPointerDownEntry;

    PlayerStatics pStatics;
    LevelInfoMgr lvMgr;

    public override void Init()
    {
        base.Init();
        pStatics = PlayerStatics.Instance;
        lvMgr = LevelInfoMgr.Instance;
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
        lvBtnList = new List<LevelButton>();
        

        //为每个button 注册事件
        //获取关卡要更换的ui
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

        SetLevelButton();
        
    }

    public override void OnHide()//TODO
    {
        base.OnHide();
        mSetPanel.gameObject.Show();
        //isSetActive = true;
        //其他
        RemoveLevelButton();

        EventCenter.RemoveListener<int>(EventType.DoNumChange, SetDONum);

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

    void SetLevelButton()
    {
        //如果没生成过
        if (lvBtnList.Count == 0)
        {
            for (int i = 0; i < pStatics.finishedLevelCount + 1; i++)
            {
                GenerateLevelButton(i);
            }
        }
        //如果生成过了，需要更新
        else if (lvBtnList.Count < pStatics.finishedLevelCount + 1)
        {
            for (int i = lvBtnList.Count - 1; i < pStatics.finishedLevelCount + 1; i++)
            {
                GenerateLevelButton(i);
            }
        }
    }
    void GenerateLevelButton(int index)
    {
        GameObject lvBtn = FactoryManager.Instance.GetUI(StringMgr.Btn_MapLevel);
        lvBtn.transform.SetParent(mImg_Map.transform);
        lvBtn.transform.localPosition = lvMgr.levelInfoList[index].levelPos;
        lvBtn.transform.localScale = Vector3.one;
        LevelButton lb = new LevelButton(index,lvBtn);
        lb.ShowStar(pStatics.levelStar[index]);
        lvBtnList.Add(lb);
        //星星的更新 
        
    }
    void RemoveLevelButton()
    {
        if (lvBtnList.Count == 0) return;
        for(int i=0;i<lvBtnList.Count;i++)
        {
            FactoryManager.Instance.PushUI("Btn_MapLevel", mImg_Map.transform.GetChild(0).gameObject);
            lvBtnList[i].Clear();
        }
        lvBtnList.Clear();
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
    void OnSoundEffect()
    {
        pStatics.isEffectOff = !pStatics.isEffectOff;
        mImg_EffectsOff.gameObject.SetActive(pStatics.isEffectOff);
    }
    void OnMusic()
    {
        pStatics.isMusicOff = !pStatics.isMusicOff;
        mImg_MusicOff.gameObject.SetActive(pStatics.isMusicOff);
    }
    void OnPointerDown(PointerEventData eventData)
    {
        mSetPanel.gameObject.Hide();
        isSetActive = false;
        dragLastPosX = eventData.position.x; ;
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
        Debug.Log(mImg_Map.transform.localPosition.x);
    }

    void SetDONum(int num)
    {
        mTxt_Count.text = num.ToString();
    }

    List<LevelButton> lvBtnList;
    public class LevelButton : BaseUIListItem
    {
        //获取level id
        //更新level是否通关，更换UI和星星
        Button levelButton;//被点击时 显示关卡介绍面板 
        Image Img_Btn;//未完成时更换ui资源
        Image star1;
        Image star2;
        Image star3;

        public LevelButton(int index,GameObject root)
        {
            id = index;
            this.root = root;
            //获取预制体并生成在指定位置
            levelButton = root.GetComponent<Button>();
            star1 = Find<Image>("Img_Star1");
            star2 = Find<Image>("Img_Star2");
            star3 = Find<Image>("Img_Star3");
            levelButton.onClick.AddListener(OnButtonClick);

            star1.gameObject.Hide();
            star2.gameObject.Hide();
            star3.gameObject.Hide();
        }

        public void OnButtonClick()
        {
            UIManager.Instance.Show(UIPanelName.LevelIntroducePanel);
            EventCenter.Broadcast(EventType.LevelIntroduceUpdate, id);
        }
        public void ShowStar(int num)
        {
            if(num==0)
            {
                //levelButton.image.sprite = FactoryManager.Instance.GetSprite("");//TODO
                return;
            }
            if (num >= 1)
            {
                star1.gameObject.Show();
            }
            if (num >= 2)
            {
                star2.gameObject.Show();
            }
            if (num == 3)
            {
                star3.gameObject.Show();
            }
        }

        public override void Clear()
        {
            base.Clear();
            levelButton.onClick.RemoveAllListeners();
            star1.gameObject.Show();
            star2.gameObject.Show();
            star3.gameObject.Show();
        }

    }
}
