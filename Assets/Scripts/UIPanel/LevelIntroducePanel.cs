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
    public override void Init()
    {
        base.Init();
        closeBtn = Find<Button>("Btn_Close");
        Btn_Begin = Find<Button>("Btn_Begin");
        smallMap = Find<Image>("SmallMap");
        levelName = Find<Text>("Txt_LevelName");
        levelIntroduce= Find<Text>("Txt_LevelIntroduce");
    }

    public override void OnShow()
    {
        base.OnShow();
        closeBtn.onClick.AddListener(()=> { UIManager.Instance.Hide(UIPanelName.LevelIntroducePanel); });
        Btn_Begin.onClick.AddListener(OnEnterGame);
    }

    public void OnEnterGame()
    {

    }

    public override void OnHide()
    {
        base.OnHide();
        closeBtn.onClick.RemoveAllListeners();
        Btn_Begin.onClick.RemoveAllListeners();
    }

    
}
