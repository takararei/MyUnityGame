﻿using Assets.Framework.Factory;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPanel : BasePanel
{
    private Button mBtn_Close;
    Transform ItemContent;
    AchievementInfoMgr acMgr;
    PlayerStatics pStatics;
    public override void Init()
    {
        base.Init();
        acMgr = AchievementInfoMgr.Instance;
        pStatics = PlayerStatics.Instance;
        mBtn_Close = Find<Button>("Btn_Close");
        ItemContent = Find<Transform>("ItemContent");
        acItemList = new List<AchievementItem>();
    }

    public override void OnShow()
    {
        base.OnShow();
        mBtn_Close.onClick.AddListener(()=>OnHide());
        SetAcItem();
    }

    public override void OnHide()
    {
        base.OnHide();
        RemoveAcItem();
        mBtn_Close.onClick.RemoveAllListeners();
    }

    public void SetAcItem()
    {
        for(int i=0;i<acMgr.infoList.Count;i++)
        {
            GameObject acGo = FactoryManager.Instance.GetUI(StringMgr.AchievementItem);
            acGo.transform.SetParent(ItemContent);
            acGo.transform.localScale = Vector3.one;

            AchievementItem acItem = new AchievementItem(i, acGo);
            acItemList.Add(acItem);
        }
    }

    public void RemoveAcItem()
    {
        if (acItemList.Count == 0) return;
        for (int i = 0; i < acMgr.infoList.Count; i++)
        {
            FactoryManager.Instance.PushUI(StringMgr.AchievementItem,ItemContent.GetChild(0).gameObject);
            acItemList[i].Clear();
        }
        acItemList.Clear();
    }
    List<AchievementItem> acItemList;
    class AchievementItem:BaseUIListItem
    {
        public Image acImage;
        public Text txt_Name;
        public Text txt_Introduce;
        AchievementInfo acInfo;
        public AchievementItem(int index,GameObject root)
        {
            id = index;
            this.root = root;
            acInfo = AchievementInfoMgr.Instance.infoList[id];
            acImage = Find<Image>("Img_Light");
            txt_Name = Find<Text>("Txt_Name");
            txt_Introduce = Find<Text>("Txt_Introduce");
            txt_Name.text = acInfo.name;
            txt_Introduce.text = acInfo.introduce;
            SetAcImage();
            EventCenter.AddListener(EventType.AcItemUpdate, SetAcImage);
        }

        public void SetAcImage()
        {
            if (PlayerStatics.Instance.achievementList[id].isFinished)
            {
                //acImage.sprite = FactoryManager.Instance.GetSprite(acInfo.FinshedSprite);
                Debug.Log("完成");
            }
            else
            {
                //acImage.sprite = FactoryManager.Instance.GetSprite(acInfo.unFinishSprite);
                Debug.Log("未完成");
            }
        }

        public override void Clear()
        {
            base.Clear();
            EventCenter.RemoveListener(EventType.AcItemUpdate, SetAcImage);
        }
    }
}
