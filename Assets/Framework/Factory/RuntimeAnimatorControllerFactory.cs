using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework.Factory
{
    public class RuntimeAnimatorControllerFactory : IBaseResourceFactory<RuntimeAnimatorController>
    {
        protected Dictionary<string, RuntimeAnimatorController> factoryDict = new Dictionary<string, RuntimeAnimatorController>();
        protected string LoadPath;
        public RuntimeAnimatorControllerFactory()
        {
            LoadPath = "Animator/";
        }
        public RuntimeAnimatorController GetResource(string resourcePath)
        {
            RuntimeAnimatorController itemGo = null;
            string itemLoadPath = LoadPath + resourcePath;
            if (factoryDict.ContainsKey(resourcePath))
            {
                itemGo = factoryDict[resourcePath];
            }
            else
            {
                itemGo = Resources.Load<RuntimeAnimatorController>(itemLoadPath);
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