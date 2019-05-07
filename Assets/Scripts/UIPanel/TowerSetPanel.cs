using Assets.Framework;
using Assets.Framework.Audio;
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
    Image Img_UpPriceBG;
    bool isUpLevel;

    public GridPoint selectGrid;
    LevelInfo levelInfo;
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
        Img_UpPriceBG = Find<Image>("Img_UpPriceBG");
        levelInfo = LevelInfoMgr.Instance.levelInfoList[GameRoot.Instance.pickLevel];
        SetTowerBtn();
        SellBtn.onClick.AddListener(SellClick);
        UpLevelBtn.onClick.AddListener(UpClick);
        TowerSelect.gameObject.SetActive(false);
        TowerSet.gameObject.SetActive(false);
        EventCenter.AddListener<GridPoint>(EventType.HandleGrid, HandleGrid);
        EventCenter.AddListener(EventType.RestartGame,RestartGame);
        EventCenter.AddListener(EventType.LeaveGameScene, RestartGame);
    }

    public void SetTowerBtn()
    {
        //获取头4位并且赋值 
        for (int i = 0; i < 4; i++)
        {
            GameObject go = FactoryMgr.Instance.GetUI("TowerSelectBtn");
            go.transform.SetParent(TowerSelect);
            go.transform.localScale = new Vector3(1.3f, 1.3f, 1);
            go.transform.position = pos[i].position;
            btnTransArr[i] = go.transform;
            TowerButton tb =
                new TowerButton(LevelInfoMgr.Instance.levelInfoList[GameRoot.Instance.pickLevel].towerList[i], go,this);
            towerBtnArr[i] = tb;
        }

    }

    public override void OnShow()
    {
        base.OnShow();
    }

    public override void OnHide()
    {
        base.OnHide();
        ResetPanelPos();
        selectGrid = null;
    }

    public override void Update()
    {
        base.Update();
        if (TowerSelect.gameObject.activeSelf)
        {
            UpdateBtnSprite();
        }
        if (TowerSet.gameObject.activeSelf)
        {
            UpdateTowerSet(selectGrid);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        EventCenter.RemoveListener(EventType.LeaveGameScene, RestartGame);
        EventCenter.RemoveListener(EventType.RestartGame, RestartGame);
        EventCenter.RemoveListener<GridPoint>(EventType.HandleGrid, HandleGrid);
        RemoveListenerTowerBtn();
        ResetPanelPos();
        UpLevelBtn.onClick.RemoveAllListeners();
        SellBtn.onClick.RemoveAllListeners();
        TowerSelect.gameObject.SetActive(true);
        TowerSet.gameObject.SetActive(true);
        Img_UpPriceBG.gameObject.SetActive(true);
        selectGrid = null;
    }

    public void RemoveListenerTowerBtn()
    {
        for (int i = 0; i < 4; i++)
        {
            towerBtnArr[i].Clear();
        }
    }
   //判断显示升级面板还是建塔面板
    public void CorrectTowerSetPanel()
    {
        //GridPoint selectGrid = GameController.Instance.selectGrid;
        //if (GameController.Instance.selectGrid == null) return;
        if (selectGrid == null) 
        {
            TowerSet.gameObject.SetActive(false);
            TowerSelect.gameObject.SetActive(false);
            return;
        }
        rootUI.transform.position = Camera.main.WorldToScreenPoint(selectGrid.transform.position);
        if (selectGrid.gridState.hasTower)
        {
            //显示升级
            TowerSet.gameObject.SetActive(true);
            TowerSelect.gameObject.SetActive(false);
            CorrectTowerSet(selectGrid.gridState.id);
            UpdateTowerSet(selectGrid);
        }  
        else
        {
            //显示选择
            TowerSelect.gameObject.SetActive(true);
            TowerSet.gameObject.SetActive(false);
            CorrectTowerSelect(selectGrid.gridState.id);
            UpdateBtnSprite();
        }

    }
    //建塔面板纠正位置
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
    //建塔面板纠正位置
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

    /// <summary>
    /// 处理格子，塔的创建和显示
    /// </summary>
    /// <param name="gp"></param>
    public void HandleGrid(GridPoint gp)
    {
        if (gp == null)
            return;
        if (selectGrid == null)
        {
            selectGrid = gp;
            //如果有塔就显示塔的范围
            selectGrid.TowerRange(true);
        }
        else if (selectGrid == gp)
        {
            selectGrid.TowerRange(false);
            selectGrid = null;
        }
        else
        {
            selectGrid.TowerRange(false);
            selectGrid = gp;
            selectGrid.TowerRange(true);
        }

        CorrectTowerSetPanel();
    }


    void SellClick()
    {
        int sellCoin = selectGrid.baseTower.towerInfo.sellCoin;
        selectGrid.baseTower.Recycle();
        selectGrid.InitGrid();
        selectGrid.SetTowerID(-1);//重新置为建塔点
        GameController.Instance.ChangeCoin(sellCoin);
        AudioMgr.Instance.PlayEffectMusic(StringMgr.SellTower);
        EventCenter.Broadcast(EventType.HandleGrid, selectGrid);
        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.Sell_10);
        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.sell_50);
    }

    void UpClick()
    {
        if (!isUpLevel) return;
        //删除当前格子下的塔，回收，更新格子塔的信息
        int nextId = selectGrid.baseTower.towerInfo.nextTowerId;
        selectGrid.baseTower.Recycle();
        selectGrid.InitGrid();
        //生成新的塔
        //获取下一个塔的id
        selectGrid.SetTowerID(nextId);
        GameController.Instance.CreateTower(selectGrid);
    }

    public void UpdateBtnSprite()
    {
        for (int i = 0; i < 4; i++)
        {
            towerBtnArr[i].UpdateSprite();
        }
    }

    void UpdateTowerSet(GridPoint selectGrid)
    {
        //获得当前的
        isUpLevel = false;
        int index = selectGrid.baseTower.towerInfo.nextTowerId;
        int upCoin;
        if(index==0||!levelInfo.towerList.Contains(index))
        {
            upCoin = 0;
            UpLevelBtn.image.sprite = FactoryMgr.Instance.GetSprite(StringMgr.towerLock);
            Img_UpPriceBG.gameObject.SetActive(false);
        }
        else
        {
            upCoin = TowerInfoMgr.Instance.towerInfoList[index - 1].buildCoin;
            if (upCoin <= GameController.Instance.Coin)
            {
                UpLevelBtn.image.sprite = FactoryMgr.Instance.GetSprite(StringMgr.towerCanUp);
                Img_UpPriceBG.gameObject.SetActive(true);
                Txt_UpPrice.text = upCoin.ToString();
                isUpLevel = true;
            }
            else
            {
                UpLevelBtn.image.sprite = FactoryMgr.Instance.GetSprite(StringMgr.towerCantUp);
                Img_UpPriceBG.gameObject.SetActive(false);
            }
        }
        
        
    }

    void RestartGame()
    {
        selectGrid = null;
        TowerSelect.gameObject.SetActive(false);
        TowerSet.gameObject.SetActive(false);
        ResetPanelPos();
    }

    public class TowerButton : BaseUIListItem
    {
        Image img_PriceBG;
        Text Txt_Price;
        Button btn;
        bool isActive;
        TowerSetPanel panel;
        public TowerButton(int index, GameObject go, TowerSetPanel bspanel)
        {
            id = index;
            root = go;
            panel = bspanel;
            img_PriceBG = Find<Image>("Img_PriceBG");
            Txt_Price = Find<Text>("Txt_Price");
            btn = go.GetComponent<Button>();

            if (id == 0)
            {
                //更换图片
                btn.image.sprite = FactoryMgr.Instance.GetSprite(StringMgr.towerLock);
                img_PriceBG.gameObject.SetActive(false);
                isActive = false;
            }
            else
            {
                //设置图片，注册事件
                btn.image.sprite = FactoryMgr.Instance.GetSprite("Change/t" + id + "_1");
                Txt_Price.text = TowerInfoMgr.Instance.towerInfoList[id - 1].buildCoin.ToString();
                btn.onClick.AddListener(OnBtnClick);
            }

        }
        public void UpdateSprite()
        {
            isActive = false;
            //查看一下钱够不够，够就isActive=true;
            if (GameController.Instance.Coin >= TowerInfoMgr.Instance.towerInfoList[id - 1].buildCoin)
            {
                isActive = true;
            }
            if (isActive)
            {
                btn.image.sprite = FactoryMgr.Instance.GetSprite("Change/t" + id + "_1");
            }
            else
            {
                btn.image.sprite = FactoryMgr.Instance.GetSprite("Change/t" + id + "_2");
            }
        }

        void OnBtnClick()
        {
            if (!isActive) return;
            GridPoint gp = panel.selectGrid;
            if (gp == null) return;

            gp.SetTowerID(id);
            GameController.Instance.CreateTower(gp);
        }

        public override void Clear()
        {
            base.Clear();
            btn.onClick.RemoveAllListeners();
            img_PriceBG.gameObject.SetActive(true);
        }

    }

}