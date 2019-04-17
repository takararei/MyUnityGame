
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Framework.Factory
{
    public class BaseResourceFactory<T> : IBaseResourceFactory<T> where T:Object
    {
        protected Dictionary<string, T> factoryDict = new Dictionary<string, T>();

        protected string LoadPath;
        public T GetResource(string resourcePath)
        {
            T item = null;
            string itemLoadPath = LoadPath + resourcePath;
            if(!factoryDict.TryGetValue(resourcePath,out item))
            {
                item = Resources.Load<T>(itemLoadPath);
                if (item == null)
                {
                    Debug.Log(resourcePath + "获取失败，路径有误" + itemLoadPath);
                }
                else
                {
                    factoryDict.Add(resourcePath, item);
                }
            }
            
            //if(factoryDict.ContainsKey(resourcePath))
            //{
            //    item = factoryDict[resourcePath];
            //}
            //else
            //{
            //    item = Resources.Load<T>(itemLoadPath);
            //    factoryDict.Add(resourcePath, item);
            //}

            //if(item==null)
            //{
            //    Debug.Log(resourcePath + "获取失败，路径有误" + itemLoadPath);
            //}

            return item;
        }
    }
}
