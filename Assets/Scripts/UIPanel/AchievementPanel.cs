using Assets.Framework;
using Assets.Framework.Audio;
using Assets.Framework.Factory;
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
    public override void Init()
    {
        base.Init();
        acMgr = AchievementInfoMgr.Instance;
        mBtn_Close = Find<Button>("Btn_Close");
        ItemContent = Find<Transform>("ItemContent");
        acItemList = new List<AchievementItem>();
        SetAcItem();
    }

    public override void OnShow()
    {
        base.OnShow();
        mBtn_Close.onClick.AddListener(OnCloseClick);
        
    }

    public override void OnHide()
    {
        base.OnHide();
        
        mBtn_Close.onClick.RemoveAllListeners();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        RemoveAcItem();
    }

    public void OnCloseClick()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        UIMgr.Instance.Hide(UIPanelName.AchievementPanel);
    }

    public void SetAcItem()
    {
        for(int i=0;i<acMgr.infoList.Count;i++)
        {
            GameObject acGo = FactoryMgr.Instance.GetUI(StringMgr.AchievementItem);
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
            FactoryMgr.Instance.PushUI(StringMgr.AchievementItem,ItemContent.GetChild(0).gameObject);
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
        PlayerData playerData;
        public AchievementItem(int index,GameObject root)
        {
            id = index;
            this.root = root;
            playerData = PlayerDataOperator.Instance.playerData;
            acInfo = AchievementInfoMgr.Instance.infoList[id];
            acImage = Find<Image>("Img_Light");
            txt_Name = Find<Text>("Txt_Name");
            txt_Introduce = Find<Text>("Txt_Introduce");
            txt_Name.text = acInfo.name;
            txt_Introduce.text = acInfo.introduce;
            SetAcImage();
            EventCenter.AddListener(EventType.AcItemUpdate, SetAcImage);
        }
        //设置成就是否点亮
        private void SetAcImage()
        {
            if (playerData.achievementList[id].isFinished)
            {
                acImage.sprite = FactoryMgr.Instance.GetSprite(acInfo.FinshedSprite);
            }
            else
            {
                acImage.sprite = FactoryMgr.Instance.GetSprite(acInfo.unFinishSprite);
            }
            acImage.SetNativeSize();
        }

        public override void Clear()
        {
            base.Clear();
            EventCenter.RemoveListener(EventType.AcItemUpdate, SetAcImage);
        }
    }
}
