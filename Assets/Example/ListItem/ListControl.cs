using Assets.Framework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListControl : MonoBehaviour
{
    public GameObject content;
    public ScrollRect scrollRect;
    //int lastID=-1;
    public List<ListItemBtn> BtnItemList=new List<ListItemBtn>();
    public static Object res;
    void Start()
    {
        res = Resources.Load("TestButton");

        for (int i = 0; i < 20; i++)
        {
            //实例物体出来
            GameObject go = Instantiate(res) as GameObject;
            go.transform.SetParent(content.transform);

            //对应类
            ListItemBtn lib = new ListItemBtn(i,this);
            BtnItemList.Add(lib);
            
        }
    }
    

   public class ListItemBtn:BaseUIListItem
    {
        public Image image;
        public Button btn;
        public static int lastID=-1;
        public List<ListItemBtn> BtnList;
        public ListItemBtn(int index, ListControl listC)
        {
            root = GameObject.Instantiate(res) as GameObject;
            BtnList =listC.BtnItemList;

            btn = Find<Button>("Button");
            image = Find<Image>("Image");
            image.gameObject.SetActive(false);
            id = index;
            btn.onClick.AddListener(Test);
        }

        public void Test()
        {
            if (ListItemBtn.lastID != -1)
            {
                BtnList[lastID].image.gameObject.SetActive(false);
            }
            image.gameObject.SetActive(true);
            ListItemBtn.lastID = id;
        }

        public override void Clear()
        {
            base.Clear();
            btn.onClick.RemoveAllListeners();
        }

    }
}
