using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework.Factory
{
    public class ScriptableObjectFactory
    {
        protected Dictionary<string, ScriptableObject> factoryDict = new Dictionary<string, ScriptableObject>();
        protected string LoadPath;

        public ScriptableObjectFactory()
        {
            LoadPath = "AssetData/";
        }
        public T GetResource<T>(string resourcePath) where T:ScriptableObject
        {
            T itemGo = null;
            string itemLoadPath = LoadPath + resourcePath;
            if (factoryDict.ContainsKey(resourcePath))
            {
                itemGo = (T)factoryDict[resourcePath];
            }
            else
            {
                itemGo = Resources.Load<T>(itemLoadPath);
                factoryDict.Add(resourcePath, itemGo);

            }

            if (itemGo == null)
            {
                Debug.Log(resourcePath + "获取失败，路径有误" + itemLoadPath);
            }

            return itemGo;
        }
        
    }
}
