﻿using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPanel : BasePanel
{
    private Button mBtn_Close;
    
    public override void Init()
    {
        base.Init();
        mBtn_Close = Find<Button>("Btn_Close");
    }

    public override void OnShow()
    {
        base.OnShow();
        mBtn_Close.onClick.AddListener(()=>OnHide());
    }

    public override void OnHide()
    {
        base.OnHide();
        mBtn_Close.onClick.RemoveAllListeners();
    }
}
