using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel {

    private Button mBtn_Close;
    private Button mBtn_Buy;
    private Text mTxt_Count;//持有钻石数
    //商品展示
    private Text mTxt_Name;//商品名称
    private Text mTxt_Introduce;//商品介绍
    private Text mTxt_Diamond;//商品价格

    public override void Init()
    {
        base.Init();
        mBtn_Close = Find<Button>("Btn_Close");
        mBtn_Buy = Find<Button>("Btn_Buy");
        mTxt_Count = Find<Text>("Txt_Count");

        mTxt_Name = Find<Text>("Txt_Name");
        mTxt_Introduce = Find<Text>("Txt_Introduce");
        mTxt_Diamond = Find<Text>("Txt_Diamond");


    }

    public override void OnShow()
    {
        base.OnShow();
        mBtn_Close.onClick.AddListener(OnHide);
        mBtn_Buy.onClick.AddListener(OnBuyButtonClick);
    }

    public override void OnHide()
    {
        base.OnHide();
        mBtn_Close.onClick.RemoveAllListeners();
        mBtn_Buy.onClick.RemoveAllListeners();
    }

    private void OnBuyButtonClick()
    {

    }
}
