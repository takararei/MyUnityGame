using Assets.Framework.Audio;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Framework.Extension;
using DG.Tweening;
using System;
using Assets.Framework.Factory;

public class HelpPanel : BasePanel
{
    Button mBtn_Close;

    Transform EnemyContent;
    Transform TowerContent;

    Transform TowerIntroPanel;
    Transform EnemyIntroPanel;
    Button mBtn_EnemyClose;
    Button mBtn_TowerClose;

    Image Img_EnemyPicture;
    Text Txt_EnemyName;
    Text Txt_EnemyIntroduce;
    Text Txt_TotalLife;
    Text Txt_Heart;
    Text Txt_Def;
    Text Txt_Mdef;
    Text Txt_Speed;

    Image Img_TowerPicture;
    Text Txt_TowerName;
    Text Txt_TowerIntroduce;
    Text Txt_Phy;
    Text Txt_Magic;
    Text Txt_CD;
    Text Txt_Range;

    float enemyInitPos = -1368;
    float towerInitPos = 1368;
    float enemyShowPos = -408;
    float towerShowPos = 408;
    float moveTime = 0.5f;

    List<TowerIntroBtn> towerBtnList;
    List<EnemyIntroBtn> enemyBtnList;
    public override void Init()
    {
        base.Init();
        mBtn_Close = Find<Button>("Btn_Close");
        TowerIntroPanel = Find<Transform>("TowerIntroPanel");
        EnemyIntroPanel = Find<Transform>("EnemyIntroPanel");
        EnemyContent = Find<Transform>("EnemyContent");
        TowerContent = Find<Transform>("TowerContent");

        mBtn_EnemyClose = UITool.FindChild<Button>(EnemyIntroPanel.gameObject, "Btn_EnemyClose");
        mBtn_TowerClose = UITool.FindChild<Button>(TowerIntroPanel.gameObject, "Btn_TowerClose");

        Img_EnemyPicture = UITool.FindChild<Image>(EnemyIntroPanel.gameObject, "Img_Picture");
        Txt_EnemyName = UITool.FindChild<Text>(EnemyIntroPanel.gameObject, "Txt_Name");
        Txt_EnemyIntroduce = UITool.FindChild<Text>(EnemyIntroPanel.gameObject, "Txt_Introduce");
        Txt_TotalLife = UITool.FindChild<Text>(EnemyIntroPanel.gameObject, "Txt_TotalLife");
        Txt_Heart = UITool.FindChild<Text>(EnemyIntroPanel.gameObject, "Txt_Heart");
        Txt_Def = UITool.FindChild<Text>(EnemyIntroPanel.gameObject, "Txt_Def");
        Txt_Mdef = UITool.FindChild<Text>(EnemyIntroPanel.gameObject, "Txt_Mdef");
        Txt_Speed = UITool.FindChild<Text>(EnemyIntroPanel.gameObject, "Txt_Speed");

        Img_TowerPicture= UITool.FindChild<Image>(TowerIntroPanel.gameObject, "Img_Picture");
        Txt_TowerName = UITool.FindChild<Text>(TowerIntroPanel.gameObject, "Txt_Name");
        Txt_TowerIntroduce = UITool.FindChild<Text>(TowerIntroPanel.gameObject, "Txt_Introduce");
        Txt_Phy = UITool.FindChild<Text>(TowerIntroPanel.gameObject, "Txt_Phy");
        Txt_Magic = UITool.FindChild<Text>(TowerIntroPanel.gameObject, "Txt_Magic");
        Txt_CD = UITool.FindChild<Text>(TowerIntroPanel.gameObject, "Txt_CD");
        Txt_Range = UITool.FindChild<Text>(TowerIntroPanel.gameObject, "Txt_Range");

        towerBtnList = new List<TowerIntroBtn>();
        enemyBtnList = new List<EnemyIntroBtn>();
        SetTowerIntroBtn();
        SetEnemyIntroBtn();

    }

    public override void OnShow()
    {
        base.OnShow();
        TowerIntroPanel.SetLocalPosX(towerInitPos);
        EnemyIntroPanel.SetLocalPosX(enemyInitPos);
        mBtn_EnemyClose.interactable = false;
        mBtn_TowerClose.interactable = false;
        mBtn_Close.onClick.AddListener(ClosePanel);
        
        mBtn_EnemyClose.onClick.AddListener(BtnClose_Enemy);
        mBtn_TowerClose.onClick.AddListener(BtnClose_Tower);
    }
    
    public override void OnHide()
    {
        base.OnHide();
        mBtn_Close.onClick.RemoveAllListeners();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        RemoveEnemyIntroBtn();
        RemoveTowerIntroBtn();
        towerBtnList = null;
        enemyBtnList = null;
    }

