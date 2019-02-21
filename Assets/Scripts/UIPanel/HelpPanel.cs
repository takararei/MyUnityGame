using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : BasePanel {
    Button mBtn_Close;
    Button mBtn_Enemy;
    Button mBtn_Tower;

    public override void Init()
    {
        base.Init();
        mBtn_Close = Find<Button>("Btn_Close");
        mBtn_Enemy = Find<Button>("Btn_Enemy");
        mBtn_Tower = Find<Button>("Btn_Tower");
    }

    public override void OnShow()
    {
        base.OnShow();
        mBtn_Close.onClick.AddListener(OnHide);
    }

    public override void OnHide()
    {
        base.OnHide();
        mBtn_Close.onClick.RemoveAllListeners();
    }
}
