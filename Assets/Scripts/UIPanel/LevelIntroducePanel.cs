using Assets.Framework;
using Assets.Framework.Factory;
using Assets.Framework.SceneState;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroducePanel : BasePanel
{
    Button closeBtn;
    Text levelName;
    Text levelIntroduce;
    Image smallMap;
    Button Btn_Begin;
    LevelInfoMgr lvMgr;
    int pickLevel;
    public override void Init()
    {
        base.Init();
        lvMgr = LevelInfoMgr.Instance;
        closeBtn = Find<Button>("Btn_Close");
        Btn_Begin = Find<Button>("Btn_Begin");
        smallMap = Find<Image>("SmallMap");
        levelName = Find<Text>("Txt_LevelName");
        levelIntroduce= Find<Text>("Txt_LevelIntroduce");
    }

    public override void OnShow()
    {
        base.OnShow();
        closeBtn.onClick.AddListener(()=> { UIMgr.Instance.Hide(UIPanelName.LevelIntroducePanel); });
        Btn_Begin.onClick.AddListener(OnEnterGame);
        EventCenter.AddListener<int>(EventType.LevelIntroduceUpdate, UpdateLevelInfo);
    }
    public override void OnHide()
    {
        base.OnHide();
        closeBtn.onClick.RemoveAllListeners();
        Btn_Begin.onClick.RemoveAllListeners();
    }

    public void OnEnterGame()
    {
        GameRoot.Instance.pickLevel = pickLevel;
        SceneStateMgr.Instance.ChangeSceneState(new GameLoadSceneState());
    }

    public void UpdateLevelInfo(int index)
    {
        pickLevel = index;
        LevelInfo info = lvMgr.levelInfoList[index];
        smallMap.sprite = FactoryMgr.Instance.GetSprite(info.mapPath);
        levelName.text = info.levelName;
        levelIntroduce.text = info.levelIntroduce;
    }

    

    
}
