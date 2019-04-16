﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.Factory
{
    public class BaseFactory:IBaseFactory
    {
        //游戏物体资源 存放已经加载过的资源，如 子弹，用于生成新子弹
        protected Dictionary<string, GameObject> factoryDict = new Dictionary<string, GameObject>();

        //对象池字典 栈 (类型，对象链表) （子弹，子弹对象池）已经生成的
        protected Dictionary<string, Stack<GameObject>> objectPoolDict = new Dictionary<string, Stack<GameObject>>();

        protected string loadPath;
        public BaseFactory()
        {
            loadPath = "Prefabs/";
        }

        public GameObject GetItem(string itemName)
        {
            GameObject itemGo = null;
            //字典中是否有这种类型的对象的对象池
            if (objectPoolDict.ContainsKey(itemName))
            {
                //池中有就弹出，没有就生成
                if (objectPoolDict[itemName].Count == 0)
                {
                    GameObject go = GetResource(itemName);
                    itemGo = GameRoot.Instance.CreateItem(go);
                }
                else
                {
                    itemGo = objectPoolDict[itemName].Pop();
                    itemGo.SetActive(true);
                }
            }
            else
            {
                //没有就创建对象池
                objectPoolDict.Add(itemName, new Stack<GameObject>());
                GameObject go = GetResource(itemName);
                itemGo = GameRoot.Instance.CreateItem(go);
            }

            if (itemGo == null)
            {
                Debug.Log(itemName + "实例获取失败");
            }

            return itemGo;
        }

        private GameObject GetResource(string itemName)
        {
            GameObject itemGo = null;
            string itemLoadPath = loadPath + itemName;
            if (factoryDict.ContainsKey(itemName))
            {
                itemGo = factoryDict[itemName];

            }
            else
            {
                itemGo = Resources.Load<GameObject>(itemLoadPath);
                factoryDict.Add(itemName, itemGo);
            }

            if (itemGo == null)
            {
                Debug.Log(itemName + "资源获取失败，失败路径" + itemLoadPath);
            }
            return itemGo;
        }

        public void PushItem(string itemName, GameObject item)
        {
            item.SetActive(false);
            item.transform.SetParent(GameRoot.Instance.transform);
            if (objectPoolDict.ContainsKey(itemName))
            {
                objectPoolDict[itemName].Push(item);
            }
            else
            {
                Debug.Log("字典没有这样的对象池栈" + itemName);
            }

        }
    }
}
