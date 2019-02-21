using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListControl : MonoBehaviour
{
    public GameObject content;
    public ScrollRect scrollRect;
    int lastID=-1;
    public List<ListItemBtn> BtnItemList=new List<ListItemBtn>();
    void Start()
    {
        Object res = Resources.Load("TestButton");
        for (int i = 0; i < 20; i++)
        {
            //实例物体出来
            GameObject go = Instantiate(res) as GameObject;
            go.transform.SetParent(content.transform);

            //对应类
            ListItemBtn lib = new ListItemBtn(go);
            lib.ID = i;
            lib.image.gameObject.SetActive(false);
            lib.btn.onClick.AddListener(() =>
                {
                    if(lastID!=-1)
                    {
                        BtnItemList[lastID].image.gameObject.SetActive(false);
                    }
                    Debug.Log(lib.ID);
                    lib.image.gameObject.SetActive(true);
                    lastID = lib.ID;
                }
            );


            BtnItemList.Add(lib);

        }
    }

   public class ListItemBtn
    {
        public Image image;
        public Button btn;
        public int ID = 0;

        public ListItemBtn(GameObject go)
        {
            btn = go.transform.Find("Button").GetComponent<Button>();
            image = go.transform.Find("Image").GetComponent<Image>();
        }
    }
}
