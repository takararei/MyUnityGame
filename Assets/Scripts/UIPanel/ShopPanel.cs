using Assets.Framework.Factory;
using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{

    private Button mBtn_Close;
    private Button mBtn_Buy;
    private Text mTxt_Count;//持有钻石数
    //商品展示
    private Text mTxt_Name;//商品名称
    private Text mTxt_Introduce;//商品介绍
    private Text mTxt_Diamond;//商品价格

    Transform GoodsContent;
    Transform PackageContent;

    int pickItem;

    PlayerStatics pStatics;
    ItemInfoMgr itemMgr;
    public override void Init()
    {
        base.Init();
        pStatics = PlayerStatics.Instance;
        itemMgr = ItemInfoMgr.instance;
        mBtn_Close = Find<Button>("Btn_Close");
        mBtn_Buy = Find<Button>("Btn_Buy");
        mTxt_Count = Find<Text>("Txt_Count");

        mTxt_Name = Find<Text>("Txt_Name");
        mTxt_Introduce = Find<Text>("Txt_Introduce");
        mTxt_Diamond = Find<Text>("Txt_Diamond");

        GoodsContent = Find<Transform>("GoodsContent");
        PackageContent = Find<Transform>("PackageContent");
        itemHoldList = new List<ItemHold>();
        itemShopBtnList = new List<ItemShop>();
    }

    public override void OnShow()
    {
        base.OnShow();
        mBtn_Close.onClick.AddListener(OnHide);
        mBtn_Buy.onClick.AddListener(OnBuyButtonClick);

        mTxt_Count.text = pStatics.DO.ToString();
        SetItemHold();
        SetItemGoods();
        ItemIntroduceUpdate(0);
        EventCenter.AddListener<int>(EventType.ItemIntroduceUpdate, ItemIntroduceUpdate);
    }

    public override void OnHide()
    {
        base.OnHide();
        EventCenter.RemoveListener<int>(EventType.ItemIntroduceUpdate, ItemIntroduceUpdate);
        RemoveItemGoods();
        RemoveItemHold();
        mBtn_Close.onClick.RemoveAllListeners();
        mBtn_Buy.onClick.RemoveAllListeners();
    }

    public void ItemIntroduceUpdate(int index)
    {
        mTxt_Name.text = itemMgr.itemInfoList[index].name;
        mTxt_Introduce.text = itemMgr.itemInfoList[index].introduce;
        mTxt_Diamond.text = itemMgr.itemInfoList[index].price.ToString();
    }

    public void SetItemHold()
    {
        for (int i = 0; i < itemMgr.itemInfoList.Count; i++)
        {
            GameObject itemHold = FactoryManager.Instance.GetUI(StringMgr.ItemHold);
            itemHold.transform.SetParent(PackageContent);
            itemHold.transform.localScale = Vector3.one;
            //道具持有数量等,更换图片资源TODO
            ItemHold ih = new ItemHold(i, itemHold);
            itemHoldList.Add(ih);
        }
    }
    public void SetItemGoods()
    {
        for (int i = 0; i < itemMgr.itemInfoList.Count; i++)
        {
            GameObject itemShopButton = FactoryManager.Instance.GetUI(StringMgr.ItemShopButton);
            itemShopButton.transform.SetParent(GoodsContent);
            itemShopButton.transform.localScale = Vector3.one;
            //更换图片TODO
            ItemShop itemShop = new ItemShop(i, itemShopButton);
            itemShopBtnList.Add(itemShop);
        }
    }
    public void RemoveItemGoods()
    {
        for(int i=0;i<itemShopBtnList.Count;i++)
        {
            FactoryManager.Instance.PushUI(StringMgr.ItemShopButton, GoodsContent.GetChild(0).gameObject);
            itemShopBtnList[i].Clear();
        }
        itemShopBtnList.Clear();
    }
    public void RemoveItemHold()
    {
        for(int i=0;i<itemHoldList.Count;i++)
        {
            FactoryManager.Instance.PushUI(StringMgr.ItemHold, PackageContent.GetChild(0).gameObject);
            itemHoldList[i].Clear();
        }
        itemHoldList.Clear();
    }

    private void OnBuyButtonClick()
    {

    }


    List<ItemHold> itemHoldList;
    List<ItemShop> itemShopBtnList;
    class ItemHold:BaseUIListItem
    {
        Image itemImage;
        Text txt_ItemCount;

        public ItemHold(int index,GameObject root)
        {
            this.id = index;
            this.root = root;
            itemImage = root.GetComponent<Image>();
            txt_ItemCount = Find<Text>("Txt_ItemCount");
            txt_ItemCount.text = PlayerStatics.Instance.itemNum[id].ToString();
            //itemImage.sprite = FactoryManager.Instance.GetSprite("");
            EventCenter.AddListener(EventType.ItemCountUpdate, ItemCountUpdate);
        }

        public void ItemCountUpdate()
        {
            txt_ItemCount.text = PlayerStatics.Instance.itemNum[id].ToString();
        }

        public override void Clear()
        {
            base.Clear();
            EventCenter.RemoveListener(EventType.ItemCountUpdate, ItemCountUpdate);
        }
    }

    class ItemShop:BaseUIListItem
    {
        Image bg;
        Button btn_ItemShop;
        Text txt_Price;

        public ItemShop(int index,GameObject root)
        {
            this.id = index;
            this.root = root;
            bg = root.GetComponent<Image>();
            btn_ItemShop = Find<Button>("Btn_Item");
            txt_Price = Find<Text>("Txt_Price");
            btn_ItemShop.onClick.AddListener(OnBtnClick);
            txt_Price.text = ItemInfoMgr.instance.itemInfoList[id].price.ToString();
            //bg.sprite = FactoryManager.Instance.GetSprite("");
        }

        public void OnBtnClick()
        {
            EventCenter.Broadcast(EventType.ItemIntroduceUpdate, id);
        }

        public override void Clear()
        {
            base.Clear();
            btn_ItemShop.onClick.RemoveAllListeners();
        }
    }
}
