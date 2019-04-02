using Assets.Framework.Extension;
using Assets.Framework.Factory;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class TowerSetPanel : BasePanel
{
    Transform[] pos;
    Transform TowerSet;
    Transform TowerSelect;
    Transform[] btnTransArr;
    TowerButton[] towerBtnArr;
    Button UpLevelBtn;
    Button SellBtn;
    Text Txt_UpPrice;
    public override void Init()
    {
        base.Init();
        rootUI.transform.position = new Vector3(-900, 0, 0);
        pos = new Transform[8];
        btnTransArr = new Transform[4];
        towerBtnArr = new TowerButton[4];
        for (int i = 0; i < 8; i++)
        {
            pos[i] = Find<Transform>((i + 1).ToString());
        }
        TowerSet = Find<Transform>("TowerSet");
        TowerSelect = Find<Transform>("TowerSelect");
        UpLevelBtn = Find<Button>("UpLevel");
        SellBtn = Find<Button>("Sell");
        Txt_UpPrice = Find<Text>("Up_Price");
        SetTowerBtn();
        GameController.Instance.towerSetPanel = this;
    }

    public void SetTowerBtn()
    {
        //获取头4位并且赋值
        //LevelInfoMgr.Instance.levelInfoList[PlayerStatics.Instance.nowLevel].towerList;
        for (int i = 0; i < 4; i++)
        {
            GameObject go = FactoryManager.Instance.GetUI("TowerSelectBtn");
            go.transform.SetParent(TowerSelect);
            go.transform.localScale = new Vector3(1.3f, 1.3f, 1);
            go.transform.position = pos[i].position;
            btnTransArr[i] = go.transform;
            TowerButton tb =
                new TowerButton(LevelInfoMgr.Instance.levelInfoList[PlayerStatics.Instance.nowLevel].towerList[i], go);
            towerBtnArr[i] = tb;
        }

    }

    public void UpdateBtnSprite()
    {
        for (int i = 0; i < 4; i++)
        {
            towerBtnArr[i].UpdateSprite();
        }
    }

    public void RemoveListenerTowerBtn()
    {
        for (int i = 0; i < 4; i++)
        {
            towerBtnArr[i].Clear();
        }
    }
    public override void OnShow()
    {
        base.OnShow();
        UpdateBtnSprite();
    }

    public override void OnHide()
    {
        base.OnHide();
        //RemoveListenerTowerBtn();
    }

    public void CorrectTowerSetPanel()
    {
        GridPoint selectGrid = GameController.Instance.selectGrid;
        rootUI.transform.position = Camera.main.WorldToScreenPoint(selectGrid.transform.position);
        if (selectGrid.gridState.hasTower)
        {
            //显示升级
            TowerSet.gameObject.SetActive(true);
            TowerSelect.gameObject.SetActive(false);
            CorrectTowerSet(selectGrid.gridState.id);
        }
        else
        {
            //显示选择
            TowerSelect.gameObject.SetActive(true);
            TowerSet.gameObject.SetActive(false);
            CorrectTowerSelect(selectGrid.gridState.id);
        }

    }
    void CorrectTowerSelect(int index)
    {
        if (index <= 7)
        {
            btnTransArr[0].position = pos[6].position;
            btnTransArr[3].position = pos[7].position;
            btnTransArr[1].position = pos[1].position;
            btnTransArr[2].position = pos[2].position;
        }
        else if (index >= 100)
        {
            btnTransArr[1].position = pos[6].position;
            btnTransArr[2].position = pos[7].position;
            btnTransArr[0].position = pos[0].position;
            btnTransArr[3].position = pos[3].position;
        }
        else if (index % 9 == 0)
        {
            btnTransArr[0].position = pos[0].position;
            btnTransArr[3].position = pos[4].position;
            btnTransArr[1].position = pos[1].position;
            btnTransArr[2].position = pos[5].position;
        }
        else if ((index + 1) % 9 == 0)
        {
            btnTransArr[0].position = pos[4].position;
            btnTransArr[3].position = pos[3].position;
            btnTransArr[1].position = pos[5].position;
            btnTransArr[2].position = pos[2].position;
        }
        else
        {
            btnTransArr[0].position = pos[0].position;
            btnTransArr[3].position = pos[3].position;
            btnTransArr[1].position = pos[1].position;
            btnTransArr[2].position = pos[2].position;
        }
    }
    void CorrectTowerSet(int index)
    {
        if (index % 9 == 0)
        {
            UpLevelBtn.transform.position = pos[6].position;
            SellBtn.transform.position = pos[5].position;
        }
        else if ((index + 1) % 9 == 0)
        {
            UpLevelBtn.transform.position = pos[5].position;
            SellBtn.transform.position = pos[7].position;
        }
        else
        {
            UpLevelBtn.transform.position = pos[6].position;
            SellBtn.transform.position = pos[7].position;
        }
    }
    public void ResetPanelPos()
    {
        rootUI.transform.position = new Vector3(-900, 0, 0);
    }



    public class TowerButton : BaseUIListItem
    {
        Image img_PriceBG;
        Text Txt_Price;
        Button btn;
        bool isActive;
        public TowerButton(int index, GameObject go)
        {
            id = index;
            root = go;
            img_PriceBG = Find<Image>("Img_PriceBG");
            Txt_Price = Find<Text>("Txt_Price");
            btn = go.GetComponent<Button>();

            if (id == 0)
            {
                //更换图片
                btn.image.sprite = FactoryManager.Instance.GetSprite("Change/t0");
                img_PriceBG.gameObject.SetActive(false);
                isActive = false;
            }
            else
            {
                //设置图片，注册事件
                btn.image.sprite = FactoryManager.Instance.GetSprite("Change/t" + id + "_1");
                Txt_Price.text = TowerInfoMgr.Instance.towerInfoList[id - 1].buildCoin.ToString();
                btn.onClick.AddListener(OnBtnClick);
            }

        }
        public void UpdateSprite()
        {
            isActive = false;
            //查看一下钱够不够，够就isActive=true;
            if (GameController.Instance.coin >= TowerInfoMgr.Instance.towerInfoList[id - 1].buildCoin)
            {
                isActive = true;
            }
            if (isActive)
            {
                btn.image.sprite = FactoryManager.Instance.GetSprite("Change/t" + id + "_1");
            }
            else
            {
                btn.image.sprite = FactoryManager.Instance.GetSprite("Change/t" + id + "_2");
            }
        }

        void OnBtnClick()
        {
            if (!isActive) return;
            Debug.Log(id);
            GameController.Instance.selectGrid.SetTowerID(id);
            GameController.Instance.TowerSet();

        }

        public override void Clear()
        {
            base.Clear();
            btn.onClick.RemoveAllListeners();
        }

    }

}