    private void ClosePanel()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        UIMgr.Instance.Hide(UIPanelName.HelpPanel);
    }

    private void ShowTowerIntro()
    {
        TowerIntroPanel.DOLocalMoveX(towerShowPos, moveTime);
        mBtn_TowerClose.interactable = true;
    }

    private void HideToweIntro()
    {
        TowerIntroPanel.DOLocalMoveX(towerInitPos, moveTime);
        mBtn_TowerClose.interactable = false;
    }

    private void ShowEnemyIntro()
    {
        EnemyIntroPanel.DOLocalMoveX(enemyShowPos, moveTime);
        mBtn_EnemyClose.interactable = true;
    }

    private void HideEnemyIntro()
    {
        EnemyIntroPanel.DOLocalMoveX(enemyInitPos, moveTime);
        mBtn_EnemyClose.interactable = false;
    }

    private void BtnClose_Enemy()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        HideEnemyIntro();
    }

    private void BtnClose_Tower()
    {
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        HideToweIntro();
    }

    public void UpdateTowerIntro(int toweID)
    {
        //更新图片 
        TowerInfo info = TowerInfoMgr.Instance.towerInfoList[toweID];
        Txt_TowerName.text = info.Name;
        Txt_TowerIntroduce.text = info.Introduce;
        Img_TowerPicture.sprite = FactoryMgr.Instance.GetSprite(info.helpSprite);
        if (info.damageType == 1)
        {
            Txt_Phy.text = info.damage.ToString();
            Txt_Magic.text = "0";
        }
        else
        {
            Txt_Phy.text = "0";
            Txt_Magic.text = info.damage.ToString();
        }

        Txt_CD.text = info.CD.ToString();
        Txt_Range.text = info.Range.ToString();
        if (!mBtn_TowerClose.interactable)//说明面板没有显示，显示面板
        {
            ShowTowerIntro();
        }
        if(mBtn_EnemyClose.interactable)//说明另一个面板还显示着，隐藏它
        {
            HideEnemyIntro();
        }
    }

    public void UpdateEnemyIntro(int EnemyID)
    {
        //更新图片
        EnemyInfo info = EnemyInfoMgr.Instance.enemyInfoList[EnemyID];
        Txt_EnemyName.text = info.Name;
        Txt_EnemyIntroduce.text = info.Introduce;
        Txt_TotalLife.text = info.life.ToString();
        Txt_Def.text = info.Def.ToString();
        Txt_Mdef.text = info.Mdef.ToString();
        Txt_Magic.text = info.Mdef.ToString();
        Txt_Heart.text = info.heart.ToString();
        Txt_Speed.text = info.speed.ToString();
        Img_EnemyPicture.sprite = FactoryMgr.Instance.GetSprite(info.helpSprite);
        if (!mBtn_EnemyClose.interactable)
        {
            ShowEnemyIntro();
        }
        if(mBtn_TowerClose.interactable)
        {
            HideToweIntro();
        }
    }

    private void SetTowerIntroBtn()
    {
        for(int i=0;i<TowerInfoMgr.Instance.towerInfoList.Count;i++)
        {
            GameObject go = FactoryMgr.Instance.GetUI(StringMgr.HelpItemBtn);
            go.transform.SetParent(TowerContent);
            go.transform.localScale = Vector3.one;

            TowerIntroBtn btn = new TowerIntroBtn(i,go, this);
            towerBtnList.Add(btn);
        }
    }

    private void SetEnemyIntroBtn()
    {
        for (int i = 0; i < EnemyInfoMgr.Instance.enemyInfoList.Count; i++)
        {
            GameObject go = FactoryMgr.Instance.GetUI(StringMgr.HelpItemBtn);
            go.transform.SetParent(EnemyContent);
            go.transform.localScale = Vector3.one;

            EnemyIntroBtn btn = new EnemyIntroBtn(i, go, this);
            enemyBtnList.Add(btn);
        }
    }

    private void RemoveTowerIntroBtn()
    {
        foreach (var item in towerBtnList)
        {
            item.Clear();
        }
        towerBtnList.Clear();
    }

    private void RemoveEnemyIntroBtn()
    {
        foreach (var item in enemyBtnList)
        {
            item.Clear();
        }
        enemyBtnList.Clear();
    }
    
    public class TowerIntroBtn:BaseUIListItem
    {
        //获取按钮和图片
        Button towerBtn;
        HelpPanel panel;
        public TowerIntroBtn(int id,GameObject root,HelpPanel panel)
        {
            this.id = id;
            this.root = root;
            this.panel = panel;
            towerBtn = root.GetComponent<Button>();
            towerBtn.image.sprite = FactoryMgr.Instance.GetSprite(TowerInfoMgr.Instance.towerInfoList[id].helpSprite);
            towerBtn.onClick.AddListener(BtnClick);
        }

        private void BtnClick()
        {
            AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
            panel.UpdateTowerIntro(id);
        }

        public override void Clear()
        {
            base.Clear();
            towerBtn.onClick.RemoveAllListeners();
        }
    }
   
    public class EnemyIntroBtn:BaseUIListItem
    {
        Button enemyBtn;
        HelpPanel panel;
        public EnemyIntroBtn(int id, GameObject root, HelpPanel panel)
        {
            this.id = id;
            this.root = root;
            this.panel = panel;
            enemyBtn = root.GetComponent<Button>();
            enemyBtn.image.sprite = FactoryMgr.Instance.GetSprite(EnemyInfoMgr.Instance.enemyInfoList[id].helpSprite);
            enemyBtn.onClick.AddListener(BtnClick);
        }

        private void BtnClick()
        {
            AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
            panel.UpdateEnemyIntro(id);
        }

        public override void Clear()
        {
            base.Clear();
            enemyBtn.onClick.RemoveAllListeners();
        }

    }

}
